using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using UnityEngine.SceneManagement;

public class BattleManger : MonoBehaviour
{
    public bool defending;
    public MonsterSO monsterSO;
    private MonsterSO.MonsterType monsterType;
    public Text monsterName;
    public int monsterHealth;
    public Slider monsterHealthBar;
    public Text monsterHealthText;
    public Image monsterPicture;
    private Sprite monsterSprite;
    public Slider monsterRageBar;
    public int monsterRageCounter;
    public Text monsterRageCounterText;
    private Board board;
    public int attackCounter;
    public Slider playerHealthBar;
    public Slider playerAbilityBar;
    private int currentPlayerHealth;
    public GameObject endPanel;
    public GameObject[] manaPotions;
    public GameObject[] particles;
    public GameObject heavyAttackPrefab;
    public AudioSource monsterSound;
    public Text playerHealth;
    public Text playerAbilityAmount;
    private GameObject enemy;
    public GameObject slashEffect;
    public GameObject playerDamagedPortrait;
    public GameObject playerPortrait;
    public GameObject abilityEffect;
    public GameObject playerDamagedEffect;
    private SoundManager soundManager;
    private CurrencyManager currencyManager;
    public GameObject playerImage;
    public int turnCounter;
    private Vector2 playerImageLocation;
    public GameObject player;
    public Text moveCounterText;
    public AbilityHolder[] abilityHolder;

    public Text monsterKillValue;
    public Text monsterKillXP;
    public int monsterValue;
    public int monsterXP;

    public int playerXP;

    private MonsterInfo monsterInfo;

    private GameObject playerHudDisplay;




    void Start()
    {
        playerHealthBar.value = PersistantData.data.health;
        playerAbilityBar.value = PersistantData.data.mana;
        playerHudDisplay = GameObject.FindGameObjectWithTag("PlayerHud");
        currencyManager = GameObject.FindGameObjectWithTag("CurrencyManager").GetComponent<CurrencyManager>();
        monsterInfo = GameObject.FindGameObjectWithTag("CurrentEnemy").GetComponent<MonsterInfo>();
        monsterSO = monsterInfo.monster;
        turnCounter = 0;
        playerImageLocation = new Vector2(-4f, 5.5f);
        abilityHolder = FindObjectsOfType<AbilityHolder>();
        enemy = GameObject.FindGameObjectWithTag("Enemy");
        board = FindObjectOfType<Board>();
        soundManager = FindObjectOfType<SoundManager>();
        monsterType = monsterSO.monsterType;
        monsterName.text = monsterSO.monsterName;
        monsterHealth = monsterSO.monsterHealth;
        monsterRageCounter = monsterSO.rageCounter;
        monsterPicture.sprite = monsterSO.monsterImage;
        monsterHealthBar.maxValue = monsterHealth;
        monsterSound.clip = monsterSO.monsterNoise;
        monsterSound.Play();
        monsterValue = monsterSO.monsterKillValue;
        monsterXP = monsterSO.monsterKillXP;
        Destroy(GameObject.FindGameObjectWithTag("CurrentEnemy"));
        Destroy(GameObject.FindGameObjectWithTag("OverworldObject"));

    }

    private void Update()
    {
        monsterHealthText.text = monsterHealth.ToString();
        monsterRageCounterText.text = monsterRageCounter.ToString();
        playerHealth.text = PersistantData.data.health.ToString();
        playerAbilityAmount.text = PersistantData.data.mana.ToString();
        if (monsterHealth <= 0)
        {
            endPanel.SetActive(true);
        }
        moveCounterText.text = turnCounter.ToString();

        if (monsterSO.isBoss)
        {
            monsterPicture.transform.localPosition = new Vector2(monsterPicture.transform.position.x, 0);
            monsterPicture.rectTransform.localScale = new Vector3(1.5f, 1.5f, 0);
        }
    }

    private void KeepHud()
    {
        DontDestroyOnLoad(playerHudDisplay);
    }
    private void Victory()
    {
        monsterValue = Random.Range(11, monsterSO.monsterKillValue);
        monsterXP = monsterSO.monsterKillXP;
        monsterKillValue.text = monsterValue.ToString();
        monsterKillXP.text = monsterXP.ToString();
        monsterSO.isDefeated = true;

    }
    public void ClaimVictory()
    {
        PersistantData.data.currency += monsterValue;
        PersistantData.data.experience += monsterXP;
        PersistantData.data.totalCompleteLevels++;
        //PersistantData.data.playerToken.SetActive(true);
        GameObject.FindGameObjectWithTag("Claim").GetComponent<Button>().enabled = false;
        //KeepHud();
        SceneManager.LoadScene("LevelMap");
        
    }
    public void CountTurn()
    {
            turnCounter++;
        for (int i = 0; i < abilityHolder.Length; i++)
        {
            if (abilityHolder[i].currentCount != 0)
            {
                abilityHolder[i].currentRemainder = turnCounter - abilityHolder[i].currentCount;
            }
        }
    }
    private IEnumerator DamagedReset()
    {
        yield return new WaitForSeconds(.5f);
        Destroy(GameObject.FindGameObjectWithTag("SlashEffect"));
        enemy.GetComponent<Animator>().SetBool("Damaged", false);
    }
    public void EnemyDamaged()
    {
        Vector2 particlePosition =new Vector2(6.5f, 2.5f);
        enemy.GetComponent<Animator>().SetBool("Damaged", true);
        Instantiate(slashEffect, particlePosition, Quaternion.identity);
        StartCoroutine(DamagedReset());
        

    }
    private IEnumerator SmallDelay()
    {
        soundManager.abilitySound.Play();
        yield return new WaitForSeconds(2f);
        particles = GameObject.FindGameObjectsWithTag("AbilityEffect");

        for (int j = 0; j < particles.Length; j++)
        {
            Destroy(particles[j]);
        }
        for (int i = 0; i < manaPotions.Length; i++)
        {
            manaPotions[i].GetComponent<Icon>().isMatched = true;
            Destroy(manaPotions[i]);
        }
        particles = null;
        manaPotions = null;
        board.RefillOnAbility();
        board.DestroyMatches();
        board.currentState = GameState.move;
    }
    
    public void UseRageAbility()
    {
        if (board.currentState == GameState.move)
        {
            board.currentState = GameState.wait;
            manaPotions = GameObject.FindGameObjectsWithTag("Attack");
            Debug.Log("Array Populated");
            for (int i = 0; i < manaPotions.Length; i++)
            {
                Instantiate(abilityEffect, manaPotions[i].transform.position, Quaternion.identity);
            }
            StartCoroutine(SmallDelay());
            
        }
    }
    public void UseHealAbility()
    {
        if (board.currentState == GameState.move)
        {
            board.currentState = GameState.wait;
           
        }
    }
    public void UseSacrificeAbility()
    {
        if (board.currentState == GameState.move)
        {
            PersistantData.data.health -= 5;
            monsterHealth -= 30;
            monsterHealthBar.value += 30;
            if (monsterHealth <= 0)
            {
                Victory();
            }
        }
    }

    private void UpdatePlayer()
    {
        if (board != null && playerHealthBar != null)
        {
            if (monsterRageCounter >= 1f)
            {
                playerHealthBar.value = -1;
            }
        }
    }

    public void DecreasMonsterHealth(int healthLoss)
    {
        //int monsterStartingHealth = monsterHealth;
        monsterHealth -= healthLoss;
        if (monsterHealth <= 0)
        {
            monsterHealthBar.value = monsterHealthBar.minValue;
            Victory();
        }
        UpdateBar();
    }
    public void IncreaseMonsterRage(int rageGain)
    {
        monsterRageCounter += rageGain;
        UpdateBar();
    }

    private IEnumerator DestroyParticles()
    {
        yield return new WaitForSeconds(1f);
        GameObject[] attackedParticles = GameObject.FindGameObjectsWithTag("AttackParticle");
        for (int i = 0; i < attackedParticles.Length; i++)
        {
            Destroy(attackedParticles[i]);
        }
        player.GetComponent<Animator>().SetBool("Damaged", false);
        attackedParticles = null;
    }
    private void UpdateBar()
    {
        if (board != null && monsterHealthBar != null)
        {
            //monsterHealthBar.maxValue = monsterHealth;
            monsterHealthBar.value = monsterHealthBar.maxValue - monsterHealth;
            monsterRageBar.value = monsterRageCounter / 10f;
           
            if (monsterRageBar.value >= 1)
            {
                //Do Attack
                monsterSound.clip = monsterSO.monsterAttackSound;
                Instantiate(playerDamagedPortrait, playerPortrait.transform.position, Quaternion.identity);
                Instantiate(playerDamagedEffect, playerImageLocation, Quaternion.identity);
                monsterSound.Play();
                attackCounter++;
                if (attackCounter >= 1)
                {
                    if (defending == true)
                    {
                        PersistantData.data.health = playerHealthBar.value - 3f;
                        player.GetComponent<Animator>().SetBool("Damaged", true);
                        attackCounter = 0;
                        defending = false;
                    }
                    else
                    {
                        PersistantData.data.health = playerHealthBar.value - 4f;
                        player.GetComponent<Animator>().SetBool("Damaged", true);
                        attackCounter = 0;
                    }
                }
                monsterRageBar.value = 0;
                monsterRageCounter = monsterRageCounter - 10;
                
                StartCoroutine(DestroyParticles());
               
                
            }
        }
    }
}
