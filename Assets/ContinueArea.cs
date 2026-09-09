using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ContinueArea : MonoBehaviour, IPointerClickHandler
{
    public System.Action OnClickEvent;

    private void Start()
    {
        SoundManager.instance.EarnCoin();
    }

    private void Update()
    {

    }

    public void OnPointerClick(PointerEventData pointerEventData)
    {

        SoundManager.instance.ButtonSound();
        OnClickEvent?.Invoke();
    }
}
