using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class BattleManger : MonoBehaviour
{
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
    //public Image ability1;
    //public Image ability2;
    //public Image ability3;
    //private int abilityCost;
    private GameObject enemy;
    public GameObject slashEffect;
    public GameObject playerDamagedPortrait;
    public GameObject playerPortrait;
    public GameObject abilityEffect;
    public GameObject playerDamagedEffect;
    private SoundManager soundManager;
    public GameObject playerImage;
    private Vector2 playerImageLocation;
    public GameObject player;
    //public GameObject[,] icons;
    //private int iconEffectValue;
    // Start is called before the first frame update
    void Start()
    {
        playerImageLocation = new Vector2(-4f, 5.5f);
        //ability1.color = new Color (.5f, .5f, .5f, 1f);
        //ability2.color = new Color(.5f, .5f, .5f, 1f);
        //ability3.color = new Color(.5f, .5f, .5f, 1f);
        //abilityCost = 40;

        enemy = GameObject.FindGameObjectWithTag("Enemy");
        board = FindObjectOfType<Board>();
        soundManager = FindObjectOfType<SoundManager>();
        //iconEffectValue = board.currentIcon.GetComponent<IconSO>().EffectValue;
        monsterType = monsterSO.monsterType;
        monsterName.text = monsterSO.monsterName;
        monsterHealth = monsterSO.monsterHealth;
        monsterRageCounter = monsterSO.rageCounter;
        monsterPicture.sprite = monsterSO.monsterImage;
        monsterHealthBar.maxValue = monsterHealth;
        monsterSound.clip = monsterSO.monsterNoise;
        monsterSound.Play();

    }

    private void Update()
    {
        monsterHealthText.text = monsterHealth.ToString();
        monsterRageCounterText.text = monsterRageCounter.ToString();
        playerHealth.text = playerHealthBar.value.ToString();
        playerAbilityAmount.text = playerAbilityBar.value.ToString();
        if (monsterHealth <= 0)
        {
            endPanel.SetActive(true);
        }
        //AbilityColor();
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
    /*private void AbilityColor()
    {
        if (playerAbilityBar.value >= abilityCost)
        {
            ability1.color = new Color(1f, 1f, 1f, 1f);
            ability2.color = new Color(1f, 1f, 1f, 1f);
            ability3.color = new Color(1f, 1f, 1f, 1f);
        }
        else
        {
            ability1.color = new Color(.5f, .5f, .5f, 1f);
            ability2.color = new Color(.5f, .5f, .5f, 1f);
            ability3.color = new Color(.5f, .5f, .5f, 1f);
        }

    }*/

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
            manaPotions = GameObject.FindGameObjectsWithTag("Gems");
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
            manaPotions = GameObject.FindGameObjectsWithTag("ManaPotion");
            Debug.Log("Array Populated");
            for (int i = 0; i < manaPotions.Length; i++)
            {
                Instantiate(abilityEffect, manaPotions[i].transform.position, Quaternion.identity);
            }
            StartCoroutine(SmallDelay());
        }
    }
    public void UseSacrificeAbility()
    {
        if (board.currentState == GameState.move)
        {
            board.currentState = GameState.wait;
            manaPotions = GameObject.FindGameObjectsWithTag("ManaPotion");
            Debug.Log("Array Populated");
            for (int i = 0; i < manaPotions.Length; i++)
            {
                Instantiate(abilityEffect, manaPotions[i].transform.position, Quaternion.identity);
            }
            StartCoroutine(SmallDelay());
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
            if (monsterHealth <= 0)
            {
                monsterHealthBar.value = monsterHealthBar.minValue;
            }
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
                    playerHealthBar.value = playerHealthBar.value -4f;
                    player.GetComponent<Animator>().SetBool("Damaged", true);
                    attackCounter = 0;
                }
                monsterRageBar.value = 0;
                monsterRageCounter = monsterRageCounter - 10;
                StartCoroutine(DestroyParticles());
                
            }
        }
    }
}
