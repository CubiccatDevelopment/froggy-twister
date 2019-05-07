using System.Collections;
using UnityEngine;

public class CameraPivotController : MonoBehaviour
{
    [SerializeField] float rotationTime;

    public void RotateOnce()
    {
        StartCoroutine(Bundle.LerpToRotationRoutine(
            transform, Quaternion.Euler(transform.rotation.eulerAngles + Vector3.up * 90f), rotationTime));
    }

    public void Reset()
    {
        StopAllCoroutines();

        StartCoroutine(Bundle.LerpToRotationRoutine(
            transform, Quaternion.identity, rotationTime));

        StartCoroutine(Bundle.LerpToPositionRoutine(
            transform, Vector3.zero, rotationTime));
    }

    public void SetHeight(float y)
    {
        Vector3 newPosition = transform.position;
        newPosition.y = y;
        transform.position = newPosition;
    }
}
