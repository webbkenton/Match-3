using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AbilityHolder : MonoBehaviour
{
    public AbilitySO abilitySO;
    public BattleManger battleManager;

    private void Start()
    {
        battleManager = GameObject.FindGameObjectWithTag("BattleManager").GetComponent<BattleManger>();
    }
    private void Update()
    {
        AbilityColor();
    }

    private void AbilityColor()
    {
        if (battleManager.playerAbilityBar.value >= abilitySO.abilityCost)
        {
            this.gameObject.GetComponent<Image>().color = new Color(1f, 1f, 1f, 1f);
            //ability1.color = new Color(1f, 1f, 1f, 1f);
            //ability2.color = new Color(1f, 1f, 1f, 1f);
            //ability3.color = new Color(1f, 1f, 1f, 1f);
        }
        else
        {
            this.gameObject.GetComponent<Image>().color = new Color(.5f, .5f, .5f, 1f);
            //ability1.color = new Color(.5f, .5f, .5f, 1f);
            //ability2.color = new Color(.5f, .5f, .5f, 1f);
            //ability3.color = new Color(.5f, .5f, .5f, 1f);
        }

    }

    public void RageAbility()
    {
        if (abilitySO.abilityType == AbilitySO.AbilityType.Rage)
        {
            Debug.Log("Rage");
            battleManager.UseRageAbility();
        }
    }
    public void HealAbility()
    {
        if (abilitySO.abilityType == AbilitySO.AbilityType.Heal)
        {
            Debug.Log("Heal");
            battleManager.UseHealAbility();
        }
    }
    public void SacrificeAbility()
    {
        Debug.Log("Sac");
        if (abilitySO.abilityType == AbilitySO.AbilityType.Sacrifice)
        {
            battleManager.UseSacrificeAbility();
        }
    }
    public void UseAbility()
    {
        if (abilitySO.abilityType == AbilitySO.AbilityType.Rage && abilitySO.abilityCost <= battleManager.playerAbilityBar.value)
        {
            RageAbility();
            battleManager.playerAbilityBar.value -= abilitySO.abilityCost;
        }
        else if (abilitySO.abilityType == AbilitySO.AbilityType.Heal && abilitySO.abilityCost <= battleManager.playerAbilityBar.value)
        {
            HealAbility();
            battleManager.playerAbilityBar.value -= abilitySO.abilityCost;
        }
        else if (abilitySO.abilityType == AbilitySO.AbilityType.Sacrifice && abilitySO.abilityCost <= battleManager.playerAbilityBar.value)
        {
            SacrificeAbility();
            battleManager.playerAbilityBar.value -= abilitySO.abilityCost;
        }
    }
}
