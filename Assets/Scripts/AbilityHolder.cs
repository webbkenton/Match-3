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
    public int currentRemainder;
    public Text abilityDescription;
    public GameObject abilityReady;
    public Text abilityReadyText;
    private bool coolDown;

    Ray ray;
    RaycastHit hit;

    private void Start()
    {
        battleManager = GameObject.FindGameObjectWithTag("BattleManager").GetComponent<BattleManger>();
        board = GameObject.FindGameObjectWithTag("Board").GetComponent<Board>();
        abilityDescription.text = abilitySO.abilityDescription;
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
        AbilityColor();
        OffCD();
        
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
        if (currentCount == 0)
        {
            currentCount = battleManager.turnCounter;
        }
        
    }

    IEnumerator ReadyText()
    {
        abilityReady.SetActive(true);
        abilityReadyText.text = abilitySO.abilityName + " Is Ready";
        yield return new WaitForSeconds(2f);
        abilityReady.SetActive(false);
    }    

    private void OffCD()
    {
        if (currentRemainder >= abilitySO.coolDownTime)
        {
            this.GetComponent<Button>().interactable = true;
            currentRemainder = 0;
            currentCount = 0;
        }
    }

    public void RageAbility()
    {
        if (abilitySO.abilityType == AbilitySO.AbilityType.Rage)
        {
            Debug.Log("Rage");
            battleManager.UseRageAbility();
            this.GetComponent<Button>().interactable = false;
            AbilityCooldown();
            board.currentState = GameState.move;
            //Debug.Log(currentCount);

        }
    }
    public void HealAbility()
    {
        if (abilitySO.abilityType == AbilitySO.AbilityType.Heal)
        {
            Debug.Log("Heal");
            battleManager.playerHealthBar.value += abilitySO.abilityCost;
            this.GetComponent<Button>().interactable = false;
            AbilityCooldown();
            board.currentState = GameState.move;
            //Debug.Log(currentCount);

        }
    }
    public void SacrificeAbility()
    {
        Debug.Log("Sac");
        if (abilitySO.abilityType == AbilitySO.AbilityType.Sacrifice)
        {
            battleManager.UseSacrificeAbility();
            this.GetComponent<Button>().interactable = false;
            AbilityCooldown();
            board.currentState = GameState.move;
            //Debug.Log(currentCount);

        }
    }
    public void UseAbility()
    {
        if (abilitySO.abilityType == AbilitySO.AbilityType.Rage && abilitySO.abilityCost <= battleManager.playerAbilityBar.value )//&& abilitySO.coolDownTime >= TurnCounter
        {
            RageAbility();
            battleManager.playerAbilityBar.value -= abilitySO.abilityCost;
        }
        else if (abilitySO.abilityType == AbilitySO.AbilityType.Heal && abilitySO.abilityCost <= battleManager.playerAbilityBar.value)
        {
            HealAbility();
            battleManager.playerAbilityBar.value -= abilitySO.abilityCost;
            //battleManager.playerHealthBar.value += abilitySO.healValue;
        }
        else if (abilitySO.abilityType == AbilitySO.AbilityType.Sacrifice && abilitySO.abilityCost <= battleManager.playerAbilityBar.value)
        {
            SacrificeAbility();
            battleManager.playerAbilityBar.value -= abilitySO.abilityCost;
        }
    }
}
