using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterInfo : MonoBehaviour
{
    public MonsterSO monster;

    private void Update()
    {
        if (monster.isDefeated)
        {
            Destroy(this.gameObject);
        }
    }
    void Start()
    {
        DontDestroyOnLoad(this.gameObject);
    }
}
