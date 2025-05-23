using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance {get; set;}

    public AudioSource dropItemSound;
    public AudioSource axeSwingSound;
    public AudioSource treeHitSound;
    public AudioSource craftItemSound;
    public AudioSource collectItemSound;
    public AudioSource eatFoodSound;
    public AudioSource drinkSound;



    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
    }

    public void PlaySound(AudioSource soundToPlay)
    {
        if (!soundToPlay.isPlaying)
        {
            soundToPlay.Play();
        }
    }

}
