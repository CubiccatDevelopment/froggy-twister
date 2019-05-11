using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public static class Bundle
{
    public static IEnumerator LerpToRotationRoutine(
        Transform t, Quaternion rotation, float routineTime,
        Action onRoutineStart = null, Action onRoutineEnd = null
        )
    {
        onRoutineStart?.Invoke();
        Quaternion startingRotation = t.rotation;
        float deltaTime = 0;
        float f = 0;
        while (deltaTime < routineTime)
        {
            deltaTime += Time.deltaTime;
            f = deltaTime / routineTime;
            t.rotation = Quaternion.Lerp(startingRotation, rotation, f);
            yield return null;
        }
        onRoutineEnd?.Invoke();
    }

    public static IEnumerator LerpToLocalRotationRoutine(
    Transform t, Quaternion localRotation, float routineTime,
    Action onRoutineStart = null, Action onRoutineEnd = null
    )
    {
        onRoutineStart?.Invoke();
        Quaternion startingRotation = t.localRotation;
        float deltaTime = 0;
        float f = 0;
        while (deltaTime < routineTime)
        {
            deltaTime += Time.deltaTime;
            f = deltaTime / routineTime;
            t.localRotation = Quaternion.Lerp(startingRotation, localRotation, f);
            yield return null;
        }
        onRoutineEnd?.Invoke();
    }

    public static IEnumerator LerpToPositionRoutine(
        Transform t, Vector3 position, float routineTime,
        Action onRoutineStart = null, Action onRoutineEnd = null, Action onEveryFrame = null)
    {
        onRoutineStart?.Invoke();
        Vector3 startingPosition = t.position;
        float deltaTime = 0;
        float f = 0;
        while (deltaTime < routineTime)
        {
            deltaTime += Time.deltaTime;
            f = deltaTime / routineTime;
            t.position = Vector3.Lerp(startingPosition, position, f);
            onEveryFrame?.Invoke();
            yield return null;
        }
        onRoutineEnd?.Invoke();
    }

    public static IEnumerator LerpToLocalPositionRoutine(
        Transform t, Vector3 localPosition, float routineTime,
        Action onRoutineStart = null, Action onRoutineEnd = null)
    {
        onRoutineStart?.Invoke();
        Vector3 startingPosition = t.localPosition;
        float deltaTime = 0;
        float f = 0;
        while (deltaTime < routineTime)
        {
            deltaTime += Time.deltaTime;
            f = deltaTime / routineTime;
            t.localPosition = Vector3.Lerp(startingPosition, localPosition, f);
            yield return null;
        }
        onRoutineEnd?.Invoke();
    }

    public static IEnumerator LerpToLocalScale(
    Transform t, Vector3 localScale, float routineTime,
    Action onRoutineStart = null, Action onRoutineEnd = null)
    {
        onRoutineStart?.Invoke();
        Vector3 startingScale = t.localScale;
        float deltaTime = 0;
        float f = 0;
        while (deltaTime < routineTime)
        {
            deltaTime += Time.deltaTime;
            f = deltaTime / routineTime;
            t.localScale = Vector3.Lerp(startingScale, localScale, f);
            yield return null;
        }
        onRoutineEnd?.Invoke();
    }

    public static int GetPreviousIndex(int index, int arrayLength)
    {
        int i = index;
        if (--i < 0)
            i = arrayLength - 1;
        return i;
    }

    public static int GetNextIndex(int index, int arrayLength)
    {
        int i = index;
        if (++i >= arrayLength)
            i = 0;
        return i;
    }
}
