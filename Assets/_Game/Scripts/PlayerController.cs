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
    [SerializeField] BoxCollider footCollider;

    bool canJump = true;
    bool grounded = true;
    float skinLength = 0.005f;
    [SerializeField] LayerMask footRayTargetMask;

    [SerializeField] float maxForwardForce;
    [SerializeField] float maxUpwardForce;

    [SerializeField] float minLandTime;
    float timeSinceLastJump = 0;
    bool isDead = false;

    private void Awake()
    {
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

            if (!grounded && Grounded() && timeSinceLastJump >= minLandTime)
            {
                grounded = true;
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

            StartCoroutine(
                Bundle.LerpToRotationRoutine(transform, Quaternion.Euler(transform.rotation.eulerAngles + Vector3.up * 90f), 0.3f));

            OnJump?.Invoke();
        }
    }

    public bool Grounded()
    {
        int raysCount = 3;

        float raysStepX = footCollider.size.x / (raysCount - 1);
        float raysStepZ = footCollider.size.z / (raysCount - 1);

        float rayLength = footCollider.size.y / 2 + skinLength;

        Vector3 raysOrigin = footCollider.center;
        raysOrigin.x -= footCollider.size.x / 2;
        raysOrigin.z -= footCollider.size.z / 2;

        Vector3 rayStart;
        RaycastHit hit;

        for (int x = 0; x < raysCount; x++)
        {
            for (int z = 0; z < raysCount; z++) {
                rayStart.x = raysOrigin.x + x * raysStepX;
                rayStart.y = raysOrigin.y;
                rayStart.z = raysOrigin.z + z * raysStepZ;

                Ray ray = new Ray(transform.TransformPoint(rayStart), -transform.up);

                Debug.DrawLine(
                    transform.TransformPoint(rayStart), 
                    transform.TransformPoint(rayStart) + Vector3.down * rayLength, 
                    Color.red);

                if (Physics.Raycast(ray, out hit, rayLength, footRayTargetMask))
                    return true;
            }
        }

        return false;
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

    public void Reset(Vector3 position)
    {
        grounded = true;
        rigidbody.velocity = Vector3.zero;
        transform.position = position;
        FreezeRotation();
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
        }
    }
}
