using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FightTrigger : MonoBehaviour
{
    private Scene currentScene;
    private string sceneName;
    public bool inCombat;
    public bool defeated;
    public bool playerSelected;
    private Transform currentPosition;

    private void Start()
    {
        //currentPosition = GameObject.FindGameObjectWithTag("Player").GetComponent<LevelTracker>().currentPosition;
    }
    private void Update()
    {
        StartCoroutine(Defeated());
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && this.playerSelected == true)
        {
            //Debug.Log("21FightTrigger");

            
            StartFight();
        }
    }

    private IEnumerator Defeated()
    {
        if (defeated)
        {
            this.GetComponent<Animator>().SetBool("Defeated", true);
            yield return new WaitForSeconds(1f);
            this.GetComponent<Animator>().SetBool("AnimationPlayed", true);
        }
    }

    private void GetSceneInfo()
    {
        currentScene = SceneManager.GetActiveScene();
        sceneName = currentScene.name;
        //PersistantData.data.EnemyWorldToken = this.gameObject; --No Longer Works With New Map Configuration
        //GameObject.FindGameObjectWithTag("Player").GetComponent<LevelTracker>().enemy = this.gameObject;
        //GameObject.FindGameObjectWithTag("Player").GetComponent<LevelTracker>().currentPosition = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        GameObject.FindGameObjectWithTag("Player").GetComponent<MoveTowardsTest>().inCombat = true;
        GameObject.FindGameObjectWithTag("Player").GetComponent<LevelTracker>().currentLevel = sceneName;
        //this.gameObject.GetComponentInParent<ColumnTracker>().TokenHolder = this.GetComponent<IconIdle>().tokenHolder;
    }
    public void StartFight()
    {
        if (!inCombat && !defeated)
        {
            inCombat = true;
            GetSceneInfo();
            StartCoroutine(LoadLevel());
        }
    }
    public IEnumerator LoadLevel()
    {
        StartCoroutine(PersistantData.data.TransitionIn());
        yield return new WaitForSeconds(2f);
        //GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>().map.transform.localScale = new Vector3(0, 0, 0);
        GameObject.FindGameObjectWithTag("Player").transform.localScale = new Vector3(0, 0, 0);
        GameObject.FindGameObjectWithTag("UIManager").GetComponent<MenuScript>().Match3UI.SetActive(true);
        GameObject.FindGameObjectWithTag("UIManager").GetComponent<MenuScript>().Match3Board.SetActive(true);
        GameObject.FindGameObjectWithTag("BattleManager").GetComponent<BattleManger>().GetInfo();
        GameObject.FindGameObjectWithTag("BattleManager").GetComponent<BattleManger>().GetMonsterInfo();
        SceneManager.LoadScene("MainMatch3");
    }
}
