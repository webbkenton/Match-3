using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColumnTracker : MonoBehaviour
{
    public List<GameObject> ColumnEnemies;
    public string columnName;
    public GameObject TokenHolder;
    void Start()
    {
        FindTheChildren();
    }

    private void FindTheChildren()
    {
        foreach (Transform child in transform)
        {
            ColumnEnemies.Add(child.gameObject);
        }
    }
}
