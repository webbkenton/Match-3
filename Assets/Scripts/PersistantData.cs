using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PersistantData : MonoBehaviour
{


    public static PersistantData data;

    public float health;
    public float maxHealth;
    public float mana;
    public float maxMana;
    public int experience;
    public int currency;
    public float experienceToLevel;
    public Vector3 playerPosition;
    public GameObject playerToken;
    public bool tutorial;
    public bool waitForMove;
    public GameObject currentObjective;
    public GameObject outTransition;
    public GameObject inTransition;
    public int pointsToSpend;
    public int playerLevel;

    public bool inEvent;
    public bool choiceConfirmed;
    public bool choiceDenied;

    private PlayerTokenMover playerTokenMover;
    //public Transform parentTransform;
    //public OverWorldMap overWorld;

    public GameObject[] completed;
    public List<GameObject> completedObject = new List<GameObject>();
    public List<MonsterSO> defeatedMonsters = new List<MonsterSO>();
    public List<AbilitySO> talentList = new List<AbilitySO>();
    public List<AbilitySO> currentAbilities = new List<AbilitySO>();

    public int totalCompleteLevels;

    public int ragePoints;
    public int healPoints;
    public int sacPoints;

    public bool selectingAbility;
    public bool newPointer;
    public GameObject abilitySelector;






    public IEnumerator TransitionOut()
    {
        inTransition.SetActive(false);
        outTransition.SetActive(true);
        Animator[] animators = outTransition.GetComponentsInChildren<Animator>();
        for (int i = 0; i < animators.Length; i++)
        {
            animators[i].SetBool("slideIn", false);
        }
        yield return new WaitForSeconds(2.5f);
    }
    public IEnumerator TransitionIn()
    {
        outTransition.SetActive(false);
        inTransition.SetActive(true);
        Animator[] animators = inTransition.GetComponentsInChildren<Animator>();
        for (int i = 0; i < animators.Length; i++)
        {
            animators[i].SetBool("slideIn", true);
        }
        yield return new WaitForSeconds(2.5f);
        //StartCoroutine(TransitionOut());
    }
    private void Awake()
    {
        //transitionOut();
        
        if (data == null)
        {
            data = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }
    private void ObjectiveComplete()
    {
        for (int i = 0; i < completedObject.Count; i++)
        {
            if (completed[i].GetComponent<OverWorldMap>().objectiveComplete)
            {
                totalCompleteLevels++;
            }
        }
    }

    public void ContinueButton()
    {
        SceneManager.LoadScene("Level1");
    }
    private void LevelUp()
    {
        if (experience >= experienceToLevel)
        {
            pointsToSpend++;
            playerLevel++;
            newPointer = true;
            experience = (int)experienceToLevel - experience;
            experienceToLevel = experienceToLevel * 1.5f;
        }
    }

    /*private void RemoveAbility()
    {
        for (int i = 0; i < currentAbilities.Count; i++)
        {
            //We need to have a list of our current abilities
            //and a list of old current abilities
            // if oldcurrentabilities !contian currentabilties remove currentabilties[i]
            if(currentAbilities[i])
        }
    }*/
    public void DespawnMonster()
    {
        for (int i = 0; i < defeatedMonsters.Count; i++)
        {
            if (GameObject.FindGameObjectWithTag("OverworldObject").GetComponent<MonsterSO>() == defeatedMonsters[i])
            {
                Destroy(GameObject.FindGameObjectWithTag("OverworldObject").GetComponent<MonsterSO>());
            }
        }
    }

    private void Update()
    {
        if (mana > maxMana)
        {
            mana = maxMana;
        }
        if (health > maxHealth)
        {
            health = maxHealth;
        }
        //playerToken = GameObject.FindGameObjectWithTag("Player");
        //ExtraTokens();
        if (GameObject.FindGameObjectWithTag("Tutorial"))
        {
            tutorial = true;
        }
        ObjectiveComplete();
        LevelUp();

        if (playerToken == null && inEvent == false)
        {
            playerToken = GameObject.FindGameObjectWithTag("Player");
        }
    }
}
