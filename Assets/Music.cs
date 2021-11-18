using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Music : MonoBehaviour
{
    int counter = 0;

    private AudioSource _audioSource;
    private void Awake()
     {
         DontDestroyOnLoad(transform.gameObject);
         _audioSource = GetComponent<AudioSource>();

         if (counter == 0) PlayMusic();
         counter += 1;
     }

     public void PlayMusic()
     {
         if (counter == 0) _audioSource.Play();
         if (counter == 1) StopMusic();
     }

     public void StopMusic()
     {
         _audioSource.Stop();
     }
}
