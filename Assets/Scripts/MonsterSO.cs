using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
[CreateAssetMenu(fileName = "Monster", menuName ="New Monster")]
public class MonsterSO : ScriptableObject
{
    public enum MonsterType
    {
        Melee,
        Magic,
        Aerial
    }

    public MonsterType monsterType;
    public string monsterName;
    public int monsterHealth;
    public int monsterValue;
    public Sprite monsterImage;
    public int rageCounter;
    public AudioClip monsterNoise;
    public AudioClip monsterAttackSound;
}
