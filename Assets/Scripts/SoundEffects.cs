using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundEffects : MonoBehaviour {

    [SerializeField] AudioClip itemSFX;
    [SerializeField] AudioClip jumpSFX;
    [SerializeField] AudioClip plugSFX;
    [SerializeField] AudioClip checkpointSFX;
    [SerializeField] AudioClip shootingSFX;


    public void PlayItemSound() {
        //AudioSource.PlayOneShot(itemSFX);
    }


}
