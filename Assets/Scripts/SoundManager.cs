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
    [SerializeField] private AudioClip button;
    [SerializeField] private AudioClip gameOver;
    [SerializeField] private AudioClip earnCoin;
    [SerializeField] private AudioClip leaderboard;
    [SerializeField] private AudioClip popSound;
    [SerializeField] private AudioClip letgo;

    private void Awake()
    {
        if (instance == null)
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

    public void ButtonSound()
    {
        audioSource.PlayOneShot(button);
    }

    public void GameOverSound()
    {
        audioSource.PlayOneShot(gameOver);
    }

    public void EarnCoin()
    {
        audioSource.PlayOneShot(earnCoin);
    }

    public void Leaderboard()
    {
        audioSource.PlayOneShot(leaderboard);
    }

    public void PopSound()
    {
        audioSource.PlayOneShot(popSound);
    }

    public void letGoSound()
    {
        audioSource.PlayOneShot(letgo);
    }
}
