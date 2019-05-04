using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TEST_WorldCotroller : MonoBehaviour
{
    public bool isGoingDown;
    public float speed;

    public Transform[] islands;

    public KeyCode setLastIslandPositionKey;

    private int lastIslandIndex = 3;

    bool canAct = true;

    private void Update()
    {
        if (Input.GetKeyDown(setLastIslandPositionKey) && canAct)
        {
            canAct = false;
            RandomizeLastHeight();
            RotateOnce();
            SetLastIslandIndex(Bundle.GetNextIndex(lastIslandIndex, islands.Length));
        }
            
        if(isGoingDown)
        {
            foreach (Transform island in islands)
            {
                island.position -= Time.deltaTime * speed * Vector3.down;
            }
        }
            
    }



    private void RandomizeLastHeight()
    {
        Vector3 beforeLastPosition =
            islands[Bundle.GetPreviousIndex(lastIslandIndex, islands.Length)].localPosition;
        Vector3 
            lastPosition = islands[lastIslandIndex].localPosition;
        lastPosition.y = beforeLastPosition.y + Random.Range(0.1f, 0.5f);
        StartCoroutine(Bundle.LerpToLocalPositionRoutine(islands[lastIslandIndex], lastPosition, 0.75f));
    }

    private void SetLastIslandIndex(int index)
    {
        lastIslandIndex = index;
    }

    public void RotateOnce()
    {
        Vector3 rotation = transform.eulerAngles;
        rotation.y = rotation.y + 90f;
        StartCoroutine(
            Bundle.LerpToRotationRoutine(transform, Quaternion.Euler(rotation), 0.75f, null, () => canAct = true));
    }

    
}
