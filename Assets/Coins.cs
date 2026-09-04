using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coins : MonoBehaviour
{
    private void FixedUpdate()
    {
        transform.Rotate(new Vector3(0,0,30) * Time.deltaTime);       
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent<Coins_Bag>(out Coins_Bag bags))
        {
            bags.AddsCoins();
            Destroy(gameObject);
        }
    }
}
