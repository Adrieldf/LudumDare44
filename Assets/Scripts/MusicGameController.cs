using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicGameController : MonoBehaviour
{
    public static MusicGameController Instance;
    private AudioSource Source;
    public AudioClip PowerUp, Battle;
    void Awake()
    {
        Instance = this;        
        Source = GetComponent<AudioSource>();
        Source.loop = true;
    }

    void Update(){
        if(Input.GetKeyDown(KeyCode.P)){
            PlayPowerUpMusic();
        }

        if(Input.GetKeyDown(KeyCode.B)){
            PlayBattleMusic();
        }
    }

    public void PlayPowerUpMusic(){
        Source.clip = PowerUp;
        Source.Play();
    }

    public void PlayBattleMusic(){
        Source.clip = Battle;
        Source.Play();
    }
}
