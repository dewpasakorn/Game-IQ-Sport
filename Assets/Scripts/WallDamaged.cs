using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class WallDamaged : MonoBehaviour
{
    [SerializeField] private int damaged = 1;
    private CinemachineImpulseSource impulseSource;

    void Start()
    {
        impulseSource = GetComponent<CinemachineImpulseSource>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent<Health>(out Health _health))
        {
            _health.GetDamaged(damaged);
            CameraShakeManager.instance.CameraShake(impulseSource);
        }
    }
}
