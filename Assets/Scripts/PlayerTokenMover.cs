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
    public MoveCounter moveCounter;
    private Transform tokenTransform;
    private GameObject dialogPanel;

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


    // Start is called before the first frame update

    private void Awake()
    {
        FixToken();
    }
    private void FixToken()
    {
        playerToken.GetComponent<Animator>().enabled = true;
        playerToken.GetComponent<PlayerTokenMover>().enabled = true;
        playerToken.GetComponent<CapsuleCollider2D>().enabled = true;
    }

    public void PlacePeice()
    {
        if (PersistantData.data.totalCompleteLevels >= 1 && PersistantData.data.inEvent == true)
        {
            if (PersistantData.data.totalCompleteLevels == 1)
            {
                Destroy(GameObject.FindGameObjectWithTag("Player"));
                Instantiate(playerToken, platform1.GetComponent<Platform>().playerPlatForm.transform);
                playerToken.transform.position = tokenTransform.position;
                playerToken.transform.rotation = tokenTransform.rotation;
                playerToken.transform.localScale = tokenTransform.localScale;
                FixToken();
                PersistantData.data.inEvent = false;
            }
            if (PersistantData.data.totalCompleteLevels == 2)
            {
                Destroy(GameObject.FindGameObjectWithTag("Player"));
                Instantiate(playerToken, platform2.GetComponent<Platform>().playerPlatForm.transform);
                playerToken.transform.position = tokenTransform.position;
                playerToken.transform.rotation = tokenTransform.rotation;
                playerToken.transform.localScale = tokenTransform.localScale;
                FixToken();
                PersistantData.data.inEvent = false;
            }
            if (PersistantData.data.totalCompleteLevels == 3)
            {
                Destroy(GameObject.FindGameObjectWithTag("Player"));
                Instantiate(playerToken, platform3.GetComponent<Platform>().playerPlatForm.transform);
                playerToken.transform.position = tokenTransform.position;
                playerToken.transform.rotation = tokenTransform.rotation;
                playerToken.transform.localScale = tokenTransform.localScale;
                FixToken();
                PersistantData.data.inEvent = false;
            }
            if (PersistantData.data.totalCompleteLevels == 4)
            {
                Destroy(GameObject.FindGameObjectWithTag("Player"));
                Instantiate(playerToken, platform4.GetComponent<Platform>().playerPlatForm.transform);
                playerToken.transform.position = tokenTransform.position;
                playerToken.transform.rotation = tokenTransform.rotation;
                playerToken.transform.localScale = tokenTransform.localScale;
                FixToken();
                PersistantData.data.inEvent = false;
            }

            if (PersistantData.data.totalCompleteLevels == 5)
            {
                Destroy(GameObject.FindGameObjectWithTag("Player"));
                Instantiate(playerToken, platform5.GetComponent<Platform>().playerPlatForm.transform);
                playerToken.transform.position = tokenTransform.position;
                playerToken.transform.rotation = tokenTransform.rotation;
                playerToken.transform.localScale = tokenTransform.localScale;
                FixToken();
                PersistantData.data.inEvent = false;
            }

            if (PersistantData.data.totalCompleteLevels == 6)
            {
                Destroy(GameObject.FindGameObjectWithTag("Player"));
                Instantiate(playerToken, platform6.GetComponent<Platform>().playerPlatForm.transform);
                playerToken.transform.position = tokenTransform.position;
                playerToken.transform.rotation = tokenTransform.rotation;
                playerToken.transform.localScale = tokenTransform.localScale;
                FixToken();
                PersistantData.data.inEvent = false;
            }

            if (PersistantData.data.totalCompleteLevels == 7)
            {
                Destroy(GameObject.FindGameObjectWithTag("Player"));
                Instantiate(playerToken, platform7.GetComponent<Platform>().playerPlatForm.transform);
                playerToken.transform.position = tokenTransform.position;
                playerToken.transform.rotation = tokenTransform.rotation;
                playerToken.transform.localScale = tokenTransform.localScale;
                FixToken();
                PersistantData.data.inEvent = false;
            }

            if (PersistantData.data.totalCompleteLevels == 8)
            {
                Destroy(GameObject.FindGameObjectWithTag("Player"));
                Instantiate(playerToken, platform8.GetComponent<Platform>().playerPlatForm.transform);
                playerToken.transform.position = tokenTransform.position;
                playerToken.transform.rotation = tokenTransform.rotation;
                playerToken.transform.localScale = tokenTransform.localScale;
                FixToken();
                PersistantData.data.inEvent = false;
            }

            if (PersistantData.data.totalCompleteLevels == 9)
            {
                Destroy(GameObject.FindGameObjectWithTag("Player"));
                Instantiate(playerToken, platform9.GetComponent<Platform>().playerPlatForm.transform);
                playerToken.transform.position = tokenTransform.position;
                playerToken.transform.rotation = tokenTransform.rotation;
                playerToken.transform.localScale = tokenTransform.localScale;
                FixToken();
                PersistantData.data.inEvent = false;
            }

            if (PersistantData.data.totalCompleteLevels == 10)
            {
                Destroy(GameObject.FindGameObjectWithTag("Player"));
                Instantiate(playerToken, platform10.GetComponent<Platform>().playerPlatForm.transform);
                playerToken.transform.position = tokenTransform.position;
                playerToken.transform.rotation = tokenTransform.rotation;
                playerToken.transform.localScale = tokenTransform.localScale;
                FixToken();
                PersistantData.data.inEvent = false;
            }
        }

    }

    void Start()
    {
        tokenTransform = this.transform;

        playerToken = GameObject.FindGameObjectWithTag("Player");
        moveCounter = GameObject.FindGameObjectWithTag("MoveCounter").GetComponent<MoveCounter>();

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
        for (int i = 0; i < p1.Length; i++)
        {
            p1[i].color = new Color(1f, 1f, 1f, 1f);
        }
        for (int i = 0; i < p0.Length; i++)
        {
            p0[i].color = new Color(1f, 1f, 1f, 1f);
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
        
        if (PersistantData.data.totalCompleteLevels != 0)
        {
            PlacePeice();
        }
       
    }
    private void Update()
    {
        PeiceMoved();
        tokenTransform = this.transform;
    }

    private void PeiceMoved()
    {

        if (PersistantData.data.totalCompleteLevels >= 1)
        {
            for (int i = 0; i < p2.Length; i++)
            {
                p2[i].color = new Color(1f, 1f, 1f, 1f);
            }
            
            platform2.interactable = true;
        }
        if (PersistantData.data.totalCompleteLevels >= 2)
        {
            for (int i = 0; i < p3.Length; i++)
            {
                p3[i].color = new Color(1f, 1f, 1f, 1f);
            }
            platform3.interactable = true;

        }
        if (PersistantData.data.totalCompleteLevels >= 3)
        {
            for (int i = 0; i < p4.Length; i++)
            {
                p4[i].color = new Color(1f, 1f, 1f, 1f);
            }
            platform4.interactable = true;

        }
        if (PersistantData.data.totalCompleteLevels >= 4)
        {
            for (int i = 0; i < p5.Length; i++)
            {
                p5[i].color = new Color(1f, 1f, 1f, 1f);
            }
            platform5.interactable = true;


        }
        if (PersistantData.data.totalCompleteLevels >= 5)
        {
            for (int i = 0; i < p6.Length; i++)
            {
                p6[i].color = new Color(1f, 1f, 1f, 1f);
            }
            platform6.interactable = true;

        }
        if (PersistantData.data.totalCompleteLevels >= 6)
        {
            for (int i = 0; i < p7.Length; i++)
            {
                p7[i].color = new Color(1f, 1f, 1f, 1f);
            }
            platform7.interactable = true;

        }
        if (PersistantData.data.totalCompleteLevels >= 7)
        {
            for(int i = 0; i < p8.Length; i++)
            {
                p8[i].color = new Color(1f, 1f, 1f, 1f);
            }
            platform8.interactable = true;

        }
        if (PersistantData.data.totalCompleteLevels >= 8)
        {
            for (int i = 0; i < p9.Length; i++)
            {
                p9[i].color = new Color(1f, 1f, 1f, 1f);
            }
            platform9.interactable = true;

        }
        if (PersistantData.data.totalCompleteLevels >= 9)
        {
            for (int i = 0; i < p10.Length; i++)
            {
                p10[i].color = new Color(1f, 1f, 1f, 1f);
            }
            platform10.interactable = true;
        }

    }


}
