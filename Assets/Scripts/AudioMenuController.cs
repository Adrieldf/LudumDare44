using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioMenuController : MonoBehaviour
{
    public static AudioMenuController Instance;
    private AudioSource Audio;
    public AudioClip ClickSound;

    private void Awake(){
        Instance = this;
        Audio = GetComponent<AudioSource>();
    }

    public void PlayClickSound(){
        Audio.loop = false;
        Audio.clip = ClickSound;
        Audio.Play();
    }

    IEnumerator StopSound()
    {
        yield return new WaitForSeconds(0.5f);
        Audio.Stop();
    }
}
