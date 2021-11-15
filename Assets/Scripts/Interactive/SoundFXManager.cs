using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundFXManager : MonoBehaviour
{
    public AudioSource source;
    public AudioClip shot;
    public AudioClip enemysDeath;
    public AudioClip dragonFire;
    public AudioClip dragonDeath;
    public AudioClip cubesAttack;
    public AudioClip cubesDeath;
    public AudioClip punch;
    public AudioClip playersDeath;
    public AudioClip money;
    public AudioClip water;
    public AudioClip beer;
    public AudioClip food;

    public static SoundFXManager sfxClone;

    private void Awake()
    {
        source = GetComponent<AudioSource>();
        if(sfxClone != null && sfxClone != this)
        {
            Destroy(this.gameObject);
            return;
        }

        sfxClone = this;
        DontDestroyOnLoad(this);
    }
}
