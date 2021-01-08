using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectAbilities : MonoBehaviour
{
    public AbilitySO ability;

    public void SelectAbility()
    {
        PersistantData.data.selectingAbility = true;
        PersistantData.data.abilitySelector = this.gameObject;
    }

    public void Update()
    {
        if (ability != null)
        {
            this.GetComponent<Image>().sprite = ability.abilityIcon;
            if (!PersistantData.data.currentAbilities.Contains(ability))
            {
                PersistantData.data.currentAbilities.Add(ability);
            }
        }
    }
}
