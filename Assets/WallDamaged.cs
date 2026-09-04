using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallDamaged : MonoBehaviour
{
    [SerializeField] private int damaged = 1;
    private void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent<Health>(out Health _health))
        {
            _health.GetDamaged(damaged);
        }
    }
}
