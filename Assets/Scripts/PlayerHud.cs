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
    public GameObject talentTree;
    //public GameObject pointer;

    public Text healthText;
    public Text manaText;
    public Text currencyText;

    public Image experienceBar;

    public Text playerLevel;
    public Text pointToSpend;
    

    private void Update()
    {
        health.maxValue = PersistantData.data.maxHealth;
        health.value = PersistantData.data.health;
        mana.value = PersistantData.data.mana;
        currency = PersistantData.data.currency;
        experience = PersistantData.data.experience;
        experienceToLevel = PersistantData.data.experienceToLevel;
        playerLevel.text = PersistantData.data.playerLevel.ToString();

        if (pointToSpend != null)
        {
            pointToSpend.text = PersistantData.data.pointsToSpend.ToString();
        }

        healthText.text = health.value.ToString();
        manaText.text = mana.value.ToString();
        currencyText.text = currency.ToString();

        experienceBar.fillAmount = experience / experienceToLevel;
        PointAlert();
        //Debug.Log(experienceBar.fillAmount);
    }

    private void PointAlert()
    {
        if (PersistantData.data.pointsToSpend >= 1 && PersistantData.data.newPointer)
        {
            StartCoroutine(disablePointer());
        }
    }
    IEnumerator disablePointer()
    {
        new WaitForSeconds(5f);
        //pointer.SetActive(true);
        yield return new WaitForSeconds(15f);
       // pointer.SetActive(false);
        PersistantData.data.newPointer = false;
    }
    public void OpenTalentTree()
    {
        if (!talentTree.activeInHierarchy && PersistantData.data.inEvent == false)
        {
            talentTree.SetActive(true);
        }
        else
        {
            talentTree.SetActive(false);
        }
    }
}
