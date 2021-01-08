using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TalentAbilityHolder : MonoBehaviour
{
    public AbilitySO abilitySO;
    public BattleManger battleManager;
    public Text abilityDescription;
    public Sprite abilitySprite;
    public GameObject talentPurchased;
    public bool talentAvailable;
    public bool choiceConfirmed;
    public GameObject descriptionBox;

    public GameObject currentAbility1;
    public GameObject currentAbility2;
    public GameObject currentAbility3;


    // Start is called before the first frame update
    void Start()
    {
        abilityDescription = this.GetComponentInChildren<Text>();
        talentPurchased.gameObject.SetActive(false);
        abilityDescription.gameObject.SetActive(false);
        abilityDescription.text = abilitySO.abilityDescription;
        abilitySprite = abilitySO.abilityIcon;
        this.gameObject.GetComponent<Image>().sprite = abilitySprite;

        currentAbility1 = GameObject.FindGameObjectWithTag("CurrentAbility1");
        currentAbility2 = GameObject.FindGameObjectWithTag("CurrentAbility2");
        currentAbility3 = GameObject.FindGameObjectWithTag("CurrentAbility3");
    }
    public void AssignTalent()
    {
        if (PersistantData.data.selectingAbility && PersistantData.data.talentList.Contains(abilitySO))
        {
            if (!PersistantData.data.currentAbilities.Contains(this.abilitySO))
            {
                PersistantData.data.currentAbilities.Add(this.abilitySO);
                Debug.Log("TalentAssigned");
                if (PersistantData.data.abilitySelector == currentAbility1)
                {
                    if (PersistantData.data.currentAbilities[0] != null)
                    {
                        PersistantData.data.currentAbilities[0] = this.abilitySO;
                    }
                }
                if (PersistantData.data.abilitySelector == currentAbility2)
                {
                    if (PersistantData.data.currentAbilities[1] != null)
                    {
                        PersistantData.data.currentAbilities[1] = this.abilitySO;
                    }
                }
                if (PersistantData.data.abilitySelector == currentAbility3)
                {
                    if (PersistantData.data.currentAbilities[2] != null)
                    {
                        PersistantData.data.currentAbilities[2] = this.abilitySO;
                    }
                }
                PersistantData.data.abilitySelector.GetComponent<SelectAbilities>().ability = this.abilitySO;
                PersistantData.data.abilitySelector = null;
                PersistantData.data.selectingAbility = false;
            }
            else if (PersistantData.data.currentAbilities.Contains(this.abilitySO))
            {
                //PersistantData.data.currentAbilities.Remove(this.abilitySO);
                if (PersistantData.data.abilitySelector == currentAbility1)
                {
                    if (PersistantData.data.currentAbilities[0] != null && PersistantData.data.currentAbilities[1] != this.abilitySO && PersistantData.data.currentAbilities[2] != this.abilitySO)
                    {
                        PersistantData.data.currentAbilities[0] = this.abilitySO;
                        
                    }
                }
                else if (PersistantData.data.abilitySelector == currentAbility2)
                {
                    if (PersistantData.data.currentAbilities[1] != null && PersistantData.data.currentAbilities[0] != this.abilitySO && PersistantData.data.currentAbilities[2] != this.abilitySO)
                    {
                        PersistantData.data.currentAbilities[1] = this.abilitySO;

                    }
                }
                else if (PersistantData.data.abilitySelector == currentAbility3)
                {
                    if (PersistantData.data.currentAbilities[2] != null && PersistantData.data.currentAbilities[1] != this.abilitySO && PersistantData.data.currentAbilities[0] != this.abilitySO)
                    {
                        PersistantData.data.currentAbilities[2] = this.abilitySO;
                    }
                }
                PersistantData.data.abilitySelector.GetComponent<SelectAbilities>().ability = this.abilitySO;
                PersistantData.data.abilitySelector = null;
                PersistantData.data.selectingAbility = false;
            }
            else
            {
                return;
            }
        }
        else
        {
            PersistantData.data.selectingAbility = false;
        }
    }
    public void PurchaseTalent()
    {
        PersistantData.data.currentObjective = this.gameObject;
        if (PersistantData.data.pointsToSpend > 0 && talentAvailable == true && PersistantData.data.selectingAbility == false && PersistantData.data.choiceConfirmed)
        {
            PersistantData.data.talentList.Add(this.abilitySO);
            PersistantData.data.pointsToSpend--;
            this.GetComponent<Button>().enabled = false;
            if (abilitySO.abilityFamily == AbilitySO.AbilityFamily.Rage)
            {
                Debug.Log("RageFamily");
                PersistantData.data.ragePoints++;
                //PersistantData.data.pointsToSpend--;
            }
            else if (abilitySO.abilityFamily == AbilitySO.AbilityFamily.Heal)
            {
                PersistantData.data.healPoints++;
                //PersistantData.data.pointsToSpend--;
            }
            else if (abilitySO.abilityFamily == AbilitySO.AbilityFamily.Sac)
            {
                PersistantData.data.sacPoints++;
                //PersistantData.data.pointsToSpend--;
            }
            else
            {
                return;
            }
            talentPurchased.SetActive(true);
            descriptionBox.SetActive(false);
            
        }
    }

    private void setActive()
    {
        if (abilitySO.abilityFamily == AbilitySO.AbilityFamily.Rage)
        {
            if (PersistantData.data.ragePoints >= abilitySO.pointsRequired)
            {
                this.gameObject.GetComponent<Button>().interactable = true;
                talentAvailable = true;
            }
            else
            {
                this.gameObject.GetComponent<Button>().interactable = false;
            }
        }
        if (abilitySO.abilityFamily == AbilitySO.AbilityFamily.Heal)
        {
            if (PersistantData.data.healPoints >= abilitySO.pointsRequired)
            {
                this.gameObject.GetComponent<Button>().interactable = true;
                talentAvailable = true;
            }
            else
            {
                this.gameObject.GetComponent<Button>().interactable = false;
            }
        }
        if (abilitySO.abilityFamily == AbilitySO.AbilityFamily.Sac)
        {
            if (PersistantData.data.sacPoints >= abilitySO.pointsRequired)
            {
                this.gameObject.GetComponent<Button>().interactable = true;
                talentAvailable = true;
            }
            else
                this.gameObject.GetComponent<Button>().interactable = false;
        }
        else
        {
            return;
        }
    }

    private void TrackedAbilities()
    {
        if (PersistantData.data.talentList.Contains(this.abilitySO))
        {
            talentPurchased.SetActive(true);
        }

        if (PersistantData.data.currentAbilities.Count >= 3)
        {
            currentAbility3.GetComponent<SelectAbilities>().ability = PersistantData.data.currentAbilities[2];
            currentAbility2.GetComponent<SelectAbilities>().ability = PersistantData.data.currentAbilities[1];
            currentAbility1.GetComponent<SelectAbilities>().ability = PersistantData.data.currentAbilities[0];
            currentAbility1.GetComponent<Image>().sprite = PersistantData.data.currentAbilities[0].abilityIcon;
            currentAbility2.GetComponent<Image>().sprite = PersistantData.data.currentAbilities[1].abilityIcon;
            currentAbility3.GetComponent<Image>().sprite = PersistantData.data.currentAbilities[2].abilityIcon;
        }

    }
    private void Update()
    {
        setActive();
        selection();
        TrackedAbilities();
    }
    private void selection()
    {
        if (PersistantData.data.selectingAbility)
        {
            this.GetComponent<Button>().enabled = true;
        }
    }


}
