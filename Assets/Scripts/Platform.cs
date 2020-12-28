using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour
{
    public GameObject playerPlatForm;
    public GameObject uiPanel;
    public bool alreadyDone;
    public bool isMonster;

    private void Update()
    {
        if (GetComponentInChildren<PlayerTokenMover>())
        {
            if (PersistantData.data.waitForMove == false && alreadyDone == false && isMonster == false)
            {
                uiPanel.SetActive(true);
            }
        }
        else
        {
            uiPanel.SetActive(false);
        }
    }
    public void ObjectiveComplete()
    {
        PersistantData.data.totalCompleteLevels++;
        PersistantData.data.waitForMove = true;
        alreadyDone = true;
        uiPanel.SetActive(false);
    }
}
