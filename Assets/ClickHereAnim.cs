using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickHereAnim : MonoBehaviour
{
    public RectTransform canvas;

    private void Start()
    {
        canvas = gameObject.GetComponent<RectTransform>();

        canvas.transform.DOScale(new Vector2(0.8f,0.8f),1f).SetLoops(-1,LoopType.Yoyo);
    }
}
