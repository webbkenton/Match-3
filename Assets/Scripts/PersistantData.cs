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

    public bool inEvent;

    private PlayerTokenMover playerTokenMover;
    //public Transform parentTransform;
    //public OverWorldMap overWorld;

    public GameObject[] completed;
    public List<GameObject> completedObject = new List<GameObject>();

    public int totalCompleteLevels;


    private void Start()
    {
        playerToken = GameObject.FindGameObjectWithTag("Player");
        playerTokenMover = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerTokenMover>();
    }
    private void Awake()
    {
        if (data == null)
        {
            data = this;
        }
        DontDestroyOnLoad(data);
    }

    private void ObjectiveComplete()
    {
        totalCompleteLevels++;
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
        playerToken = GameObject.FindGameObjectWithTag("Player");
    }
}
