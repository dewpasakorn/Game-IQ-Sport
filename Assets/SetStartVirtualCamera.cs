using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class SetStartVirtualCamera : MonoBehaviour
{
    private CinemachineVirtualCamera cmVir;
    private CinemachineTransposer transposer;
    [SerializeField] private StartGame startGame;

    void OnEnable()
    {
        startGame.OnClickEvent += ResetCam;
    }

    void OnDisable()
    {
        startGame.OnClickEvent -= ResetCam;
    }

    private void ResetCam()
    {
        StartCoroutine(SlowRoutine());
    }

    void Start()
    {
        cmVir = GetComponent<CinemachineVirtualCamera>();
        transposer = cmVir.GetCinemachineComponent<CinemachineTransposer>();
    }

    private IEnumerator SlowRoutine()
    {
        float duration = 1f;
        float startZ = transposer.m_FollowOffset.z;
        float targetZ = 2.5f; // desired end offset

        float elapsed = 0f;
        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            float t = Mathf.Clamp01(elapsed / duration);
            // optional: ease it instead of linear
            // t = Mathf.SmoothStep(0f, 1f, t);

            Vector3 offset = transposer.m_FollowOffset;
            offset.z = Mathf.Lerp(startZ, targetZ, t);
            transposer.m_FollowOffset = offset;

            yield return null;
        }

        // ensure it lands exactly on target
        Vector3 finalOffset = transposer.m_FollowOffset;
        finalOffset.z = targetZ;
        transposer.m_FollowOffset = finalOffset;
    }
}
