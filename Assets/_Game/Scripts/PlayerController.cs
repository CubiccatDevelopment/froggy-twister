using UnityEngine;
using System;
using System.Collections;

[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour
{
    public event Action OnJump;
    public event Action OnLanding;
    public event Action OnDeath;

    new Rigidbody rigidbody;

    bool canJump = true;
    bool grounded = true;

    [SerializeField] GameObject landingParticles;
    [SerializeField] GameObject drowningParticles;
    [SerializeField] Transform mesh;

    Animator animator;

    [SerializeField] float maxForwardForce;
    [SerializeField] float maxUpwardForce;
    [SerializeField] float minLandTime;

    float timeSinceLastJump = 0;
    bool isDead = false;

    GroundDetector groundDetector;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        groundDetector = GetComponent<GroundDetector>();
        rigidbody = GetComponent<Rigidbody>();

        OnJump += () => { Debug.Log("Jump!"); };
        OnLanding += () => { Debug.Log("Land!"); };
    }

    public void Update()
    {
        if(!isDead)
        {
            if (!grounded)
                timeSinceLastJump += Time.deltaTime;

            if (!grounded && groundDetector.Grounded() && timeSinceLastJump >= minLandTime)
            {
                grounded = true;
                landingParticles.SetActive(true);
                animator.Play("frog_land_anim");
                OnLanding?.Invoke();
            }
        }  
    }

    public void Jump(float forcePercent)
    {
        if(grounded && canJump)
        {
            timeSinceLastJump = 0;

            Vector3 jumpVector = transform.forward * maxForwardForce * forcePercent;
            jumpVector.y = maxUpwardForce * forcePercent;

            rigidbody.AddForce(jumpVector, ForceMode.VelocityChange);

            grounded = false;

            Quaternion newRotation = Quaternion.Euler(transform.rotation.eulerAngles + Vector3.up * 90f);

            StartCoroutine(Bundle.LerpToRotationRoutine(transform, newRotation, 0.3f)); 

            transform.SetParent(null);

            animator.Play("frog_backflip_anim");

            OnJump?.Invoke();
        }
    }

    public void Die(Vector3 explosionPosition)
    {
        isDead = true;
        UnFreezeRotation();
        rigidbody.AddExplosionForce(50f, explosionPosition, 50f, 3f, ForceMode.VelocityChange);

        StartCoroutine(Bundle.LerpToLocalScale(transform, Vector3.zero, 1f));

        OnDeath?.Invoke();
    }

    public void FreezeRotation()
    {
        transform.rotation = Quaternion.identity;
        rigidbody.freezeRotation = true;
    }

    public void UnFreezeRotation()
    {
        rigidbody.freezeRotation = false;
    }

    public void Reset(Transform parent)
    {
        mesh.gameObject.SetActive(true);

        grounded = true;
        FreezeRotation();
        transform.rotation = Quaternion.identity;
        rigidbody.velocity = Vector3.zero;

        transform.SetParent(parent);
        transform.localPosition = Vector3.up * 2f;
        transform.localScale = Vector3.zero;

        StopAllCoroutines();
        StartCoroutine(Bundle.LerpToLocalScale(transform, Vector3.one, 0.5f, null, () => { isDead = false; }));

        isDead = false;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Island") && !isDead)
        {
            Debug.Log("Ouch, islands hurt!");
            Die(collision.transform.position);
        } else if(collision.collider.CompareTag("Water Lily"))
        {
            Debug.Log("Safe landing.");
            transform.SetParent(collision.transform);
        } else if (collision.collider.CompareTag("Water") && !isDead)
        {
            Debug.Log("AAaaaaa, I'm drowning!");
            mesh.gameObject.SetActive(false);
            drowningParticles.SetActive(true);
            Die(collision.transform.position);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Game Bounds") && !isDead)
        {
            Debug.Log("Player out of game bounds.");
            Die(other.transform.position);
        }
    }
}
