using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSoundManager : MonoBehaviour
{
    public static PlayerSoundManager instance;
    private AudioSource sound;

    [SerializeField] private AudioClip[] walks;
    [SerializeField] private AudioClip[] jumps;
    [SerializeField] private AudioClip[] hurts;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
    }

    private void Start()
    {
        sound = GetComponent<AudioSource>();
    }

    public void WalkSound()
    {
        int randomN = Random.Range(0, walks.Length);
        sound.PlayOneShot(walks[randomN]);
    }

    public void JumpSound()
    {
        int randomN = Random.Range(0, jumps.Length);
        sound.PlayOneShot(jumps[randomN]);
    }

    public void HurtSound()
    {
        int randomN = Random.Range(0, hurts.Length);
        sound.PlayOneShot(hurts[randomN]);
    }
}
