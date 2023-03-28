using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProgressTracker : MonoBehaviour
{
    public GameObject DialogueComingSoonButton;
    // Start is called before the first frame update
    void Start()
    {
        GameObject.FindGameObjectWithTag("Player").GetComponent<LevelTracker>().levelInProgress = true;
    }

    public void DialogueComingSoonButtonActivate()
    {
        DialogueComingSoonButton.SetActive(true);
        
    }

    public void DialogueComingSoonButtonDeactivate()
    {
        DialogueComingSoonButton.SetActive(false);
        GameObject.FindGameObjectWithTag("Player").GetComponent<PointsTracker>().points++;

    }

}
