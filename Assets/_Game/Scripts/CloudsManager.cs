using System.Collections;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class CloudsManager : MonoBehaviour
{
    [SerializeField] Vector2 cloudsSpeeds;

    CloudController[] clouds;
    BoxCollider boxCollider;

    private void Awake()
    {
        clouds = GetComponentsInChildren<CloudController>();
        boxCollider = GetComponent<BoxCollider>();
    }

    private void Start()
    {
        foreach (CloudController cloud in clouds)
            cloud.speed = Random.Range(cloudsSpeeds.x, cloudsSpeeds.y);

        StartCoroutine(UpdateCloudsPositionRoutine());
    }

    IEnumerator UpdateCloudsPositionRoutine()
    {
        yield return new WaitForSeconds(0.25f);

        for (int i = 0; i < clouds.Length; i++)
        {
            if(clouds[i].transform.localPosition.x >= boxCollider.size.x / 2)
            {
                Vector3 newPosition = clouds[i].transform.localPosition;
                newPosition.x = -boxCollider.size.x / 2;
                clouds[i].transform.localPosition = newPosition;
            }
        }

        yield return UpdateCloudsPositionRoutine();
    }

    private void OnDrawGizmos()
    {
        if(boxCollider == null)
            boxCollider = GetComponent<BoxCollider>();

        Gizmos.matrix = transform.localToWorldMatrix;
        Gizmos.DrawLine(Vector3.left * (boxCollider.size.x / 2), Vector3.right * (boxCollider.size.x / 2));
    }
}
