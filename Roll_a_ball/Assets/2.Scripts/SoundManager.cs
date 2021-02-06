using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    AudioSource audio;

    void Awake() {
        audio = GetComponent<AudioSource>();
    }

    public void playItemSound() {
        audio.Play();
    }

}
