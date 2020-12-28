using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PersistantData : MonoBehaviour
{


    public static PersistantData data;

    public float health;
    public float maxHealth;
    public float mana;
    public int experience;
    public int currency;
    public float experienceToLevel;
    public Vector3 playerPosition;
    public GameObject playerToken;
    public bool tutorial;
    public bool waitForMove;
    public GameObject currentObjective;

    public bool inEvent;

    private PlayerTokenMover playerTokenMover;
    //public Transform parentTransform;
    //public OverWorldMap overWorld;

    public GameObject[] completed;
    public List<GameObject> completedObject = new List<GameObject>();

    public int totalCompleteLevels;


    private void Start()
    {
        //playerToken = GameObject.FindGameObjectWithTag("Player");
        playerTokenMover = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerTokenMover>();
    }
    private void Awake()
    {
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

    private void Update()
    {
        if (mana > 100)
        {
            mana = 100;
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
    }
}
