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

    public void ClosePanel()
    {
        panel.SetActive(false);
        PersistantData.data.waitForMove = true;
    }

}
