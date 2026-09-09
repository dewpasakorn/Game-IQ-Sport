using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using DG.Tweening;

public class Coins_Bag : MonoBehaviour
{
    [SerializeField] private int currentCoins;
    [SerializeField] private TMP_Text coinText;

    public System.Action getCoinEvent;

    public void AddsCoins()
    {
        getCoinEvent?.Invoke();
        SoundManager.instance.PlayerGetCoin();
        currentCoins += 1;
        coinText.text = currentCoins.ToString();
    }

    public int GetCurrentCoins()
    {
        return currentCoins;
    }
}
