using UnityEngine;
using System.Collections;
using System;

public class InputManager : MonoBehaviour
{
    public event Action OnLoadPower;
    public event Action<float> OnPowerRelease;

    [SerializeField] float powerPerSec;

    float currentPowerPercent = 0;
    Coroutine powerRoutine;

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            //Debug.Log("Frog launch initialized.");

            powerRoutine = StartCoroutine(PowerRoutine());

            OnLoadPower?.Invoke();
        } else if(Input.GetKeyUp(KeyCode.Space))
        {
            //Debug.Log("Launched frog with " + currentPowerPercent * 100 + "% of force");

            if (powerRoutine != null)
                StopCoroutine(powerRoutine);

            OnPowerRelease?.Invoke(0.61f);
        }
    }

    IEnumerator PowerRoutine()
    {
        currentPowerPercent = 0;
        while (currentPowerPercent < 1)
        {
            currentPowerPercent += powerPerSec * Time.deltaTime;
            currentPowerPercent = Mathf.Clamp01(currentPowerPercent);
            yield return null;
        }
    }
}
