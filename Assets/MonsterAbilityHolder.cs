using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterAbilityHolder : MonoBehaviour
{
    public AbilitySO ability;
    private BattleManger battleManager;

    private void Start()
    {
        battleManager = GameObject.FindGameObjectWithTag("BattleManager").GetComponent<BattleManger>();
    }

    public void SetAbilityDescription()
    {
        battleManager.selectedMonsterAbility = ability;
        battleManager.DisplayMonsterAbility();
    }
}
