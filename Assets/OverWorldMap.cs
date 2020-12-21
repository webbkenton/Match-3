using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OverWorldMap : MonoBehaviour
{
    private GameObject playerToken;
    public bool objectiveComplete;
    void Start()
    {
        playerToken = GameObject.FindGameObjectWithTag("Player");
    }

    private void Awake()
    {
        if (PersistantData.data.completedObject.Contains(this.gameObject))
        {
            this.GetComponentInParent<Image>().color = new Color(1f, 1f, 1f, 1f);
        }
    }

    private void Update()
    {
        if (this.GetComponentInChildren<PlayerTokenMover>())
        {
            if (this.GetComponentInChildren<PlayerTokenMover>().eventTransform = this.transform)
            {
                this.GetComponentInParent<Image>().color = new Color(1f, 1f, 1f, 1f);
                objectiveComplete = true;
                checkComplete();
            }
            else
            {
                this.GetComponentInParent<Image>().color = new Color(0f, 0f, 0f, 1f);
            }
        }
    }

    public void checkComplete()
    {
        if (objectiveComplete == true)
        {
            if (PersistantData.data.completedObject.Contains(this.gameObject))
            {
                objectiveComplete = false;
            }
            else
            {
                PersistantData.data.completedObject.Add(this.gameObject);
                this.GetComponentInParent<Image>().color = new Color(1f, 1f, 1f, 1f);
                objectiveComplete = false;
            }
        }
    }
    public void MovePlayerToken()
    {
        playerToken.transform.SetParent(this.transform); 
        Mathf.Lerp(playerToken.transform.position.y, this.transform.position.y, .1f);
    }
}
