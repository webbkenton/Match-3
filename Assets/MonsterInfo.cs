using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterInfo : MonoBehaviour
{
    public MonsterSO monster;
    void Start()
    {
        DontDestroyOnLoad(this.gameObject);
    }
}
