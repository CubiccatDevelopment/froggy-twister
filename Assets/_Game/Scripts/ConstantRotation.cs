using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConstantRotation : MonoBehaviour
{
    [SerializeField] Vector3 rotation;
    [SerializeField] bool randomDirection;

    Vector3 randomDirections;

    private void Awake()
    {
        if(randomDirection)
        {
            rotation.x *= Random.Range(0, 2) == 1 ? 1 : -1;
            rotation.y *= Random.Range(0, 2) == 1 ? 1 : -1;
            rotation.z *= Random.Range(0, 2) == 1 ? 1 : -1;
        } 
    }

    void Update()
    {
        transform.Rotate(rotation  * Time.deltaTime);
    }
}
