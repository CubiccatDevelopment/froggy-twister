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
}
