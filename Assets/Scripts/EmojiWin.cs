using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class EmojiWin : MonoBehaviour
{
    [SerializeField] private GameObject targetImage;
    [SerializeField] private Sprite winHappy;
    [SerializeField] private Sprite winSad;
    [SerializeField] private Health health;
    [SerializeField] private StartGame startGame;

    private void Start()
    {
        targetImage.transform.localScale = Vector3.zero;
    }

    private void OnEnable()
    {
        health.GetDamagedEvent += GetSadEmoji;
        startGame.OnClickEvent += GetStartEmoji;
    }

    private void OnDisable()
    {
        health.GetDamagedEvent -= GetSadEmoji;
        startGame.OnClickEvent -= GetStartEmoji;
    }

    void GetStartEmoji()
    {
        SetEmoji(winHappy);
        SoundManager.instance.letGoSound();
    }

    private void GetSadEmoji()
    {
        SetEmoji(winSad);
    }

    void SetEmoji(Sprite emoji)
    {
        SoundManager.instance.PopSound();

        GameObject ob = Instantiate(targetImage, gameObject.transform);
        ob.SetActive(gameObject);
        Image _image = ob.GetComponent<Image>();
        _image.sprite = emoji;

        ob.transform.DOScale(0.7f, 0.5f).SetEase(Ease.OutBack);
        ob.transform.DOScale(0f, 0.5f).SetEase(Ease.OutBack).SetDelay(1.5f);
        Destroy(ob, 2f);
    }
}
