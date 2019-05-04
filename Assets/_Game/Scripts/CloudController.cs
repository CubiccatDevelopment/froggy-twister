using UnityEngine;

public class CloudController : MonoBehaviour
{
    [HideInInspector] public float speed;

    private void FixedUpdate()
    {
        transform.Translate(Vector3.right * Time.deltaTime * speed);
    }
}
