using UnityEngine;
using System;
using System.Collections;

[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour
{
    public event Action OnJump;
    public event Action OnLanding;

    new Rigidbody rigidbody;
    [SerializeField] BoxCollider footCollider;

    bool canJump = true;
    bool grounded = true;
    float skinLength = 0.005f;
    [SerializeField] LayerMask footRayTargetMask;

    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody>();

        OnJump += () => { Debug.Log("Jump!"); };
        OnLanding += () => { Debug.Log("Land!"); };
    }

    public void Update()
    {
        if (!grounded && Grounded())
        {
            grounded = true;
            OnLanding?.Invoke();
        }

        if (Input.GetKeyDown(KeyCode.Space) && grounded)
        {
            Jump(3f, 15f);
        }
    }

    public void Jump(float forwardForce, float upwardForce)
    {
        Vector3 jumpVector = transform.forward * forwardForce;
        jumpVector.y = upwardForce;

        rigidbody.AddForce(jumpVector, ForceMode.VelocityChange);

        grounded = false;

        StartCoroutine(RotateOnceRoutine(1f));

        OnJump?.Invoke();
    }

    // change to 3x3 array of raycasts
    public bool Grounded()
    {
        Debug.Log("Checkingn for ground...");

        Vector3 footCenter = transform.TransformPoint(footCollider.center);
        float rayLength = footCollider.size.y / 2 + skinLength;

        Ray ray = new Ray(footCenter, -transform.up);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, rayLength, footRayTargetMask))
            return true;

        return false;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        if(footCollider != null)
        {
            Vector3 footCenter = transform.TransformPoint(footCollider.center);
            Vector3 end = footCenter - Vector3.up * (footCollider.size.y / 2 + skinLength);
            Gizmos.DrawLine(footCenter, end);
        }   
    }

    IEnumerator RotateOnceRoutine(float rotationTime)
    {
        float deltaTime = 0;
        float frac = 0;

        Quaternion startingRotation = transform.rotation;
        Quaternion endingRotation = Quaternion.Euler(transform.rotation.eulerAngles + Vector3.up * 90f);

        while (deltaTime <= rotationTime)
        {
            deltaTime += Time.deltaTime;
            frac = Mathf.Clamp01(deltaTime / rotationTime);

            transform.rotation = Quaternion.Lerp(startingRotation, endingRotation, frac);

            yield return null;
        }
    }
}
