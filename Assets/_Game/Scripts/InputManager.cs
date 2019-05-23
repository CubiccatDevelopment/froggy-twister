using UnityEngine;
using System.Collections;
using System;
using UnityEngine.UI;

public class InputManager : MonoBehaviour
{
    public static InputManager Instance;
    void Singletone()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
        DontDestroyOnLoad(gameObject);
    }

    public event Action OnLoadPower;
    public event Action<float> OnPowerRelease;
    public event Action OnReset;

    [SerializeField] float powerPerSec;

    float currentPowerPercent = 0;
    Coroutine powerRoutine;

    private void Awake()
    {
        Singletone();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
            InvokeReset();
    }

    public void InvokeReset()
    {
        OnReset?.Invoke();
    }

    public void StartPowerRoutine()
    {
        powerRoutine = StartCoroutine(PowerRoutine());
        OnLoadPower?.Invoke();
    }

    public void EndPowerRoutine()
    {
        if (powerRoutine != null)
            StopCoroutine(powerRoutine);

        OnPowerRelease?.Invoke(currentPowerPercent);
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
