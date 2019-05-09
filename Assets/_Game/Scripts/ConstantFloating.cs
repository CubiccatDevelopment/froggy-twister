using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DisallowMultipleComponent]
public class ConstantFloating : MonoBehaviour
{
    [SerializeField] float maxOffset = 0.5f;
    [SerializeField] float floatTime = 1f;

    Vector3 minPosition;
    Vector3 maxPosition;

    [SerializeField] bool movingUp = true;
    [SerializeField] bool randomDirection;

    private void Awake()
    {
        if(randomDirection)
            movingUp = Random.Range(0, 2) == 1;

        maxPosition = transform.localPosition + Vector3.up * maxOffset;
        minPosition = transform.localPosition - Vector3.up * maxOffset;

        StartFloatingRoutine();
    }

    private void StartFloatingRoutine()
    {
        StopAllCoroutines();
        if (movingUp)
        {
            movingUp = false;
            StartCoroutine(Bundle.LerpToLocalPositionRoutine(transform, minPosition, floatTime, null, StartFloatingRoutine));
        } else {
            movingUp = true;
            StartCoroutine(Bundle.LerpToLocalPositionRoutine(transform, maxPosition, floatTime, null, StartFloatingRoutine));
        }
            
    }
}
