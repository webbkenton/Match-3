using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerTokenMover : MonoBehaviour
{
    private Button platform1;
    private Button platform2;
    private Button platform3;
    private Button platform4;
    private Button platform5;
    private Button platform6;
    private Button platform7;
    private Button platform8;
    private Button platform9;
    private Button platform10;

    public Image platform0;

    public GameObject playerToken;
    //public GameObject playerRoot;
    //public MoveCounter moveCounter;
    private Transform tokenTransform;
    private GameObject dialogPanel;
    public GameObject tutorialDialog;

    private Image[] p0;
    private Image[] p1;
    private Image[] p2;
    private Image[] p3;
    private Image[] p4;
    private Image[] p5;
    private Image[] p6;
    private Image[] p7;
    private Image[] p8;
    private Image[] p9;
    private Image[] p10;

    private bool p0Objective;
    private bool p1Objective;
    private bool p2Objective;
    private bool p3Objective;
    private bool p4Objective;
    private bool p5Objective;
    private bool p6Objective;
    private bool p7Objective;
    private bool p8Objective;
    private bool p9Objective;
    private bool p10Objective;



    // Start is called before the first frame update

    private void Awake()
    {
        FixToken();
        //DontDestroyOnLoad(playerRoot);
    }

    IEnumerator NeedASecond()
    {
        if (playerToken != null)
        {
            if(playerToken != PersistantData.data.playerToken)
            Destroy(playerToken);
        }
        yield return new WaitForSeconds(.5f);
    }
    private void FixToken()
    {
        playerToken.GetComponent<Animator>().enabled = true;
        playerToken.GetComponent<PlayerTokenMover>().enabled = true;
        playerToken.GetComponent<CapsuleCollider2D>().enabled = true;
        //playerToken = GameObject.FindGameObjectWithTag("Player");
    }

    public void PlacePeice()
    {
        if (PersistantData.data.totalCompleteLevels >= 1 && PersistantData.data.inEvent == true)
        {
            if (PersistantData.data.totalCompleteLevels == 1)
            {
                if (playerToken.transform.position != tokenTransform.position)
                {
                    //Debug.Log("Instantiate");
                    Destroy(GameObject.FindGameObjectWithTag("Player"));
                    //StartCoroutine(NeedASecond());
                    //PersistantData.data.playerToken.SetActive(true);
                    //Instantiate(PersistantData.data.playerToken, platform4.GetComponent<Platform>().playerPlatForm.transform);
                    playerToken = this.gameObject;
                    playerToken.transform.SetParent(platform1.GetComponent<Platform>().playerPlatForm.transform);
                    playerToken.transform.position = tokenTransform.position;
                    playerToken.transform.rotation = tokenTransform.rotation;
                    playerToken.transform.localScale = tokenTransform.localScale;
                    FixToken();
                }
                PersistantData.data.inEvent = false;
            }

            if (PersistantData.data.totalCompleteLevels == 2)
            {
                if (playerToken.transform.position != tokenTransform.position)
                {
                    //Debug.Log("Instantiate");
                    Destroy(GameObject.FindGameObjectWithTag("Player"));
                    //StartCoroutine(NeedASecond());
                    //PersistantData.data.playerToken.SetActive(true);
                    //Instantiate(PersistantData.data.playerToken, platform4.GetComponent<Platform>().playerPlatForm.transform);
                    playerToken = this.gameObject;
                    playerToken.transform.SetParent(platform2.GetComponent<Platform>().playerPlatForm.transform);
                    playerToken.transform.position = tokenTransform.position;
                    playerToken.transform.rotation = tokenTransform.rotation;
                    playerToken.transform.localScale = tokenTransform.localScale;
                    FixToken();
                }
                PersistantData.data.inEvent = false;
            }
            if (PersistantData.data.totalCompleteLevels == 3)
            {
                if (playerToken.transform.position != tokenTransform.position)
                {
                    //Debug.Log("Instantiate");
                    Destroy(GameObject.FindGameObjectWithTag("Player"));
                    //StartCoroutine(NeedASecond());
                    //PersistantData.data.playerToken.SetActive(true);
                    //Instantiate(PersistantData.data.playerToken, platform4.GetComponent<Platform>().playerPlatForm.transform);
                    playerToken = this.gameObject;
                    playerToken.transform.SetParent(platform3.GetComponent<Platform>().playerPlatForm.transform);
                    playerToken.transform.position = tokenTransform.position;
                    playerToken.transform.rotation = tokenTransform.rotation;
                    playerToken.transform.localScale = tokenTransform.localScale;
                    FixToken();
                }
                PersistantData.data.inEvent = false;
            }
            if (PersistantData.data.totalCompleteLevels == 4)
            {
                if (playerToken.transform.position != tokenTransform.position)
                {
                    //Debug.Log("Instantiate");
                    Destroy(GameObject.FindGameObjectWithTag("Player"));
                    //StartCoroutine(NeedASecond());
                    //PersistantData.data.playerToken.SetActive(true);
                    //Instantiate(PersistantData.data.playerToken, platform4.GetComponent<Platform>().playerPlatForm.transform);
                    playerToken = this.gameObject;
                    playerToken.transform.SetParent(platform4.GetComponent<Platform>().playerPlatForm.transform);
                    playerToken.transform.position = tokenTransform.position;
                    playerToken.transform.rotation = tokenTransform.rotation;
                    playerToken.transform.localScale = tokenTransform.localScale;
                    FixToken();
                }
                PersistantData.data.inEvent = false;
            }

            if (PersistantData.data.totalCompleteLevels == 5)
            {
                if (playerToken.transform.position != tokenTransform.position)
                {
                    //Debug.Log("Instantiate");
                    Destroy(GameObject.FindGameObjectWithTag("Player"));
                    //StartCoroutine(NeedASecond());
                    //PersistantData.data.playerToken.SetActive(true);
                    //Instantiate(PersistantData.data.playerToken, platform4.GetComponent<Platform>().playerPlatForm.transform);
                    playerToken = this.gameObject;
                    playerToken.transform.SetParent(platform5.GetComponent<Platform>().playerPlatForm.transform);
                    playerToken.transform.position = tokenTransform.position;
                    playerToken.transform.rotation = tokenTransform.rotation;
                    playerToken.transform.localScale = tokenTransform.localScale;
                    FixToken();
                }
                PersistantData.data.inEvent = false;
            }

            if (PersistantData.data.totalCompleteLevels == 6)
            {
                if (playerToken.transform.position != tokenTransform.position)
                {
                    //Debug.Log("Instantiate");
                    Destroy(GameObject.FindGameObjectWithTag("Player"));
                    //StartCoroutine(NeedASecond());
                    //PersistantData.data.playerToken.SetActive(true);
                    //Instantiate(PersistantData.data.playerToken, platform4.GetComponent<Platform>().playerPlatForm.transform);
                    playerToken = this.gameObject;
                    playerToken.transform.SetParent(platform6.GetComponent<Platform>().playerPlatForm.transform);
                    playerToken.transform.position = tokenTransform.position;
                    playerToken.transform.rotation = tokenTransform.rotation;
                    playerToken.transform.localScale = tokenTransform.localScale;
                    FixToken();
                }
                PersistantData.data.inEvent = false;
            }

            if (PersistantData.data.totalCompleteLevels == 7)
            {
                if (playerToken.transform.position != tokenTransform.position)
                {
                    //Debug.Log("Instantiate");
                    Destroy(GameObject.FindGameObjectWithTag("Player"));
                    //StartCoroutine(NeedASecond());
                    //PersistantData.data.playerToken.SetActive(true);
                    //Instantiate(PersistantData.data.playerToken, platform4.GetComponent<Platform>().playerPlatForm.transform);
                    playerToken = this.gameObject;
                    playerToken.transform.SetParent(platform7.GetComponent<Platform>().playerPlatForm.transform);
                    playerToken.transform.position = tokenTransform.position;
                    playerToken.transform.rotation = tokenTransform.rotation;
                    playerToken.transform.localScale = tokenTransform.localScale;
                    FixToken();
                }
                PersistantData.data.inEvent = false;
            }

            if (PersistantData.data.totalCompleteLevels == 8)
            {
                if (playerToken.transform.position != tokenTransform.position)
                {
                    //Debug.Log("Instantiate");
                    Destroy(GameObject.FindGameObjectWithTag("Player"));
                    //StartCoroutine(NeedASecond());
                    //PersistantData.data.playerToken.SetActive(true);
                    //Instantiate(PersistantData.data.playerToken, platform4.GetComponent<Platform>().playerPlatForm.transform);
                    playerToken = this.gameObject;
                    playerToken.transform.SetParent(platform8.GetComponent<Platform>().playerPlatForm.transform);
                    playerToken.transform.position = tokenTransform.position;
                    playerToken.transform.rotation = tokenTransform.rotation;
                    playerToken.transform.localScale = tokenTransform.localScale;
                    FixToken();
                }
                PersistantData.data.inEvent = false;
            }

            if (PersistantData.data.totalCompleteLevels == 9)
            {
                if (playerToken.transform.position != tokenTransform.position)
                {
                    //Debug.Log("Instantiate");
                    Destroy(GameObject.FindGameObjectWithTag("Player"));
                    //StartCoroutine(NeedASecond());
                    //PersistantData.data.playerToken.SetActive(true);
                    //Instantiate(PersistantData.data.playerToken, platform4.GetComponent<Platform>().playerPlatForm.transform);
                    playerToken = this.gameObject;
                    playerToken.transform.SetParent(platform9.GetComponent<Platform>().playerPlatForm.transform);
                    playerToken.transform.position = tokenTransform.position;
                    playerToken.transform.rotation = tokenTransform.rotation;
                    playerToken.transform.localScale = tokenTransform.localScale;
                    FixToken();
                }
                PersistantData.data.inEvent = false;
            }

            if (PersistantData.data.totalCompleteLevels >= 10)
            {
                if (playerToken.transform.position != tokenTransform.position)
                {
                    //Debug.Log("Instantiate");
                    Destroy(GameObject.FindGameObjectWithTag("Player"));
                    //StartCoroutine(NeedASecond());
                    //PersistantData.data.playerToken.SetActive(true);
                    //Instantiate(PersistantData.data.playerToken, platform4.GetComponent<Platform>().playerPlatForm.transform);
                    playerToken = this.gameObject;
                    playerToken.transform.SetParent(platform10.GetComponent<Platform>().playerPlatForm.transform);
                    playerToken.transform.position = tokenTransform.position;
                    playerToken.transform.rotation = tokenTransform.rotation;
                    playerToken.transform.localScale = tokenTransform.localScale;
                    FixToken();
                }
                PersistantData.data.inEvent = false;
            }
        }

    }

    void Start()
    {
        tokenTransform = this.transform;

        playerToken = PersistantData.data.playerToken;

        platform0 = GameObject.FindGameObjectWithTag("Platform0").GetComponent<Image>();
        platform1 = GameObject.FindGameObjectWithTag("Platform1").GetComponent<Button>();
        platform2 = GameObject.FindGameObjectWithTag("Platform2").GetComponent<Button>();
        platform3 = GameObject.FindGameObjectWithTag("Platform3").GetComponent<Button>();
        platform4 = GameObject.FindGameObjectWithTag("Platform4").GetComponent<Button>();
        platform5 = GameObject.FindGameObjectWithTag("Platform5").GetComponent<Button>();
        platform6 = GameObject.FindGameObjectWithTag("Platform6").GetComponent<Button>();
        platform7 = GameObject.FindGameObjectWithTag("Platform7").GetComponent<Button>();
        platform8 = GameObject.FindGameObjectWithTag("Platform8").GetComponent<Button>();
        platform9 = GameObject.FindGameObjectWithTag("Platform9").GetComponent<Button>();
        platform10 = GameObject.FindGameObjectWithTag("Platform10").GetComponent<Button>();

        p0 = GameObject.FindGameObjectWithTag("Platform0").GetComponentsInChildren<Image>();
        p1 = GameObject.FindGameObjectWithTag("Platform1").GetComponentsInChildren<Image>();
        p2 = GameObject.FindGameObjectWithTag("Platform2").GetComponentsInChildren<Image>();
        p3 = GameObject.FindGameObjectWithTag("Platform3").GetComponentsInChildren<Image>();
        p4 = GameObject.FindGameObjectWithTag("Platform4").GetComponentsInChildren<Image>();
        p5 = GameObject.FindGameObjectWithTag("Platform5").GetComponentsInChildren<Image>();
        p6 = GameObject.FindGameObjectWithTag("Platform6").GetComponentsInChildren<Image>();
        p7 = GameObject.FindGameObjectWithTag("Platform7").GetComponentsInChildren<Image>();
        p8 = GameObject.FindGameObjectWithTag("Platform8").GetComponentsInChildren<Image>();
        p9 = GameObject.FindGameObjectWithTag("Platform9").GetComponentsInChildren<Image>();
        p10 = GameObject.FindGameObjectWithTag("Platform10").GetComponentsInChildren<Image>();


        platform1.interactable = true;
        p0Objective = true;
        p1Objective = true;
        for (int i = 0; i < p1.Length; i++)
        {
            p1[i].color = new Color(1f, 1f, 1f, 1f);
            //p1[i].GetComponent<Platform>().alreadyDone = true;
        }
        for (int i = 0; i < p0.Length; i++)
        {
            p0[i].color = new Color(1f, 1f, 1f, 1f);
            //p0[i].GetComponent<Platform>().alreadyDone = true;
        }

        platform2.interactable = false;
        platform3.interactable = false;
        platform4.interactable = false;
        platform5.interactable = false;
        platform6.interactable = false;
        platform7.interactable = false;
        platform8.interactable = false;
        platform9.interactable = false;
        platform10.interactable = false;

        if (PersistantData.data.totalCompleteLevels != 0 )
        {
            PlacePeice();
        }
       
    }
    private void Update()
    {
        PeiceMoved();
        tokenTransform = this.transform;
        /*if (PersistantData.data.tutorial == true && PersistantData.data.totalCompleteLevels == 1)
        {
            tutorialDialog.SetActive(true);
        }*/
    }

    private void PeiceMoved()
    {

        if (PersistantData.data.totalCompleteLevels >= 1 && PersistantData.data.inEvent == false)
        {
            for (int i = 0; i < p2.Length; i++)
            {
                p2[i].color = new Color(1f, 1f, 1f, 1f);
                

            }
            platform1.GetComponentInParent<Platform>().alreadyDone = true;
            p2Objective = true;
            platform2.interactable = true;
        }
        if (PersistantData.data.totalCompleteLevels >= 2 && PersistantData.data.inEvent == false)
        {
            for (int i = 0; i < p3.Length; i++)
            {
                p3[i].color = new Color(1f, 1f, 1f, 1f);
            }
            platform2.GetComponentInParent<Platform>().alreadyDone = true;
            p3Objective = true;
            platform3.interactable = true;

        }
        if (PersistantData.data.totalCompleteLevels >= 3 && PersistantData.data.inEvent == false)
        {
            for (int i = 0; i < p4.Length; i++)
            {
                p4[i].color = new Color(1f, 1f, 1f, 1f);
                //p4[i].GetComponent<Platform>().alreadyDone = true;
            }
            platform3.GetComponentInParent<Platform>().alreadyDone = true;
            p4Objective = true;
            platform4.interactable = true;

        }
        if (PersistantData.data.totalCompleteLevels >= 4 && PersistantData.data.inEvent == false)
        {
            for (int i = 0; i < p5.Length; i++)
            {
                p5[i].color = new Color(1f, 1f, 1f, 1f);
                //p5[i].GetComponent<Platform>().alreadyDone = true;
            }
            platform4.GetComponentInParent<Platform>().alreadyDone = true;
            p5Objective = true;
            platform5.interactable = true;


        }
        if (PersistantData.data.totalCompleteLevels >= 5 && PersistantData.data.inEvent == false)
        {
            for (int i = 0; i < p6.Length; i++)
            {
                p6[i].color = new Color(1f, 1f, 1f, 1f);
                //p6[i].GetComponent<Platform>().alreadyDone = true;
            }
            platform5.GetComponentInParent<Platform>().alreadyDone = true;
            p6Objective = true;
            platform6.interactable = true;

        }
        if (PersistantData.data.totalCompleteLevels >= 6 && PersistantData.data.inEvent == false)
        {
            for (int i = 0; i < p7.Length; i++)
            {
                p7[i].color = new Color(1f, 1f, 1f, 1f);
                //p7[i].GetComponent<Platform>().alreadyDone = true;
            }
            platform6.GetComponentInParent<Platform>().alreadyDone = true;
            p7Objective = true;
            platform7.interactable = true;

        }
        if (PersistantData.data.totalCompleteLevels >= 7 && PersistantData.data.inEvent == false)
        {
            for(int i = 0; i < p8.Length; i++)
            {
                p8[i].color = new Color(1f, 1f, 1f, 1f);
                //p8[i].GetComponent<Platform>().alreadyDone = true;
            }
            platform7.GetComponentInParent<Platform>().alreadyDone = true;
            p8Objective = true;
            platform8.interactable = true;

        }
        if (PersistantData.data.totalCompleteLevels >= 8 && PersistantData.data.inEvent == false)
        {
            for (int i = 0; i < p9.Length; i++)
            {
                p9[i].color = new Color(1f, 1f, 1f, 1f);
                //p9[i].GetComponent<Platform>().alreadyDone = true;
            }
            platform8.GetComponentInParent<Platform>().alreadyDone = true;
            p9Objective = true;
            platform9.interactable = true;

        }
        if (PersistantData.data.totalCompleteLevels >= 9 && PersistantData.data.inEvent == false)
        {
            for (int i = 0; i < p10.Length; i++)
            {
                p10[i].color = new Color(1f, 1f, 1f, 1f);
                //p10[i].GetComponent<Platform>().alreadyDone = true;
            }
            platform9.GetComponentInParent<Platform>().alreadyDone = true;
            p10Objective = true;
            platform10.interactable = true;
        }

    }


}
