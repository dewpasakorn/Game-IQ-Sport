using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class StartGame : MonoBehaviour, IPointerClickHandler
{
    public System.Action OnClickEvent;
    public void OnPointerClick(PointerEventData pointerEventData)
    {
        OnClickEvent?.Invoke();
        gameObject.SetActive(false);
    }
}
