using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    public AudioSource BackgroundMusic;
    void Start()
    {
        BackgroundMusic.Play();
    }
}
