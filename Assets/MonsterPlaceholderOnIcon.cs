using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterPlaceholderOnIcon : MonoBehaviour
{
    public GameObject randomMonster;
    private int randomMonsterNumber;
    public MonsterSO monsterSO;
    void Start()
    {
        randomMonsterNumber = Random.Range(0, GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>().level1Monsters.Length);
        randomMonster = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>().level1Monsters[randomMonsterNumber];
        monsterSO = randomMonster.GetComponent<MonsterInfo>().monster;
    }
}
