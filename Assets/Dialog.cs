using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Dialog : MonoBehaviour
{
    //public DialogSO[] dialog;
    public Text dialogText;
    public Image topText;
    public Image midText;
    public Image bottomText;
    public GameObject panel;
    public Text bottomPanelText;
    public Button exit;


    private void Update()
    {
        NextLine();
    }

    public void ClosePanel()
    {
        panel.SetActive(false);
    }
    private void NextLine()
    {
         if (dialogText.enabled)
        {
            if (Input.GetMouseButtonDown(0))
            {
                Debug.Log(Input.GetMouseButtonDown(0));
                Debug.Log("Input");
                topText.gameObject.SetActive(true);
                if (topText.enabled)
                {
                    midText.gameObject.SetActive(true);
                }
                if (midText.enabled)
                {
                    bottomText.gameObject.SetActive(true);
                    bottomPanelText.gameObject.SetActive(false);
                    exit.gameObject.SetActive(true);
                }
                return;

            }
        }
        
    }
}
