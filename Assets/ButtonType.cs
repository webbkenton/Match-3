using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonType : MonoBehaviour
{
    public bool Fight;
    public bool Shop;
    public bool Dialogue;
    public bool Completed;
    public GameObject SceneHandler;
    public int pointsRequired;
    //spublic GameObject DialogueComingSoonButton;

    private void Start()
    {
        SceneHandler = GameObject.FindGameObjectWithTag("SceneHandler");
        if (this.Fight != true)
        {
            this.GetComponent<MonsterPlaceholderOnIcon>().enabled = false;
        }
    }

    private void Update()
    {
        if (pointsRequired != GameObject.FindGameObjectWithTag("Player").GetComponent<PointsTracker>().points)
        {
            this.GetComponent<Button>().interactable = false;          
        }
        if (GameObject.FindGameObjectWithTag("Player").GetComponent<PointsTracker>().points == pointsRequired)
        {
            this.GetComponent<Button>().interactable = true;
        }
    }


    public void ActivateButtonEvent()
    {
        if (Fight == true)
        {
            Debug.Log("Fight");
            //"Image Pops Up Containing Details of the scene?"
            SceneHandler.GetComponent<FightTrigger>().StartFight();
            GameObject.FindGameObjectWithTag("Player").GetComponent<LevelTracker>().enemy = this.GetComponent<MonsterPlaceholderOnIcon>().randomMonster;
        }
        if (Shop == true)
        {
            Debug.Log("Shop");
            //Open the Shop
            SceneHandler.GetComponent<ShopToken>().OpenShop();
        }
        if (Dialogue == true)
        {
            Debug.Log("Dialogue");
            SceneHandler.GetComponent<ProgressTracker>().DialogueComingSoonButtonActivate();
            //Open the next Dialogue Scene.
        }
    }
}
