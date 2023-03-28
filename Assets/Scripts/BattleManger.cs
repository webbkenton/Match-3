using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using UnityEngine.SceneManagement;

public class BattleManger : MonoBehaviour
{
    public bool defending;
    public bool perfectDefense;
    public bool penUltimateHealEffect;
    public bool ultimateHealEffect;
    public bool lesserHealEffect;
    public bool lesserSacEffect;
    public bool greaterSacEffect;
    public bool ultimateSacEffect;
    public bool rageEffect;
    private bool getInfoDone;

    public int ultimateHealCounter;
    public int lesserHealCounter;
    public int lesserSacCounter;
    public int greaterSacCounter;
    public int ultimateSacCounter;


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

    private Boardv2 board;
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

    public MonsterInfo monsterInfo;


    public GameObject[] heavyAttack;
    public GameObject[] magicAttack;
    private GameObject playerHudDisplay;
    public GameObject EnemyAbility0;
    public GameObject EnemyAbility1;
    public GameObject EnemyAbility2;
    public GameObject EnemyAbility3;

    public AbilitySO playerAbility1;
    public AbilitySO playerAbility2;
    public AbilitySO playerAbility3;

    public GameObject playerAbilityHolder;
    public GameObject enemyAbilityDescription;
    private GameObject match3Enemy;

    public AbilitySO selectedMonsterAbility;

    public string playerScene;






    private void Start()
    {
        player = PersistantData.data.playerToken;
        //GetMonsterInfo();
    }

    public void GetInfo()
    {
        if (this.gameObject.activeInHierarchy && player.GetComponent<MoveTowardsTest>().inCombat == true)
        {
            playerScene = GameObject.FindGameObjectWithTag("Player").GetComponent<LevelTracker>().currentLevel;

            playerHudDisplay = GameObject.FindGameObjectWithTag("PlayerHud");
            currencyManager = GameObject.FindGameObjectWithTag("CurrencyManager").GetComponent<CurrencyManager>();
            turnCounter = 0;
            playerImageLocation = new Vector2(-4f, 5.5f);
            abilityHolder = FindObjectsOfType<AbilityHolder>();

            board = FindObjectOfType<Boardv2>();
            soundManager = FindObjectOfType<SoundManager>();
            player = GameObject.FindGameObjectWithTag("Player");
            GetPlayerInfo();
            Destroy(GameObject.FindGameObjectWithTag("OverworldObject"));
            enemy = player.GetComponent<LevelTracker>().enemy;
            match3Enemy = GameObject.FindGameObjectWithTag("Match3Enemy");
            getInfoDone = true;
        }
    }
    public void GetMonsterInfo()
    {
        GetInfo();
        monsterSO = enemy.GetComponent<MonsterInfo>().monster;
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
    }
    private void GetPlayerInfo()
    {         
        playerHealthBar.value = PersistantData.data.health;
        playerAbilityBar.value = PersistantData.data.mana;
    }

    private void Update()
    {
        GetInfo();
        //GetMonsterInfo();
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
        //MonsterAbilities();
        SetPlayerAbilities();
        UltimateHealEffect();
        LesserHealEffect();
        LesserSacEffect();
        GreaterSacEffect();
        UltimateSacEffect();
    }

    private void UltimateSacEffect()
    {
        if (ultimateSacEffect)
        {
            if (turnCounter == ultimateSacCounter)
            {
                if (monsterHealth >= 1)
                {
                    PersistantData.data.health -= 5;
                    ultimateSacCounter++;  
                }
                else if (monsterHealth <= 0)
                {
                    PersistantData.data.health = PersistantData.data.maxHealth;
                }
            }
        }
    }
    private void GreaterSacEffect()
    {
        if (greaterSacEffect)
        {
            if (turnCounter == greaterSacCounter)
            {
                PersistantData.data.health -= Random.Range(0, 3);
                monsterHealth -= Random.Range(4, 8);
            }
        }
    }
    private void LesserSacEffect()
    {
        if(lesserSacEffect)
        {
            if (turnCounter == lesserSacCounter)
            {
                monsterHealth -= 3;
                lesserSacCounter++;
            }
        }
    }
    private void LesserHealEffect()
    {
        if (lesserHealEffect)
        {
            if (turnCounter == lesserHealCounter)
            {
                PersistantData.data.health += 2;
                lesserHealCounter++;
            }
        }
    }
    private void UltimateHealEffect()
    {
        if (ultimateHealEffect)
        {
            if (turnCounter == ultimateHealCounter)
            {
                PersistantData.data.health += 10;
                monsterHealth -= 10;
                ultimateHealCounter++;
            }
        }
    }
    private void SetPlayerAbilities()
    {
        if (PersistantData.data.currentAbilities.Count == 1)
        {
            playerAbility1 = PersistantData.data.currentAbilities[0];
            GameObject.FindGameObjectWithTag("PlayerAbility1").GetComponent<AbilityHolder>().abilitySO = playerAbility1;
            //playerAbilityHolder.GetComponentInChildren<AbilityHolder>().abilitySO = playerAbility1;
            if (GameObject.FindGameObjectWithTag("PlayerAbility2"))
            {
                GameObject.FindGameObjectWithTag("PlayerAbility2").SetActive(false);
                GameObject.FindGameObjectWithTag("PlayerAbility3").SetActive(false);
            }
            else
            {
                return;
            }
        }
        else if (PersistantData.data.currentAbilities.Count == 2)
        {
            playerAbility1 = PersistantData.data.currentAbilities[0];
            playerAbility2 = PersistantData.data.currentAbilities[1];
            GameObject.FindGameObjectWithTag("PlayerAbility1").GetComponent<AbilityHolder>().abilitySO = playerAbility1;
            GameObject.FindGameObjectWithTag("PlayerAbility2").GetComponent<AbilityHolder>().abilitySO = playerAbility2;
            if (GameObject.FindGameObjectWithTag("PlayerAbility3"))
            {
                GameObject.FindGameObjectWithTag("PlayerAbility3").SetActive(false);
            }

        }
        else if (PersistantData.data.currentAbilities.Count >= 3)
        {
            playerAbility1 = PersistantData.data.currentAbilities[0];
            playerAbility2 = PersistantData.data.currentAbilities[1];
            playerAbility3 = PersistantData.data.currentAbilities[2];
            GameObject.FindGameObjectWithTag("PlayerAbility1").GetComponent<AbilityHolder>().abilitySO = playerAbility1;
            GameObject.FindGameObjectWithTag("PlayerAbility2").GetComponent<AbilityHolder>().abilitySO = playerAbility2;
            GameObject.FindGameObjectWithTag("PlayerAbility3").GetComponent<AbilityHolder>().abilitySO = playerAbility3;
        }
        else
        {
            playerAbilityHolder.SetActive(false);
        }
    }
    //private void MonsterAbilities()
    //{
    //    if (monsterSO.hasAbilities)
    //    {
    //        if (monsterSO.numberOfAbilities == 4)
    //        {
    //            EnemyAbility0.SetActive(true);
    //            EnemyAbility0.GetComponentInChildren<Image>().sprite = monsterInfo.monsterAbilties[0].abilityIcon;
    //            EnemyAbility0.GetComponentInChildren<MonsterAbilityHolder>().ability = monsterInfo.monsterAbilties[0];
    //            EnemyAbility1.SetActive(true);
    //            EnemyAbility1.GetComponentInChildren<Image>().sprite = monsterInfo.monsterAbilties[1].abilityIcon;
    //            EnemyAbility1.GetComponentInChildren<MonsterAbilityHolder>().ability = monsterInfo.monsterAbilties[1];
    //            EnemyAbility2.SetActive(true);
    //            EnemyAbility2.GetComponentInChildren<Image>().sprite = monsterInfo.monsterAbilties[2].abilityIcon;
    //            EnemyAbility2.GetComponentInChildren<MonsterAbilityHolder>().ability = monsterInfo.monsterAbilties[2];
    //            EnemyAbility3.SetActive(true);
    //            EnemyAbility3.GetComponentInChildren<Image>().sprite = monsterInfo.monsterAbilties[3].abilityIcon;
    //            EnemyAbility3.GetComponentInChildren<MonsterAbilityHolder>().ability = monsterInfo.monsterAbilties[3];

    //        }
    //        if (monsterSO.numberOfAbilities == 3)
    //        {
    //            EnemyAbility0.SetActive(true);
    //            EnemyAbility0.GetComponentInChildren<Image>().sprite = monsterInfo.monsterAbilties[0].abilityIcon;
    //            EnemyAbility0.GetComponentInChildren<MonsterAbilityHolder>().ability = monsterInfo.monsterAbilties[0];
    //            EnemyAbility1.SetActive(true);
    //            EnemyAbility1.GetComponentInChildren<Image>().sprite = monsterInfo.monsterAbilties[1].abilityIcon;
    //            EnemyAbility1.GetComponentInChildren<MonsterAbilityHolder>().ability = monsterInfo.monsterAbilties[1];
    //            EnemyAbility2.SetActive(true);
    //            EnemyAbility2.GetComponentInChildren<Image>().sprite = monsterInfo.monsterAbilties[2].abilityIcon;
    //            EnemyAbility2.GetComponentInChildren<MonsterAbilityHolder>().ability = monsterInfo.monsterAbilties[2];
    //        }
    //        if (monsterSO.numberOfAbilities == 2)
    //        {
    //            EnemyAbility0.SetActive(true);
    //            EnemyAbility0.GetComponentInChildren<Image>().sprite = monsterInfo.monsterAbilties[0].abilityIcon;
    //            EnemyAbility0.GetComponentInChildren<MonsterAbilityHolder>().ability = monsterInfo.monsterAbilties[0];
    //            EnemyAbility1.SetActive(true);
    //            EnemyAbility1.GetComponentInChildren<Image>().sprite = monsterInfo.monsterAbilties[1].abilityIcon;
    //            EnemyAbility1.GetComponentInChildren<MonsterAbilityHolder>().ability = monsterInfo.monsterAbilties[1];

    //        }
    //        if (monsterSO.numberOfAbilities == 1)
    //        {
    //            EnemyAbility0.SetActive(true);
    //            EnemyAbility0.GetComponentInChildren<Image>().sprite = monsterInfo.monsterAbilties[0].abilityIcon;
    //            EnemyAbility0.GetComponentInChildren<MonsterAbilityHolder>().ability = monsterInfo.monsterAbilties[0];
    //            //enemyAbilityDescription.GetComponentInChildren<Text>().text = monsterInfo.monsterAbilties[0].abilityDescription;
    //            //EnemyAbility0.GetComponent<Image>().sprite;
    //        }



    //    }
    //    else
    //    {
    //        return;
    //    }
    //}
    public void DisplayMonsterAbility()
    {
        if (selectedMonsterAbility != null)
        {
            enemyAbilityDescription.GetComponentInChildren<Text>().text = selectedMonsterAbility.abilityDescription;

            if (enemyAbilityDescription.activeInHierarchy)
            {
                enemyAbilityDescription.SetActive(false);
            }
            else
            {
                enemyAbilityDescription.SetActive(true);
            }
        }
    }
    public void Victory()
    {
        monsterValue = Random.Range(11, monsterSO.monsterKillValue);
        monsterXP = monsterSO.monsterKillXP;
        monsterKillValue.text = monsterValue.ToString();
        monsterKillXP.text = monsterXP.ToString();
        monsterSO.isDefeated = true;
        if (!PersistantData.data.defeatedMonsters.Contains(monsterSO))
        {
            PersistantData.data.defeatedMonsters.Add(monsterSO);
        }
        if (perfectDefense)
        {
            perfectDefense = false;
        }

    }
    public void ClaimVictory()
    {
        PersistantData.data.currency += monsterValue;
        PersistantData.data.experience += monsterXP;
        PersistantData.data.totalCompleteLevels++;
        //PersistantData.data.gameObject.SetActiveRecursively(true);
        PersistantData.data.playerToken = GameObject.FindGameObjectWithTag("Player");
        //player.GetComponent<LevelTracker>().enemy.GetComponent<FightTrigger>().defeated = true;
        player.GetComponent<MoveTowardsTest>().inCombat = false;
        //GameObject.FindGameObjectWithTag("Claim").GetComponent<Button>().enabled = false;
        endPanel.SetActive(false);
        GameObject.FindGameObjectWithTag("UIManager").GetComponent<MenuScript>().Match3Board.SetActive(false);
        GameObject.FindGameObjectWithTag("UIManager").GetComponent<MenuScript>().Match3UI.SetActive(false);
        //StartCoroutine(PersistantData.data.TransitionIn());
        new WaitForSeconds(3f);
        //GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>().RestoreTheMap();
        //player.GetComponent<LevelTracker>().enemy.GetComponent<FightTrigger>().inCombat = false;
        player.GetComponent<PointsTracker>().points++;
        PersistantData.data.returnToMap = true;
        SceneManager.LoadScene(playerScene);
        
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
        //match3Enemy.GetComponent<Animator>().SetBool("Damaged", false);
    }
    public void EnemyDamaged()
    {
        Vector2 particlePosition =new Vector2(6.5f, 2.5f);
        //match3Enemy.GetComponent<Animator>().SetBool("Damaged", true);
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
            for (int j = 0; j < heavyAttack.Length; j++)
            {
                for (int k = 0; k < magicAttack.Length; k++)
                {
                    manaPotions[i].GetComponent<Icon>().isMatched = true;
                    heavyAttack[j].GetComponent<Icon>().isMatched = true;
                    magicAttack[k].GetComponent<Icon>().isMatched = true;
                    Destroy(manaPotions[i]);
                    Destroy(heavyAttack[j]);
                    Destroy(magicAttack[k]);
                }
            }
            
        }
        particles = null;
        manaPotions = null;
        heavyAttack = null;
        magicAttack = null;
        //board.RefillOnAbility();
        board.DestroyMatches();
        board.currentState = GameState.move;
    }
    
    public void UseRageAbility()
    {
        if (board.currentState == GameState.move)
        {
            board.currentState = GameState.wait;
            manaPotions = GameObject.FindGameObjectsWithTag("Attack");
            heavyAttack = GameObject.FindGameObjectsWithTag("Heavy Attack");
            magicAttack = GameObject.FindGameObjectsWithTag("Magic");
            
            Debug.Log("Array Populated");
            for (int i = 0; i < manaPotions.Length; i++)
            {
                for (int j = 0; j < heavyAttack.Length; j++)
                {
                    for (int k = 0; k < magicAttack.Length; k++)
                    {
                        Instantiate(abilityEffect, manaPotions[i].transform.position, Quaternion.identity);
                        Instantiate(abilityEffect, heavyAttack[j].transform.position, Quaternion.identity);
                        Instantiate(abilityEffect, magicAttack[k].transform.position, Quaternion.identity);

                    }
                }
                
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
    /*public void UseSacrificeAbility()
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
    }*/

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

    public void DecreaseMonsterHealth(int healthLoss)
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
        //UpdateBar();
    }

    private IEnumerator DestroyParticles()
    {
        yield return new WaitForSeconds(1f);
        GameObject[] attackedParticles = GameObject.FindGameObjectsWithTag("AttackParticle");
        for (int i = 0; i < attackedParticles.Length; i++)
        {
            Destroy(attackedParticles[i]);
        }
        //player.GetComponent<Animator>().SetBool("Damaged", false);
        attackedParticles = null;
    }
    private void UpdateBar()
    {
        if (board != null && monsterHealthBar != null)
        {
            //monsterHealthBar.maxValue = monsterHealth;
            monsterHealthBar.value = monsterHealth;
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
                    if (defending == true || perfectDefense == true)
                    {
                        PersistantData.data.health = playerHealthBar.value - 3f;
                        //player.GetComponent<Animator>().SetBool("Damaged", true);
                        attackCounter = 0;
                        defending = false;
                        if (lesserSacEffect)
                        {
                            PersistantData.data.health -= 2f;
                        }
                    }
                    else
                    {
                        PersistantData.data.health = playerHealthBar.value - 4f;
                        //player.GetComponent<Animator>().SetBool("Damaged", true);
                        attackCounter = 0;
                        if (lesserSacEffect)
                        {
                            PersistantData.data.health -= 2f;
                        }
                    }
                }
                monsterRageBar.value = 0;
                monsterRageCounter = monsterRageCounter - 10;
                
                StartCoroutine(DestroyParticles());
               
                
            }
        }
    }
}
