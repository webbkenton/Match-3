using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AbilityHolder : MonoBehaviour
{
    public AbilitySO abilitySO;
    public BattleManger battleManager;
    public Board board;
    public int currentCount;
    public int currentCount2;
    public int currentCount3;
    public int currentRemainder;
    public Text abilityDescription;
    public GameObject abilityReady;
    public Text abilityReadyText;
    private bool coolDown;

    private bool ultimateHealEffect;
    private bool penUltimateRageEffect;
    private bool penUltimateHealEffect;
    private bool lesserHealEffect;
    private bool lesserSacEffect;
    private bool greaterSacEffect;
    private bool ultimateSacEffect;



    private int baseValue;
    private int endTurn;
    private int lesserHealEndTurn;
    private int penUltimateHealEndTurn;
    private int ultimateHealEndTurn;
    private int lesserSacEndTurn;
    private int greaterSacEndTurn;
    private int ultimateSacEndTurn;

    Ray ray;
    RaycastHit hit;

    private void Start()
    {
        battleManager = GameObject.FindGameObjectWithTag("BattleManager").GetComponent<BattleManger>();
        board = GameObject.FindGameObjectWithTag("Board").GetComponent<Board>();
        baseValue = board.baseTileValue;
        
    }

    private void FixAbility()
    {
        if (abilitySO.abilityDescription != abilityDescription.text)
        {
            abilityDescription.text = abilitySO.abilityDescription;
            this.GetComponent<Image>().sprite = abilitySO.abilityIcon;
        }
    }


    private void OnMouseOver()
    {
        Debug.Log("MouseOver");
        abilityDescription.gameObject.SetActive(true);

    }
    private void OnMouseExit()
    {
        Debug.Log("MouseOff");
        abilityDescription.gameObject.SetActive(false);
    }
    private void Update()
    {
        //AbilityColor();
        //OffCD();
        FixAbility();
        UltimateHealEffect();
        PenUltimateRageEffect();
        PenUltimateHealEffect();
        LesserHealEffect();
        LesserSacEffect();
        GreaterSacEffect();
        UltimateSacEffect();
        
    }

    private void AbilityColor()
    {
        if (battleManager.playerAbilityBar.value >= abilitySO.abilityCost) //&& abilitySO.cooldown == false)
        {
            this.gameObject.GetComponent<Image>().color = new Color(1f, 1f, 1f, 1f);
        }
        else
        {
            this.gameObject.GetComponent<Image>().color = new Color(.5f, .5f, .5f, 1f);
        }

    }
    private void AbilityCooldown()
    {
        abilitySO.coolDownStartTurn = battleManager.turnCounter;
    }

    /*&IEnumerator ReadyText()
    {
        abilityReady.SetActive(true);
        abilityReadyText.text = abilitySO.abilityName + " Is Ready";
        yield return new WaitForSeconds(2f);
        abilityReady.SetActive(false);
    }*/    

    //private void offcd()
    //{
    //    if (battlemanager.turncounter >= abilityso.cooldownstartturn + abilityso.cooldowntime)
    //    {
    //        this.getcomponent<button>().interactable = true;
    //        currentremainder = 0;
    //        currentcount = 0;
    //    }
    //}

    private void CleanUpAbility()
    {
        this.GetComponent<Button>().interactable = false;
        AbilityCooldown();
        board.currentState = GameState.move;
        if (battleManager.monsterHealth <= 0)
        {
            battleManager.Victory();
        }
    }
    
   


    public void LesserRageAbility()
    {
        battleManager.monsterHealth -= 10;
        CleanUpAbility();
    }
    
    public void RageAbility()
    {
        //Next Heavy Attack Will Deal Double Damage;
        battleManager.rageEffect = true;
        CleanUpAbility();
    }
    public void GreaterRageAbility()
    {
        battleManager.monsterHealth -= 20;
        CleanUpAbility();
    }
    public void PenUltimateRageAbility()
    {
        board.baseTileValue += 20;
        penUltimateRageEffect = true;
        endTurn = battleManager.turnCounter + 10;
        CleanUpAbility();
    }
    private void PenUltimateRageEffect()
    {
        if (penUltimateRageEffect == true)
        {
            if (battleManager.turnCounter >= endTurn)
            {
                penUltimateRageEffect = false;
                board.baseTileValue = baseValue;
            }
        }
    }
    public void UltimateRageAbility()
    {
        battleManager.UseRageAbility();
        CleanUpAbility();
    }
    public void UseAbility()
    {
        if (abilitySO.abilityFamily == AbilitySO.AbilityFamily.Rage && abilitySO.abilityCost <= battleManager.playerAbilityBar.value )//&& abilitySO.coolDownTime >= TurnCounter
        {
            if (abilitySO.abilityType == AbilitySO.AbilityType.lesserRage)
            {
                LesserRageAbility();
            }
            if (abilitySO.abilityType == AbilitySO.AbilityType.Rage)
            {
                RageAbility();
            }
            if (abilitySO.abilityType == AbilitySO.AbilityType.GreaterRage)
            {
                GreaterRageAbility();
            }
            if (abilitySO.abilityType == AbilitySO.AbilityType.PenUltimateRage)
            {
                PenUltimateRageAbility();
            }
            if (abilitySO.abilityType == AbilitySO.AbilityType.UltimateRage)
            {
                UltimateRageAbility();
            }
            PersistantData.data.mana -= abilitySO.abilityCost;
        }
        else if (abilitySO.abilityFamily == AbilitySO.AbilityFamily.Heal && abilitySO.abilityCost <= battleManager.playerAbilityBar.value)
        {
            if (abilitySO.abilityType == AbilitySO.AbilityType.lesserHeal)
            {
                LesserHealAbility();
            }
            if (abilitySO.abilityType == AbilitySO.AbilityType.Heal)
            {
                HealAbility();
            }
            if (abilitySO.abilityType == AbilitySO.AbilityType.GreaterHeal)
            {
                GreaterHealAbility();
            }
            if (abilitySO.abilityType == AbilitySO.AbilityType.PenUltimateHeal)
            {
                PenUltimateHealAbility();
            }
            if (abilitySO.abilityType == AbilitySO.AbilityType.UltimateHeal)
            {
                UltimateHealAbility();
            }
            //HealAbility();
            PersistantData.data.mana -= abilitySO.abilityCost;
            //battleManager.playerHealthBar.value += abilitySO.healValue;
        }
        else if (abilitySO.abilityFamily == AbilitySO.AbilityFamily.Sac && abilitySO.abilityCost <= battleManager.playerAbilityBar.value)
        {
            if (abilitySO.abilityType == AbilitySO.AbilityType.lesserSacrifice)
            {
                LesserSacAbility();
            }
            if (abilitySO.abilityType == AbilitySO.AbilityType.Sacrifice)
            {
                SacrificeAbility();
            }
            if (abilitySO.abilityType == AbilitySO.AbilityType.GreaterSacrifice)
            {
                GreaterSacAbility();
            }
            if (abilitySO.abilityType == AbilitySO.AbilityType.PenUltimateSacrifice)
            {
                PenUltimateSacAbility();
            }
            if (abilitySO.abilityType == AbilitySO.AbilityType.UltimateSacrifice)
            {
                UltimateSacAbility();
            }
            //SacrificeAbility();
            PersistantData.data.mana -= abilitySO.abilityCost;
        }
        else
        {
            return;
        }
    }

    public void LesserHealAbility()
    {
        //Restore 2hp Each Turn for 3 turns
        battleManager.lesserHealEffect = true;
        battleManager.lesserHealCounter = battleManager.turnCounter;
        lesserHealEndTurn = battleManager.turnCounter + 3;
        lesserHealEffect = true;
        CleanUpAbility();
    }
    
    private void LesserHealEffect()
    {
        if (lesserHealEffect)
        {
            if (battleManager.turnCounter >= lesserHealEndTurn)
            {
                lesserHealEffect = false;
                battleManager.lesserHealEffect = false;
            }
        }
    }
    public void HealAbility()
    {
        PersistantData.data.health += 10;
        CleanUpAbility();
    }
    public void GreaterHealAbility()
    {
        battleManager.perfectDefense = true;
        CleanUpAbility();
    }
    public void PenUltimateHealAbility()
    {
        //All Potions Broken Will Grant Additonal 3 Points For 5 Turns;
        penUltimateHealEndTurn = battleManager.turnCounter + 5;
        battleManager.penUltimateHealEffect = true;
        penUltimateHealEffect = true;
        CleanUpAbility();
    }
    private void PenUltimateHealEffect()
    {
        if (penUltimateHealEffect)
        {
            if (battleManager.turnCounter >= penUltimateHealEndTurn)
            {
                penUltimateHealEffect = false;
                battleManager.penUltimateHealEffect = false;
            }
        }
    }
    public void UltimateHealAbility()
    {
        ultimateHealEndTurn = battleManager.turnCounter + 5;
        battleManager.ultimateHealCounter = battleManager.turnCounter;
        battleManager.ultimateHealEffect = true;
        ultimateHealEffect = true;
        CleanUpAbility();
    }
    private void UltimateHealEffect()
    {
        if (ultimateHealEffect)
        {
            if (battleManager.turnCounter >= ultimateHealEndTurn)
            {
                battleManager.ultimateHealEffect = false;
                ultimateHealEffect = false;
            }
        }
    }

    public void LesserSacAbility()
    {
        //Take 2 Additional Damage From the next Enemy Attack
        //Deal 2 Additional Damage For The Next 3 turns
        battleManager.lesserSacEffect = true;
        lesserSacEffect = true;
        battleManager.lesserSacCounter = battleManager.turnCounter -1;
        lesserSacEndTurn = battleManager.turnCounter + 3;
        CleanUpAbility();
    }
    private void LesserSacEffect()
    {
        if (lesserSacEffect)
        {
            if (battleManager.turnCounter >= lesserSacEndTurn)
            {
                lesserSacEffect = false;
                battleManager.lesserSacEffect = false;
            }
        }
    }
    public void SacrificeAbility()
    {
        //Deal 5 to player, 30 to enemy
        PersistantData.data.health -= 5;
        battleManager.monsterHealth -= 30;
        CleanUpAbility();
    }
    public void GreaterSacAbility()
    {
        //Take between 0-3Damage, deal 4-8Damage for 3 turns
        greaterSacEffect = true;
        greaterSacEndTurn = battleManager.turnCounter + 3;
        battleManager.greaterSacEffect = true;
        battleManager.greaterSacCounter = battleManager.turnCounter - 1;
        CleanUpAbility();
    }
    private void GreaterSacEffect()
    {
        if (greaterSacEffect)
        {
            if (battleManager.turnCounter >= greaterSacEndTurn)
            {
                greaterSacEffect = false;
                battleManager.greaterSacEffect = false;
            }
        }
    }
    public void PenUltimateSacAbility()
    {
        //Take 10Dmg, deal 75
        PersistantData.data.health -= 10;
        battleManager.monsterHealth -= 75;
        CleanUpAbility();
    }
    public void UltimateSacAbility()
    {
        //Take 5 Damage Each Turn For 10 Turns. If Enemy Dies PlayerHealth = full;
        ultimateSacEffect = true;
        ultimateSacEndTurn = battleManager.turnCounter + 10;
        battleManager.ultimateSacEffect = true;
        battleManager.ultimateSacCounter = battleManager.turnCounter;
        CleanUpAbility();
    }
    private void UltimateSacEffect()
    {
        if (ultimateSacEffect)
        {
            if (battleManager.turnCounter >= ultimateSacEndTurn)
            {
                ultimateSacEffect = false;
                battleManager.ultimateSacEffect = false;
            }
        }
    }
}
