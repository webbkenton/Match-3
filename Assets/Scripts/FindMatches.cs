using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class FindMatches : MonoBehaviour
{
    private Board board;
    private BattleManger battleManger;
    private ScriptableObject iconSO;
    public List<GameObject> currentMatches = new List<GameObject>();
    // Start is called before the first frame update
    void Start()
    {
        battleManger = FindObjectOfType<BattleManger>();
        board = FindObjectOfType<Board>();
    }
    public void FindAllMatches()
    {
        StartCoroutine(FindAllMatchesCo());
    }


    private List<GameObject> IsAdjacentBomb(Icon icon1, Icon icon2, Icon icon3)
    {
        List<GameObject> currentIcons = new List<GameObject>();
        if (icon1.isAdjacentBomb)
        {
            currentMatches.Union(GetAdjacentPieces(icon1.column, icon1.row));
        }
        if (icon2.isAdjacentBomb)
        {
            currentMatches.Union(GetAdjacentPieces(icon2.column, icon2.row));
        }
        if (icon3.isAdjacentBomb)
        {
            currentMatches.Union(GetAdjacentPieces(icon3.column, icon3.row));
        }
        return currentIcons;
    }

    private List<GameObject> IsStarBomb(Icon icon1, Icon icon2, Icon icon3)
    {
        List<GameObject> currentIcons = new List<GameObject>();
        if (icon1.isStarBomb)
        {
            currentMatches.Union(GetStarPieces(icon1.column, icon1.row));
        }
        if (icon2.isStarBomb)
        {
            currentMatches.Union(GetStarPieces(icon2.column, icon2.row));
        }
        if (icon3.isStarBomb)
        {
            currentMatches.Union(GetStarPieces(icon3.column, icon3.row));
        }
        return currentIcons;
    }

    /*private List<GameObject> IsRowBomb(Icon icon1, Icon icon2, Icon icon3)
    {
        List<GameObject> currentIcons = new List<GameObject>();
        if (icon1.isRowBomb)
        {
            currentMatches.Union(GetRowPieces(icon1.row));
        }
        if (icon2.isRowBomb)
        {
            currentMatches.Union(GetRowPieces(icon2.row));
        }
        if (icon3.isRowBomb)
        {
            currentMatches.Union(GetRowPieces(icon3.row));
        }
        return currentIcons;
    }
    private List<GameObject> IsColumnBomb(Icon icon1, Icon icon2, Icon icon3)
    {
        List<GameObject> currentIcons = new List<GameObject>();
        if (icon1.isColumnBomb)
        {
            currentMatches.Union(GetColumnPieces(icon1.column));
        }
        if (icon2.isColumnBomb)
        {
            currentMatches.Union(GetColumnPieces(icon2.column));
        }
        if (icon3.isColumnBomb)
        {
            currentMatches.Union(GetColumnPieces(icon3.column));
        }
        return currentIcons;
    }*/

    private void AddToListAndMatch(GameObject icon)
    {
        if (!currentMatches.Contains(icon))
        {
            currentMatches.Add(icon);
        }
        icon.GetComponent<Icon>().isMatched = true;
    }

    private void GetNearbyPieces(GameObject icon1,GameObject icon2,GameObject icon3)
    {
        AddToListAndMatch(icon1);
        AddToListAndMatch(icon2);
        AddToListAndMatch(icon3);
    }
    private IEnumerator FindAllMatchesCo()
    {
        //This one controls how quickly a match is recognized 
        //must be less then MatchesOnBoard()
        //yield return new WaitForSeconds(.1f);
        yield return null;
        for (int i = 0; i < board.width; i++)
        {
            for (int j = 0; j < board.height; j++)
            {
                GameObject currentIcon = board.allIcons[i, j];
                //Icon currentIconIcon = currentIcon.GetComponent<Icon>();
                if (currentIcon != null)
                {
                    Icon currentIconIcon = currentIcon.GetComponent<Icon>();
                    if (i > 0 && i < board.width - 1)
                    {
                        GameObject leftIcon = board.allIcons[i - 1, j];
                        GameObject rightIcon = board.allIcons[i + 1, j];
                        if (leftIcon != null && rightIcon != null)
                        {
                            Icon leftIconIcon = leftIcon.GetComponent<Icon>();
                            Icon rightIconIcon = rightIcon.GetComponent<Icon>();

                            if (leftIcon.tag == currentIcon.tag && rightIcon.tag == currentIcon.tag)
                            {
                                //currentMatches.Union(IsRowBomb(leftIconIcon, currentIconIcon, rightIconIcon));
                                //currentMatches.Union(IsColumnBomb(leftIconIcon, currentIconIcon, rightIconIcon));
                                currentMatches.Union(IsAdjacentBomb(leftIconIcon, currentIconIcon, rightIconIcon));
                                currentMatches.Union(IsStarBomb(leftIconIcon, currentIconIcon, rightIconIcon));
                                GetNearbyPieces(leftIcon, currentIcon, rightIcon);
                            }       
                        }
                    }
                    if (j > 0 && j < board.height - 1)
                    {
                        GameObject upIcon = board.allIcons[i, j + 1];
                        GameObject downIcon = board.allIcons[i, j - 1];
                        if (upIcon != null && downIcon != null)
                        {
                            Icon upIconIcon = upIcon.GetComponent<Icon>();
                            Icon downIconIcon = downIcon.GetComponent<Icon>();

                            if (upIcon.tag == currentIcon.tag && downIcon.tag == currentIcon.tag)
                            {

                                //currentMatches.Union(IsRowBomb(upIconIcon, currentIconIcon, downIconIcon));
                                //currentMatches.Union(IsColumnBomb(upIconIcon, currentIconIcon, downIconIcon));
                                currentMatches.Union(IsAdjacentBomb(upIconIcon, currentIconIcon, downIconIcon));
                                currentMatches.Union(IsStarBomb(upIconIcon, currentIconIcon, downIconIcon));

                                GetNearbyPieces(upIcon, currentIcon, downIcon);

                            }
                        }
                    }
                }
            }
        }
    }

    List<GameObject> GetStarPieces(int column, int row)
    {
        // These entries are used to locate the peices that need to be destoyed by the Bombs
        Debug.Log("StarPieces");
        List<GameObject> icons = new List<GameObject>();
        for (int i = 0; i < board.height; i++)
        {
            for (int j = 0; j < board.width; j++)
            {
                if (i >=0 && i < board.height || j >= 0 && j<board.width)
                {
                    if (board.allIcons[column, i] != null)
                    {
                        Icon icon = board.allIcons[column, i].GetComponent<Icon>();
                        //if (icon.isTypeBomb == true || icon.isAdjacentBomb == true || icon.isStarBomb == true)
                        //{
                        //    FindAllMatches();
                        //}
                        //else
                        //{
                        icon.isMatched = true;
                        //}
                    }
                    if (board.allIcons[j, row] != null)
                    {
                        Icon icon2 = board.allIcons[j, row].GetComponent<Icon>();
                        //if (icon2.isTypeBomb == true || icon2.isAdjacentBomb == true || icon2.isStarBomb == true)
                        //{
                        //    FindAllMatches();
                        //}
                        //else
                        //{
                        //    icon2.isMatched = true;
                        icon2.isMatched = true;
                        //}

                    }        
                 
                }

            }
        }
        return icons;
    }

    /*public void CheckAgain()
    {
        for (int i = 0; i < board.height; i++)
        {
            for (int j = 0; j < board.width; j++)
            {
                if (i >= 0 && i < board.height || j >= 0 && j < board.width)
                {
                    if (board.allIcons[column, i] != null)
                    {

                    }
                }
            }
        }
    }*/
    List<GameObject> GetAdjacentPieces(int column, int row)
    {
        Debug.Log("AdjacentPieces");
        List<GameObject> icons = new List<GameObject>();
        for (int i = column - 1; i <= column + 1 ; i++)
        {
            for (int j = row - 1; j <= row +1 ; j++)
            {
                if (i >= 0 && i < board.width && j >= 0 && j < board.height)
                {
                    if (board.allIcons[i, j] != null)
                    {
                        icons.Add(board.allIcons[i, j]);
                        board.allIcons[i, j].GetComponent<Icon>().isMatched = true;
                    }
                }
            }
        }
        return icons;
    }
    public void MatchPiecesOfColor(string type)
    {
        Debug.Log("PiecesOfColor");
        for (int i = 0; i < board.width; i++)
        {
            for (int j = 0; j < board.height; j++)
            {
                if (board.allIcons[i, j] != null)
                {
                    if (board.allIcons[i, j].tag == type)
                    {
                        board.allIcons[i, j].GetComponent<Icon>().isMatched = true;
                    }
                }
            }
        }
    }

    /*List<GameObject> GetColumnPieces(int column)
    {
        List<GameObject> icons = new List<GameObject>();
        for (int i = 0; i < board.height; i++)
        {
            if (board.allIcons[column, i] != null)
            {
                Icon icon = board.allIcons[column, i].GetComponent<Icon>();
                if (icon.isRowBomb)
                {
                    icons.Union(GetRowPieces(i)).ToList();
                }
                icons.Add(board.allIcons[column, i]);
                icon.isMatched = true;
            }
        }

        return icons;
    }

    List<GameObject> GetRowPieces(int row)
    {
        List<GameObject> icons = new List<GameObject>();
        for (int i = 0; i < board.width; i++)
        {
            if (board.allIcons[i, row] != null)
            {
                Icon icon = board.allIcons[i, row].GetComponent<Icon>();
                if (icon.isColumnBomb)
                {
                    icons.Union(GetColumnPieces(i)).ToList() ;
                }
                icons.Add(board.allIcons[i, row]);
                icon.isMatched = true;
            }
        }

        return icons;
    }

    public void CheckBombs()
    {
        if (board.currentIcon != null)
        {
            if (board.currentIcon.isMatched)
            {
                board.currentIcon.isMatched = false;
                int typeOfBomb = Random.Range(0, 100);
                if (typeOfBomb < 50)
                {
                    //Make row bomb
                    board.currentIcon.MakeRowBomb();

                }
                else if (typeOfBomb > 50)
                {
                    board.currentIcon.MakeColumnBomb();
                }
            }
            else if (board.currentIcon.otherIcon != null)
            {
                Icon otherIcon = board.currentIcon.otherIcon.GetComponent<Icon>();
                if (otherIcon.isMatched)
                {
                    otherIcon.isMatched = false;
                    int typeOfBomb = Random.Range(0, 100);
                    if (typeOfBomb < 50)
                    {
                        //Make row bomb
                        otherIcon.MakeRowBomb();

                    }
                    else if (typeOfBomb > 50)
                    {
                        otherIcon.MakeColumnBomb();
                    }

                }

            }
        }
    }*/
}
