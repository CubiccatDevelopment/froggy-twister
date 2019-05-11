using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class IslandsManager : MonoBehaviour
{
    public event Action OnIslandMove;

    IslandController[] islands;
    int currentIndex = 0;
    [SerializeField] float maxDifference;

    private void Awake()
    {
        islands = GetComponentsInChildren<IslandController>();
    }

    public void SetPreviousIslandRandomPosition()
    {
        currentIndex = currentIndex + 1 >= islands.Length ? 0 : currentIndex + 1;

        int lastIndex = GetPreviousIslandIndex(currentIndex);
        int beforeLastIndex = GetPreviousIslandIndex(lastIndex);

        Vector3 newPosition = islands[lastIndex].transform.position;
        newPosition.y = islands[beforeLastIndex].transform.position.y + UnityEngine.Random.Range(-maxDifference, maxDifference);

        StartCoroutine(Bundle.LerpToPositionRoutine(
            islands[lastIndex].transform, newPosition, 0.5f, null, null, OnIslandMove));

        RandomDiamondActivation(lastIndex);
    }

    public void RandomDiamondActivation(int islandIndex)
    {
        if (UnityEngine.Random.Range(0f, 1f) > 0.5f)
        {
            Debug.Log("Activating diamond.");
            islands[islandIndex].ActivateDiamond();
        }
    }

    public float GetAverageHeight()
    {
        float avg = 0;
        for (int i = 0; i < islands.Length; i++)
        {
            avg += islands[i].transform.position.y;
        }

        if (avg == 0)
            return 0;

        return avg / islands.Length;
    }

    private int GetPreviousIslandIndex(int index)
    {
        if (index - 1 >= 0)
            return index - 1;
        return islands.Length - 1;
    }

    public IslandController GetStartingIsland()
    {
        return islands[0];
    }

    public void Reset()
    {
        currentIndex = 0;
        StopAllCoroutines();
        Vector3 newPosition;
        for (int i = 0; i < islands.Length; i++)
        {
            newPosition = islands[i].transform.position;
            newPosition.y = 0;
            StartCoroutine(Bundle.LerpToPositionRoutine(islands[i].transform, newPosition, 1f));

            islands[i].GetDiamond().Reset();
        }
    }
}
