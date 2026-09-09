using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ScaleUpAnim : MonoBehaviour
{

    private void Start()
    {
        ScaleUp();
    }

    void ScaleUp()
    {
        Vector2 targetScale = transform.localScale;
        transform.localScale = Vector2.zero;
        transform.DOScale(targetScale, 1).SetEase(Ease.OutBack);
    }
}
