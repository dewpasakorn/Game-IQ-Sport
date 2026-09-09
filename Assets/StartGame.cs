using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using DG.Tweening;
using Unity.UI;

public class StartGame : MonoBehaviour, IPointerClickHandler
{
    public System.Action OnClickEvent;
    public Animator anim;

    public void OnPointerClick(PointerEventData pointerEventData)
    {
        SoundManager.instance.ButtonSound();
        SoundManager.instance.StartGame();
        OnClickEvent?.Invoke();
        anim.SetTrigger("Start");

        CanvasGroup go = gameObject.GetComponent<CanvasGroup>();

        DeActive();
    }

    void DeActive()
    {
        gameObject.SetActive(false);
    }
}
