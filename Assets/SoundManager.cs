using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;
    private AudioSource audioSource;
    [SerializeField] private AudioClip coin;
    [SerializeField] private AudioClip startGame;
    [SerializeField] private AudioClip damaged;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
    }

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void PlayerGetCoin()
    {
        audioSource.PlayOneShot(coin);
    }

    public void StartGame()
    {
        audioSource.PlayOneShot(startGame);
    }

    public void PlayerHurt()
    {
        audioSource.PlayOneShot(damaged);
    }
}
