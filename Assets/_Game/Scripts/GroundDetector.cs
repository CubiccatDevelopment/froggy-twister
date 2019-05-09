using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class GroundDetector : MonoBehaviour
{
    [SerializeField] new BoxCollider collider;
    [SerializeField] LayerMask detectionLayers;
    [Range(0.01f, 0.1f)]
    [SerializeField] float skinLength;
    [Range(3,20)] 
    [SerializeField] int raysCount;

    public bool Grounded()
    {
        int raysCount = 3;

        float raysStepX = collider.size.x / (raysCount - 1);
        float raysStepZ = collider.size.z / (raysCount - 1);

        float rayLength = collider.size.y / 2 + skinLength;

        Vector3 raysOrigin = collider.center;
        raysOrigin.x -= collider.size.x / 2;
        raysOrigin.z -= collider.size.z / 2;

        Vector3 rayStart;
        RaycastHit hit;

        for (int x = 0; x < raysCount; x++)
        {
            for (int z = 0; z < raysCount; z++)
            {
                rayStart.x = raysOrigin.x + x * raysStepX;
                rayStart.y = raysOrigin.y;
                rayStart.z = raysOrigin.z + z * raysStepZ;

                Ray ray = new Ray(transform.TransformPoint(rayStart), -transform.up);

                Debug.DrawLine(
                    transform.TransformPoint(rayStart),
                    transform.TransformPoint(rayStart) + Vector3.down * rayLength,
                    Color.red);

                if (Physics.Raycast(ray, out hit, rayLength, detectionLayers))
                    return true;
            }
        }

        return false;
    }
}
