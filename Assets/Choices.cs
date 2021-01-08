using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Choices : MonoBehaviour
{
    public GameObject descriptionBox;

    public void SetDescriptionBox()
    {
        if (!PersistantData.data.talentList.Contains(PersistantData.data.currentObjective.GetComponent<TalentAbilityHolder>().abilitySO))
        {
            descriptionBox.SetActive(true);
            descriptionBox.GetComponentInChildren<Text>().text = PersistantData.data.currentObjective.GetComponent<TalentAbilityHolder>().abilityDescription.text;
        }
    }
    public void ChoiceConfirmed()
    {
        PersistantData.data.choiceConfirmed = true;
        PersistantData.data.currentObjective.GetComponent<TalentAbilityHolder>().PurchaseTalent();
        PersistantData.data.choiceConfirmed = false;
    }
    public void ChoiceDenied()
    {
        PersistantData.data.choiceDenied = true;
        PersistantData.data.currentObjective.GetComponent<TalentAbilityHolder>().descriptionBox.SetActive(false);
        PersistantData.data.currentObjective = null;
        PersistantData.data.choiceDenied = false;
    }
}
