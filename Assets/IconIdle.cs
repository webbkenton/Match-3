using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IconIdle : MonoBehaviour
{
    public bool isDefeated;
    public bool grown;
    private Transform iconSize;
    private Vector3 scaleChange;
    private Transform iconSizeOriginal;
    public GameObject token;
    public GameObject tokenHolder;
    private Vector3 maxSize;
    private Vector3 minSize;

    private void Start()
    {
        //tokenHolder = this.GetComponentInChildren<GameObject>().gameObject;
        token = this.gameObject;
        iconSize = token.transform;
        iconSizeOriginal = token.transform;
        maxSize = new Vector3(.4f, .4f, 1f);
        minSize = new Vector3(.3f, .3f, 1f);
        scaleChange = new Vector3(.05f, .05f, 0f);
        
    }
    void Update()
    {
        if (tokenHolder != null)
        { tokenHolder.transform.localPosition = new Vector2(2.5f, Vector2.zero.y); }
        Bounce();
    }

    private void Bounce()
    {
        if (isDefeated != true && iconSize.localScale.x <= iconSizeOriginal.localScale.x && grown == false)
            iconSize.localScale += scaleChange * Time.deltaTime;
        if (iconSize.localScale.x >= maxSize.x)
        {
            grown = true;
        }
        if (isDefeated != true && grown == true)
            iconSize.localScale -= scaleChange * Time.deltaTime;
        if (iconSize.localScale.x <= minSize.x)
        {
            grown = false;
        }
    }
    public void Action()
    {
        Debug.Log("Clicked");
        Debug.Log(token);
    }
}
