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
    [SerializeField] private Renderer targetRenderer; // MeshRenderer หรือ SkinnedMeshRenderer
    [SerializeField] private Color flashColor = Color.red;
    [SerializeField] private float flashDuration = 0.1f;
    [SerializeField] private int flashCount = 2;
    [SerializeField] private TheEnd theEnd;

    private Color originalColor;
    private Material matInstance;

    private void Awake()
    {
        maximumHealth = currentHealth;

        if (targetRenderer != null)
        {
            matInstance = targetRenderer.material; // สร้าง material instance
            originalColor = matInstance.color;
        }
    }

    public int GetCurrentHealth() => currentHealth;
    public int GetMaximumHealth() => maximumHealth;

    public void GetDamaged(int dmg)
    {
        currentHealth = Mathf.Max(0, currentHealth - dmg);
        GetDamagedEvent?.Invoke();

        StartCoroutine(FlashMaterial());

        if (currentHealth == 0)
        {
            theEnd.gameObject.SetActive(true);
            Destroy(gameObject);
        }
    }

    private IEnumerator FlashMaterial()
    {
        if (matInstance == null) yield break;

        for (int i = 0; i < flashCount; i++)
        {
            matInstance.color = flashColor;
            yield return new WaitForSeconds(flashDuration);
            matInstance.color = originalColor;
            yield return new WaitForSeconds(flashDuration);
        }
    }
}