using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterInfo : MonoBehaviour
{
    public MonsterSO monster;
    public List<AbilitySO> monsterAbilties = new List<AbilitySO>();

    private void Update()
    {
        if (monster.isDefeated)
        {
            Destroy(this.gameObject);
        }
    }
    void Start()
    {
       //DontDestroyOnLoad(this.gameObject);
    }
}
