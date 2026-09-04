using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private Health health;
    [SerializeField] private GameObject healthImage;
    private List<GameObject> healthInside = new List<GameObject>();

    private void Start()
    {
        for(int i = 0; i< health.GetMaximumHealth();i++)
        {
            GameObject _health = Instantiate(healthImage,transform);
            healthInside.Add(_health.transform.Find("Health_Dot").GetComponent<Image>().gameObject);
        }
    }


    private void OnEnable()
    {
        health.GetDamagedEvent += RefreshHealth;
    }

    private void OnDisable()
    {
        health.GetDamagedEvent -= RefreshHealth;
    }

    private void RefreshHealth()
    {
            foreach(GameObject _health in healthInside)
            {
            _health.SetActive(false);
            }
        for (int i = 0; i < health.GetCurrentHealth(); i++)
        {
            healthInside[i].SetActive(true);
        }
    }
}
