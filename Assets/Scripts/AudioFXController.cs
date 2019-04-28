using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioFXController : MonoBehaviour
{
    public static AudioFXController Instance;

    private AudioSource Source;

    public AudioClip Attack, Death;


    void Awake(){
        Instance = this;
        Source = GetComponent<AudioSource>();
        Source.loop = false;
    }

    public void PlayAttackFx(){
        Source.clip = Attack;
        Source.Play();
    }

    public void PlayDeathFx(){
        Source.clip = Death;
        Source.Play();
    }

}
