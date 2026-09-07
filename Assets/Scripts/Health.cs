using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    [SerializeField] private int currentHealth;
    private int maximumHealth;

    public System.Action GetDamagedEvent;
    [Header("Flash Settings")]
    [SerializeField] private TheEnd theEnd;
    [SerializeField] private Animator hitEffect;

    private void Awake()
    {
        maximumHealth = currentHealth;
    }

    public int GetCurrentHealth() => currentHealth;
    public int GetMaximumHealth() => maximumHealth;

    public void GetDamaged(int dmg)
    {
        PlayerSoundManager.instance.HurtSound();
        SoundManager.instance.PlayerHurt();
        hitEffect.SetTrigger("Hit");
        currentHealth = Mathf.Max(0, currentHealth - dmg);
        GetDamagedEvent?.Invoke();

        if (currentHealth == 0)
        {
            theEnd.gameObject.SetActive(true);
            Destroy(gameObject);
        }
    }

}