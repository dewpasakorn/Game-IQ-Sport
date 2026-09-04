using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LeaderStats : MonoBehaviour
{
    [SerializeField] private TMP_Text number, userName, coins;

    public void SetBoard(string _number,string _userName,string _coins)
    {
        number.text = _number;
        userName.text = _userName;
        coins.text = _coins;
    }
}
