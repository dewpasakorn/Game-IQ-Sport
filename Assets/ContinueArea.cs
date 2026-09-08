using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ContinueArea : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private float delay = 2;
    public System.Action OnClickEvent;
    private bool isDelay = true;

    private void Start()
    {
        SoundManager.instance.EarnCoin();
    }

    private void Update()
    {
        if (isDelay)
        {
            delay = Mathf.Max(delay -= Time.deltaTime, 0);
            if (delay == 0)
            {
                isDelay = false;
            }
        }
    }

    public void OnPointerClick(PointerEventData pointerEventData)
    {
        if (!isDelay) { return; }

        SoundManager.instance.ButtonSound();
        OnClickEvent?.Invoke();
    }
}
