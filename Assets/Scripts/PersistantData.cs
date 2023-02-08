using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PersistantData : MonoBehaviour
{


    public static PersistantData data;

    public float experienceToLevel;
    public float health;
    public float maxHealth;
    public float mana;
    public float maxMana;

    public Vector3 playerPosition;
 
    public bool selectingAbility;
    public bool newPointer;
    public bool tutorial;
    public bool waitForMove;
    public bool inEvent;
    public bool choiceConfirmed;
    public bool choiceDenied;
    public bool returnToMap;
    public bool gameStarted;

    private PlayerTokenMover playerTokenMover;

    public GameObject[] completed;
    public List<GameObject> completedObject = new List<GameObject>();
    public List<MonsterSO> defeatedMonsters = new List<MonsterSO>();
    public List<AbilitySO> talentList = new List<AbilitySO>();
    public List<AbilitySO> currentAbilities = new List<AbilitySO>();

    public int experience;
    public int currency;
    public int totalCompleteLevels;
    public int pointsToSpend;
    public int playerLevel;
    public int ragePoints;
    public int healPoints;
    public int sacPoints;

    public GameObject abilitySelector;
    public GameObject currentObjective;
    public GameObject outTransition;
    public GameObject inTransition;
    public GameObject playerToken;
    public GameObject PlayerHolder;
    public GameObject EnemyWorldToken;

    private GameObject map;


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
        DupilicateInScene();


        if (playerToken == null && inEvent == false)
        {
            playerToken = GameObject.FindGameObjectWithTag("Player");
        }
    }

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

    private void DupilicateInScene()
    {
        if (playerToken.activeInHierarchy)
        {
            if (GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>().map != null && SceneManager.GetActiveScene().name == "NewMap" && returnToMap)
            {
                Scene currentScene = SceneManager.GetSceneByName("NewMap");
                GameObject[] gameObjects = currentScene.GetRootGameObjects();
                foreach (GameObject item in gameObjects)
                {
                    if (item.name == "Map")
                    {
                        GameObject.Destroy(item);
                    }
                }
            }

        }
    }
    void OnEnable()
    {
        //Tell our 'OnLevelFinishedLoading' function to start listening for a scene change as soon as this script is enabled.
        SceneManager.sceneLoaded += OnLevelFinishedLoading;
    }

    void OnDisable()
    {
        //Tell our 'OnLevelFinishedLoading' function to stop listening for a scene change as soon as this script is disabled. Remember to always have an unsubscription for every delegate you subscribe to!
        SceneManager.sceneLoaded -= OnLevelFinishedLoading;
    }

    void OnLevelFinishedLoading(Scene scene, LoadSceneMode mode)
    {
        //Debug.Log("Level Loaded");
        Debug.Log(scene.name);
        //Debug.Log(mode);
        if (scene.name == "NewMap") 
        {
            string columnName = playerToken.GetComponent<MoveTowardsTest>().selectedColumn;
            playerToken.GetComponent<LifeTracker>().TokenHolderUIBox.SetActive(true);
            playerToken.GetComponent<MoveTowardsTest>().defaultTokenHolder = GameObject.FindGameObjectWithTag("MiddlePath").GetComponent<ColumnTracker>().TokenHolder;
            if (columnName == null)
            {
                playerToken.transform.position = playerToken.GetComponent<MoveTowardsTest>().defaultTokenHolder.transform.position;
            }
            else
            {
                playerToken.transform.position = GameObject.FindGameObjectWithTag(columnName).GetComponent<ColumnTracker>().TokenHolder.transform.position;
            }   
            playerToken.transform.position = new Vector3(playerToken.transform.position.x, playerToken.transform.position.y, 0);
        //    PlayerHolder.SetActive(true); 
        }
        StartCoroutine(TransitionOut());


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

   
}
