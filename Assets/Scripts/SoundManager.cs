using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public FindMatches findMatches;
    public Board board;
    public AudioSource[] destoryNoise;
    public AudioSource slash;
    public AudioSource heavySlash;
    public AudioSource magic;
    public AudioSource gems;
    public AudioSource potions;
    public AudioSource defend;
    public AudioSource abilitySound;

    private void Start()
    {
        findMatches = FindObjectOfType<FindMatches>();
        board = FindObjectOfType<Board>();
    }
    public void PlayDestroyNoise()
    {
        //Chose a random Number
        int ClipToPlay = Random.Range(0, destoryNoise.Length);
        //Play Clip Equal to that number
        destoryNoise[ClipToPlay].Play();
    }
    public void PlaySlash()
    {
        slash.Play();
    }
    public void PlayGem()
    {
        gems.Play();
    }
    public void PlayPotions()
    {
        potions.Play();
    }
    public void PlayHeavy()
    {
        heavySlash.Play();
    }
    public void PlayMagic()
    {
        magic.Play();
    }
    public void PlayDefend()
    {
        defend.Play();
    }
}
