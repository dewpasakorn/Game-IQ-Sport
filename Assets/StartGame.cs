using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class StartGame : MonoBehaviour, IPointerClickHandler
{
    public System.Action OnClickEvent;
    public Animator anim;
    public void OnPointerClick(PointerEventData pointerEventData)
    {
        SoundManager.instance.StartGame();
        OnClickEvent?.Invoke();
        gameObject.SetActive(false);
        anim.SetTrigger("Start");
    }
}
