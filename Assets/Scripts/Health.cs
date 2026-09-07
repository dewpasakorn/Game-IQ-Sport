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
    [SerializeField] private float _IframeCooldown = 1;
    private float _IframeCooldownMax;
    private bool isIframe;

    private void Awake()
    {
        maximumHealth = currentHealth;
        _IframeCooldownMax = _IframeCooldown;
    }

    public int GetCurrentHealth() => currentHealth;
    public int GetMaximumHealth() => maximumHealth;

    void Update()
    {
        if (isIframe)
        {
            _IframeCooldownMax = Mathf.Max(0, _IframeCooldownMax -= Time.deltaTime);
            if (_IframeCooldownMax == 0)
            {
                isIframe = false;
                _IframeCooldownMax = _IframeCooldown;
            }
        }
    }

    public void GetDamaged(int dmg)
    {
        if (isIframe)
        {
            return;
        }

        isIframe = true;
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