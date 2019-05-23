using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PowerButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public void OnPointerDown(PointerEventData eventData)
    {
        InputManager.Instance.StartPowerRoutine();
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        InputManager.Instance.EndPowerRoutine();
    }
}
