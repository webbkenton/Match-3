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

    public GameObject[] completed;
    public List<GameObject> completedObject = new List<GameObject>();

    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);

        if (data == null)
        {
            data = this;
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

    }
}
