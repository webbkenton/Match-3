using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class FindMatches : MonoBehaviour
{
    public Board board;
    public BattleManger battleManger;
    private ScriptableObject iconSO;
    public List<GameObject> currentMatches = new List<GameObject>();
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
        yield return null;
        for (int i = 0; i < board.width; i++)
        {
            for (int j = 0; j < board.height; j++)
            {
                GameObject currentIcon = board.allIcons[i, j];
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
                        icon.isMatched = true;
                    }
                    if (board.allIcons[j, row] != null)
                    {
                        Icon icon2 = board.allIcons[j, row].GetComponent<Icon>();
                        icon2.isMatched = true;

                    }        
                 
                }

            }
        }
        return icons;
    }

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
    public void CheckBombs()
    {
        //Did the player move something?
        if (board.currentIcon != null)
        {
            //Is the piece they moved matched?
            if (board.currentIcon.isMatched)
            {
                //make it unmatched
                board.currentIcon.isMatched = false;
                //Decide what kind of bomb to make
                /*
                int typeOfBomb = Random.Range(0, 100);
                if(typeOfBomb < 50){
                    //Make a row bomb
                    board.currentIcon.MakeRowBomb();
                }else if(typeOfBomb >= 50){
                    //Make a column bomb
                    board.currentIcon.MakeColumnBomb();
                }
                */
                if ((board.currentIcon.swipeAngle > -45 && board.currentIcon.swipeAngle <= 45)
                   || (board.currentIcon.swipeAngle < -135 || board.currentIcon.swipeAngle >= 135))
                {
                    board.currentIcon.MakeRowBomb();
                }
                else
                {
                    board.currentIcon.MakeColumnBomb();
                }
            }
            //Is the other piece matched?
            else if (board.currentIcon.otherIcon != null)
            {
                Icon otherDot = board.currentIcon.otherIcon.GetComponent<Icon>();
                //Is the other Dot matched?
                if (otherDot.isMatched)
                {
                    //Make it unmatched
                    otherDot.isMatched = false;
                    /*
                    //Decide what kind of bomb to make
                    int typeOfBomb = Random.Range(0, 100);
                    if (typeOfBomb < 50)
                    {
                        //Make a row bomb
                        otherDot.MakeRowBomb();
                    }
                    else if (typeOfBomb >= 50)
                    {
                        //Make a column bomb
                        otherDot.MakeColumnBomb();
                    }
                    */
                    if ((board.currentIcon.swipeAngle > -45 && board.currentIcon.swipeAngle <= 45)
                   || (board.currentIcon.swipeAngle < -135 || board.currentIcon.swipeAngle >= 135))
                    {
                        otherDot.MakeRowBomb();
                    }
                    else
                    {
                        otherDot.MakeColumnBomb();
                    }
                }
            }

        }
    }
}
