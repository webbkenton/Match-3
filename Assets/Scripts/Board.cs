﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum GameState
{
    wait,
    move
}

public enum TileKind
{
    Breakable,
    Blank,
    Normal
}
[System.Serializable]
public class TileType
{
    public int x;
    public int y;
    public TileKind tileKind;
}

public class Board : MonoBehaviour
{
    public GameState currentState = GameState.move;

    public int height;
    public int width;
    public int offSet;
    //public float refillDelay = 0.25f;
    private bool[,] blankSpaces;
    private bool shuffling;


    public GameObject tilePrefab;
    public GameObject destoryEffect;
    public GameObject breakableTilePrefab;
    public TileType[] boardLayout;
    private BackgroundTile[,] breakableTiles;
    public GameObject[] icons;
    public GameObject[,] allIcons;
    public Icon currentIcon;
    public int baseTileValue = 20;
    public int streakValue = 1;
    private ScoreManager scoreManger;
    private SoundManager soundManager;
    private BattleManger battleManger;
    private CurrencyManager currencyManager;

    public GameObject adjacentParticle;
    public GameObject deadlockedButton;
    //private ScriptableObject iconSO;
    public int[] scoreGoals;

    public FindMatches findMatches;
    // Start is called before the first frame update

    public int maxRows;
    public int maxColumns;

    //private void Awake()
    //{
    //    StartCoroutine(PersistantData.data.TransitionOut());
    //}
    void Start()
    {
        battleManger = FindObjectOfType<BattleManger>();
        soundManager = FindObjectOfType<SoundManager>();
        scoreManger = FindObjectOfType<ScoreManager>();
        currencyManager = FindObjectOfType<CurrencyManager>();
        breakableTiles = new BackgroundTile[width, height];
        findMatches = FindObjectOfType<FindMatches>();
        //allTiles = new BackgroundTile[width, height];
        blankSpaces = new bool[width, height];
        allIcons = new GameObject[width, height];
        Setup();
        
    }


    public void GenerateBlankSpaces()
    {
        for (int i = 0; i < boardLayout.Length; i++)
        {
            if (boardLayout[i].tileKind == TileKind.Blank)
            {
                blankSpaces[boardLayout[i].x, boardLayout[i].y] = true;
                Debug.Log(blankSpaces[boardLayout[i].x, boardLayout[i].y]);
            }
        }
    }

    public void GenerateBreakableTiles()
    {
        for (int i = 0; i < boardLayout.Length; i++)
        {
            if (boardLayout[i].tileKind == TileKind.Breakable)
            {
                Vector2 tempPosition = new Vector2(boardLayout[i].x, boardLayout[i].y);
                GameObject tile = Instantiate(breakableTilePrefab, tempPosition, Quaternion.identity);
                breakableTiles[boardLayout[i].x, boardLayout[i].y] = tile.GetComponent<BackgroundTile>();
            }
        }
    }
    private void Update()
    {
        //CheckToMakeMoreBombs();
    }

    private void Setup()
    {
        //GenerateBoardTiles();
        GenerateBlankSpaces();
        //GenerateBreakableTiles();
        
        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++)
            {
                if (!blankSpaces[i, j])
                {
                    Vector2 tempPosition = new Vector2(i, j + offSet);
                    Vector2 tempBoard = new Vector2(i, j);
                    GameObject backgroundTile = Instantiate(tilePrefab, tempBoard, Quaternion.identity) as GameObject;
                    backgroundTile.transform.parent = this.transform;
                    backgroundTile.name = "(" + i + ", " + j + " )";
                    int iconToUse = Random.Range(0, icons.Length);
                    int maxIteration = 0;
                    while (MatchesAt(i, j, icons[iconToUse]) && maxIteration < 100)
                    {
                        iconToUse = Random.Range(0, icons.Length);
                        maxIteration++;
                    }
                    maxIteration = 0;
                    GameObject icon = Instantiate(icons[iconToUse], tempPosition, Quaternion.identity);
                    icon.GetComponent<Icon>().row = j;
                    icon.GetComponent<Icon>().column = i;
                    icon.transform.parent = this.transform;
                    icon.name = "(" + i + ", " + j + " )";
                    allIcons[i, j] = icon;
                }
            }
        }
        
    }
    private bool MatchesAt(int column, int row, GameObject piece)
    {
        if (column > 1 && row > 1)
        {
            if (allIcons[column - 1, row] != null && allIcons[column - 2, row] != null)
            {
                if (allIcons[column - 1, row].tag == piece.tag && allIcons[column - 2, row].tag == piece.tag)
                {
                    return true;
                }
            }
            if (allIcons[column, row - 1] != null && allIcons[column, row - 2] != null)
            {
                if (allIcons[column, row - 1].tag == piece.tag && allIcons[column, row - 2].tag == piece.tag)
                {
                    return true;
                }
            }
        }
        else if (column <= 1 || row <= 1)
        {
            if (row > 1)
            {
                if (allIcons[column, row - 1] != null && allIcons[column, row - 2] != null)
                {
                    if (allIcons[column, row - 1].tag == piece.tag && allIcons[column, row - 2].tag == piece.tag)
                    {
                        return true;
                    }
                }
            }
            if (column > 1)
            {
                if (allIcons[column - 1, row] != null && allIcons[column - 2, row] != null)
                {
                    if (allIcons[column - 1, row].tag == piece.tag && allIcons[column - 2, row].tag == piece.tag)
                    {
                        return true;
                    }
                }
            }
        }
        return false;
    }

    private int ColumnOrRow()
    { //make a copy of current matches
        List<GameObject> matchCopy = findMatches.currentMatches as List<GameObject>;

        //Cycle through all of match copy and decide if a bomb needs to made
        // 4= Adjacent, 5Not in a row = Star Bomb, 5in a row = type bomb
        for (int i = 0; i < matchCopy.Count; i++)
        {
            //Store this Dot
            Icon thisIcon = matchCopy[i].GetComponent<Icon>();
            int column = thisIcon.column;
            int row = thisIcon.row;
            int columnMatch = 0;
            int rowMatch = 0;
            //Cycle though the remaining tiles
            for (int j = 0; j < matchCopy.Count; j++)
            {
                Icon nextIcon = matchCopy[j].GetComponent<Icon>();
                if (nextIcon == thisIcon)
                {
                    continue;
                }
                if (nextIcon.column == thisIcon.column && nextIcon.CompareTag(thisIcon.tag))
                {
                    columnMatch++;
                }
                if (nextIcon.row == thisIcon.row && nextIcon.CompareTag(thisIcon.tag))
                {
                    rowMatch++;
                }
            }
            //Return 3 if Adjacent
            //Return 2 if Star
            //Return 1 if Type Bomb
            if (columnMatch == 4 || rowMatch == 4)
            {
                return 1;
            }
            else if (columnMatch >= 2 && rowMatch >= 2)
            {
                if (columnMatch < 4 || rowMatch < 4)
                {
                    return 2;
                }
            }
            else if (columnMatch >= 3 || rowMatch >= 3)
            {
                if (columnMatch < 4 || rowMatch < 4)
                {
                    if (columnMatch == 3 || rowMatch == 3)
                    {
                        return 3;
                    }
                }
            }
            
        }
        return 0;
    }
                         
    
    private void CheckToMakeBombs()
    {
        //How many objects are in Findmatches() currentMatches()
        if (findMatches.currentMatches.Count > 3)
        {
            //What type of match
            //So the reason we dont make matches Is because we are only checking currentIcon. During fillBoard we need to check every icon.
            int typeOfMatch = ColumnOrRow();
            if (typeOfMatch == 1)
            {
                if (currentIcon != null)
                {
                    if (currentIcon.isMatched)
                    {
                        if (!currentIcon.isTypeBomb)
                        {
                            currentIcon.isMatched = false;
                            currentIcon.MakeTypeBomb();
                            battleManger.EnemyDamaged();
                        }
                    }
                    else
                    {
                        if (currentIcon.otherIcon != null)
                        {
                            Icon otherIcon = currentIcon.otherIcon.GetComponent<Icon>();
                            if (otherIcon.isMatched)
                            {
                                if (!otherIcon.isTypeBomb)
                                {
                                    otherIcon.isMatched = false;
                                    otherIcon.MakeTypeBomb();
                                    battleManger.EnemyDamaged();
                                }
                            }
                        }
                    }
                }
            }
            else if (typeOfMatch == 2)
            {
                if (currentIcon != null)
                {
                    if (currentIcon.isMatched)
                    {
                            currentIcon.isMatched = false;
                            currentIcon.MakeStarBomb();
                            battleManger.EnemyDamaged();
                            //Debug.Log("Star Bomb Made");

                    }
                    else
                    {
                        if (currentIcon.otherIcon != null)
                        {
                            Icon otherIcon = currentIcon.otherIcon.GetComponent<Icon>();
                            if (otherIcon.isMatched)
                            {
                                if (!otherIcon.isStarBomb)
                                {
                                    otherIcon.isMatched = false;
                                    otherIcon.MakeStarBomb();
                                    battleManger.EnemyDamaged();
                                }
                            }
                        }
                    }
                }
            }
            else if (typeOfMatch == 3)
            {
                if (currentIcon != null)
                {
                    if (currentIcon.isMatched)
                    {
                        if (!currentIcon.isAdjacentBomb)
                        {
                            currentIcon.isMatched = false;
                            currentIcon.MakeAdjacentBomb();
                            battleManger.EnemyDamaged();
                        }
                        else if (currentIcon.otherIcon != null)
                        {
                            Icon otherIcon = currentIcon.otherIcon.GetComponent<Icon>();
                            if (otherIcon.isMatched)
                            {
                                    otherIcon.isMatched = false;
                                    otherIcon.MakeAdjacentBomb();
                                    battleManger.EnemyDamaged();
                            }
                        }
                    }

                }

            }
            
        }
    }   
    private void DestroyMatchesAt(int column, int row)
    {
        if (allIcons[column, row].GetComponent<Icon>().isMatched)
        {
            //Debug.Log("Board:503 Icon.ismatched");
            if (breakableTiles[column, row] != null)
            {
                breakableTiles[column, row].TakeDamage(1);
                if (breakableTiles[column, row].hitPoints <= 0)
                {
                    breakableTiles[column, row] = null;
                }
            }
            CheckGemType(column, row);
            GameObject particle = Instantiate(destoryEffect, allIcons[column, row].transform.position, Quaternion.identity);
            Destroy(particle, .5f);
            Destroy(allIcons[column, row]);
            scoreManger.IncreaseScore(baseTileValue * streakValue);
            allIcons[column, row] = null;
            currentIcon = null;
        }
    }

    private void CheckGemType(int column, int row)
    {
        //Debug.Log(allIcons[column,row].tag);
        if (allIcons[column, row].tag == "Gem")
        {
            soundManager.PlayGem();
            PersistantData.data.currency += (3);
        }
        else if (allIcons[column, row].tag == "Attack")
        {
            // Do the base attack
            soundManager.PlaySlash();
            battleManger.DecreaseMonsterHealth(baseTileValue / 10);
            currencyManager.IncreaseCurrency(1);
            battleManger.IncreaseMonsterRage(1);
        }
        else if (allIcons[column, row].tag == "Heavy Attack")
        {
            // Do base attack * 1.5
            soundManager.PlayHeavy();
            battleManger.DecreaseMonsterHealth(baseTileValue / 4);
            PersistantData.data.currency += (1);
            battleManger.IncreaseMonsterRage(1);
            if (battleManger.rageEffect)
            {
                battleManger.DecreaseMonsterHealth(baseTileValue / 4);
                battleManger.rageEffect = false;
            }
            else
            {
                return;
            }
        }
        else if (allIcons[column, row].tag == "HealthPotion")
        {
            //restore 1 point each
            soundManager.PlayPotions();
            PersistantData.data.health++;
            PersistantData.data.currency += (1);
            if (battleManger.penUltimateHealEffect)
            {
                PersistantData.data.health += 3;
            }
        }
        else if (allIcons[column, row].tag == "Defend")
        {
            battleManger.defending = true;
            //if(Defending){enemy damge -1}
            soundManager.PlayDefend();
            PersistantData.data.currency += (1);
        }
        else if (allIcons[column, row].tag == "ManaPotion")
        {
            //Mana bar +1 point each
            soundManager.PlayPotions();
            PersistantData.data.mana += 4;
            PersistantData.data.currency += (1);
            if (battleManger.penUltimateHealEffect)
            {
                PersistantData.data.mana += 3;
            }
        }
        else if (allIcons[column, row].tag == "Magic")
        {
            soundManager.PlayMagic();
            battleManger.DecreaseMonsterHealth(baseTileValue / 10);
            PersistantData.data.currency += (1);
            battleManger.IncreaseMonsterRage(1);
        }
        else
        {
            PersistantData.data.currency += (1);
        }


    }
    public void DestroyMatches()
    {
        //Debug.Log("Checking For Bombs");
        if (findMatches.currentMatches.Count >= 3) //Findmatches.count is > 3 so destroy matches should have been called
        {
            CheckToMakeBombs();
            //CheckToMakeMoreBombs();
        }
        findMatches.currentMatches.Clear();
        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++)
            {
                if (allIcons[i, j] != null)
                { 
                    DestroyMatchesAt(i, j);
                }
            }

        }
        findMatches.currentMatches.Clear();
        StartCoroutine(DecreaseRowCo2());
    }

    private IEnumerator DecreaseRowCo2()
    {
        //This one should delay the Rows decreasing
        yield return new WaitForSeconds(.1f);
        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++)
            {
                if (!blankSpaces[i, j] && allIcons[i, j] == null)
                {
                    for (int k = j + 1; k < height; k++)
                    {
                        if (allIcons[i, k] != null)
                        {
                            allIcons[i, k].GetComponent<Icon>().row = j;
                            allIcons[i, k] = null;

                            break;
                        }
                    }
                }

            }
        }
        //This one controls how long it takes before the board begins to refill
        yield return new WaitForSeconds(.35f);
        StartCoroutine(FillBoardCo());
        findMatches.FindAllMatches();
    }

    public void RefillOnAbility()
    {
        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++)
            {
                if (allIcons[i, j] == null)
                {
                    for (int k = j + 1; k < height; k++)
                    {
                        if (allIcons[i, k] != null)
                        {
                            allIcons[i, k].GetComponent<Icon>().row = j;
                            allIcons[i, k] = null;
                        }
                    }
                    }
            }
        }
        if (findMatches.currentMatches != null)
        {
            //Debug.Log("Starting RefillAbility()");
            RefillAbility();
            //StartCoroutine(FillBoardCo());
        }
    }

    private void RefillAbility()
    {
        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++)
            {
                if (allIcons[i, j] == null && !blankSpaces[i, j])
                {
                    Vector2 tempPostion = new Vector2(i, j + offSet);
                    GameObject piece = Instantiate(battleManger.heavyAttackPrefab, allIcons[i,j].transform.position, Quaternion.identity) ;
                    allIcons[i, j] = piece;
                    piece.GetComponent<Icon>().row = j;
                    piece.GetComponent<Icon>().column = i;
                }
            }
        }
    }

    private void RefillBoard()
    {
        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++)
            {
                if (allIcons[i, j] == null)
                {
                    Vector2 tempPostion = new Vector2(i, j + offSet);
                    //Debug.Log(tempPostion);
                    int iconToUse = Random.Range(0, icons.Length);
                    int maxIterations = 0;
                    while (MatchesAt(i, j, icons[iconToUse]) && maxIterations < 100)
                    {
                        maxIterations++;
                        iconToUse = Random.Range(0, icons.Length);
                    }
                    maxIterations = 0;
                    GameObject piece = Instantiate(icons[iconToUse], tempPostion, Quaternion.identity);
                    allIcons[i, j] = piece;
                    piece.GetComponent<Icon>().row = j;
                    piece.GetComponent<Icon>().column = i;

                    if (findMatches.currentMatches.Count >= 0)
                    {
                        findMatches.FindAllMatches();
                        StartCoroutine(FillBoardCo());
                        CheckToMakeBombs();
                    }
                }
            }
        }
    }

    public bool MatchesOnBoard()
    {
        findMatches.FindAllMatches();
        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++)
            {
                if (allIcons[i, j] != null)
                {
                    if (allIcons[i, j].GetComponent<Icon>().isMatched)
                    {
                        //Debug.Log("Match");
                        return true;
                    }
                }
            }
        }
        return false;
    }

    //private IEnumerator FillBoardCo()
    //{
    //    yield return new WaitForSeconds(.2f);
    //    RefillBoard();
    //    //Debug.Log("Board Refilled");

    //    while (MatchesOnBoard())
    //    {
    //        //What does this one do
    //        yield return new WaitForSeconds(.2f);
    //        //Debug.Log("Matches On Board Started");
    //        streakValue++;
    //        DestroyMatches();
    //        yield return new WaitForSeconds(.1f);
    //        MatchesOnBoard();

    //    }

    //    //findMatches.currentMatches.Clear();
    //    //Debug.Log("Cleared Matches");
    //    currentIcon = null;


    //    if (IsDeadLocked())
    //    {
    //        yield return new WaitForSeconds(1f);
    //        ShuffleBoard();
    //        Debug.Log("DeadLocked!!!");
    //    }
    //    yield return new WaitForSeconds(.1f);
    //    currentState = GameState.move;
    //    streakValue = 1;

    //}

    private IEnumerator FillBoardCo()
    {
        RefillBoard();
        yield return new WaitForSeconds(.25f);

        while (MatchesOnBoard())
        {
            yield return new WaitForSeconds(.25f);
            DestroyMatches();
        }
        findMatches.currentMatches.Clear();
        currentIcon = null;
        yield return new WaitForSeconds(.35f);
        currentState = GameState.move;
        shuffling = false;

        yield return new WaitForSeconds(5f);
        if (IsDeadLocked())
        {
            deadlockedButton.SetActive(true);
            Debug.Log("Deadlocked");
        }
    }

    private void SwitchTile(int column, int row, Vector2 direction)
    {
        //Take the second piece and save it in a holder
        GameObject holder = allIcons[column + (int)direction.x, row + (int)direction.y] as GameObject;
        //switching the first dot to be the second position
        allIcons[column + (int)direction.x, row + (int)direction.y] = allIcons[column, row];
        //Set the first dot to be the second dot
        allIcons[column, row] = holder;
    }

    private bool CheckForMatches()
    {
        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++)
            {
                if (allIcons[i, j] != null)
                {
                    if (i < width - 2)
                    {
                        //check if the dots to the right and 2 to the right exist
                        if (allIcons[i + 1, j] != null && allIcons[i + 2, j] != null)
                        {
                            if (allIcons[i + 1, j].tag == allIcons[i, j].tag
                                && allIcons[i + 2, j].tag == allIcons[i, j].tag)

                            {
                                return true;
                            }
                        }
                    }

                    if (j < height - 2)
                    {
                        //check the updots 1 and 2
                        if (allIcons[i, j + 1] != null && allIcons[i, j + 2] != null)
                        {
                            if (allIcons[i, j + 1].tag == allIcons[i, j].tag && allIcons[i, j + 2].tag == allIcons[i, j].tag)
                            {
                                return true;
                            }
                        }
                    }
                 
                }
            }
        }
        return false;
    }

    public bool SwitchAndCheck(int column, int row, Vector2 direction)
    {
        SwitchTile(column, row, direction);
        if (CheckForMatches())
        {
            SwitchTile(column, row, direction);
            return true;
        }
        SwitchTile(column, row, direction);
        return false;
    }

    private bool IsDeadLocked()
    {
        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++)
            {
                if (allIcons[i, j] != null)
                {
                    if (i < width - 2)
                    {
                        if (SwitchAndCheck(i, j, Vector2.right))
                        {
                            return false;
                        }
                    }
                    if (j < height - 2)
                    {
                        if (SwitchAndCheck(i, j, Vector2.up))
                        {
                            return false;
                        }
                    }
                    if (j < height + 2)
                    {
                        if (SwitchAndCheck(i, j, Vector2.down))
                        {
                            return false;
                        }
                    }
                    if (i < height + 2)
                    {
                        if (SwitchAndCheck(i, j, Vector2.left))
                        {
                            return false;
                        }
                    }
                }
            }
        }
        return true;
    }
    public void ShuffleBoard()
    {
        if (IsDeadLocked() && !shuffling)
        {
            List<GameObject> newBoard = new List<GameObject>();
            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < height; j++)
                {
                    if (allIcons[i, j] != null)
                    {
                        newBoard.Add(allIcons[i, j]);
                    }
                }
            }
            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < height; j++)
                {
                    if (!blankSpaces[i, j])
                    {
                        int pieceToUse = Random.Range(0, newBoard.Count);


                        int maxIteration = 0;
                        while (MatchesAt(i, j, newBoard[pieceToUse]) && maxIteration < 100)
                        {
                            pieceToUse = Random.Range(0, newBoard.Count);
                            maxIteration++;
                        }
                        Icon piece = newBoard[pieceToUse].GetComponent<Icon>();
                        maxIteration = 0;


                        piece.column = i;
                        piece.row = j;
                        allIcons[i, j] = newBoard[pieceToUse];
                        newBoard.Remove(newBoard[pieceToUse]);
                    }
                }
            }
            deadlockedButton.SetActive(false);
            shuffling = true;
        }

    }
}