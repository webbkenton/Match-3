using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHud : MonoBehaviour
{
    public Slider health;
    public Slider mana;
    public int currency;
    public int experience;
    public float experienceToLevel;

    public Text healthText;
    public Text manaText;
    public Text currencyText;

    public Image experienceBar;
    

    private void Update()
    {
        health.maxValue = PersistantData.data.maxHealth;
        health.value = PersistantData.data.health;
        mana.value = PersistantData.data.mana;
        currency = PersistantData.data.currency;
        experience = PersistantData.data.experience;
        experienceToLevel = PersistantData.data.experienceToLevel;

        healthText.text = health.value.ToString();
        manaText.text = mana.value.ToString();
        currencyText.text = currency.ToString();

        experienceBar.fillAmount = experience / experienceToLevel;
        //Debug.Log(experienceBar.fillAmount);
        
    }
}
