using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OverWorldMap : MonoBehaviour
{
    private GameObject playerToken;
    public bool objectiveComplete;
    public Transform playerTransform;
    private PlayerTokenMover mover;
    void Start()
    {
        playerToken = GameObject.FindGameObjectWithTag("Player");
        mover = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerTokenMover>();
    }

    private void Update()
    {
        playerToken = GameObject.FindGameObjectWithTag("Player");
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
