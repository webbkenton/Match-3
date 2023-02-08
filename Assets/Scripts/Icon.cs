using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Icon : MonoBehaviour
{
    [Header("Board Variables")]
    public int column;
    public int row;
    public int previousColumn;
    public int previousRow;
    public int targetX;
    public int targetY;
    public bool isMatched = false;


    private FindMatchesV2 findMatches;
    private Boardv2 board;
    public GameObject otherIcon;
    private Vector2 firstTouchPosition;
    private Vector2 finalTouchPosition;
    private Vector2 tempPosition;

    [Header("Swipe Stuff")]
    public float swipeAngle = 0;
    public float swipeResist = 1f;

    [Header("Powerup Stuff")]
    public bool isColorBomb;
    public bool isColumnBomb;
    public bool isRowBomb;
    public GameObject rowArrow;
    public GameObject columnArrow;
    public GameObject colorBomb;

    public bool isTypeBomb;
    public bool isStarBomb;
    public bool isAdjacentBomb;

    public GameObject starBomb;
    public GameObject typeBomb;
    public GameObject adjacentMarker;
    private BattleManger battleManger;
    public GameObject selectedIcon;


    // Use this for initialization
    void Start()
    {

        isColumnBomb = false;
        isRowBomb = false;

        board = FindObjectOfType<Boardv2>();
        findMatches = FindObjectOfType<FindMatchesV2>();
        //targetX = (int)transform.position.x;
        //targetY = (int)transform.position.y;
        //row = targetY;
        //column = targetX;
        //previousRow = row;
        //previousColumn = column;

    }


    //This is for testing and Debug only.
    private void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(1))
        {
            isColorBomb = true;
            GameObject color = Instantiate(colorBomb, transform.position, Quaternion.identity);
            color.transform.parent = this.transform;
        }
    }


    // Update is called once per frame
    void Update()
    {
        /*
        if(isMatched){
            
            SpriteRenderer mySprite = GetComponent<SpriteRenderer>();
            Color currentColor = mySprite.color;
            mySprite.color = new Color(currentColor.r, currentColor.g, currentColor.b, .5f);
        }
        */
        targetX = column;
        targetY = row;
        if (Mathf.Abs(targetX - transform.position.x) > .1)
        {
            //Move Towards the target
            tempPosition = new Vector2(targetX, transform.position.y);
            transform.position = Vector2.Lerp(transform.position, tempPosition, .6f);
            if (board.allDots[column, row] != this.gameObject)
            {
                board.allDots[column, row] = this.gameObject;
            }
            findMatches.FindAllMatches();


        }
        else
        {
            //Directly set the position
            tempPosition = new Vector2(targetX, transform.position.y);
            transform.position = tempPosition;

        }
        if (Mathf.Abs(targetY - transform.position.y) > .1)
        {
            //Move Towards the target
            tempPosition = new Vector2(transform.position.x, targetY);
            transform.position = Vector2.Lerp(transform.position, tempPosition, .6f);
            if (board.allDots[column, row] != this.gameObject)
            {
                board.allDots[column, row] = this.gameObject;
            }
            findMatches.FindAllMatches();

        }
        else
        {
            //Directly set the position
            tempPosition = new Vector2(transform.position.x, targetY);
            transform.position = tempPosition;

        }
        //OnMouseOver();
    }

    public IEnumerator CheckMoveCo()
    {
        if (isColorBomb)
        {
            //This piece is a color bomb, and the other piece is the color to destroy
            findMatches.MatchPiecesOfColor(otherIcon.tag);
            isMatched = true;
        }
        else if (otherIcon.GetComponent<Icon>().isColorBomb)
        {
            //The other piece is a color bomb, and this piece has the color to destroy
            findMatches.MatchPiecesOfColor(this.gameObject.tag);
            otherIcon.GetComponent<Icon>().isMatched = true;
        }
        yield return new WaitForSeconds(.5f);
        if (otherIcon != null)
        {
            if (!isMatched && !otherIcon.GetComponent<Icon>().isMatched)
            {
                otherIcon.GetComponent<Icon>().row = row;
                otherIcon.GetComponent<Icon>().column = column;
                row = previousRow;
                column = previousColumn;
                yield return new WaitForSeconds(.5f);
                board.currentDot = null;
                board.currentState = GameState.move;
            }
            else
            {
                board.DestroyMatches();

            }
            //otherIcon = null;
        }

    }

    private void OnMouseDown()
    {
        if (board.currentState == GameState.move)
        {
            firstTouchPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        }
    }

    private void OnMouseUp()
    {
        if (board.currentState == GameState.move)
        {
            finalTouchPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            CalculateAngle();
        }
    }

    void CalculateAngle()
    {
        if (Mathf.Abs(finalTouchPosition.y - firstTouchPosition.y) > swipeResist || Mathf.Abs(finalTouchPosition.x - firstTouchPosition.x) > swipeResist)
        {
            swipeAngle = Mathf.Atan2(finalTouchPosition.y - firstTouchPosition.y, finalTouchPosition.x - firstTouchPosition.x) * 180 / Mathf.PI;
            MovePieces();
            board.currentState = GameState.wait;
            board.currentDot = this;

        }
        else
        {
            board.currentState = GameState.move;

        }
    }

    void MovePieces()
    {
        if (swipeAngle > -45 && swipeAngle <= 45 && column < board.width - 1)
        {
            //Right Swipe
            otherIcon = board.allDots[column + 1, row];
            previousRow = row;
            previousColumn = column;
            otherIcon.GetComponent<Icon>().column -= 1;
            column += 1;

        }
        else if (swipeAngle > 45 && swipeAngle <= 135 && row < board.height - 1)
        {
            //Up Swipe
            otherIcon = board.allDots[column, row + 1];
            previousRow = row;
            previousColumn = column;
            otherIcon.GetComponent<Icon>().row -= 1;
            row += 1;

        }
        else if ((swipeAngle > 135 || swipeAngle <= -135) && column > 0)
        {
            //Left Swipe
            otherIcon = board.allDots[column - 1, row];
            previousRow = row;
            previousColumn = column;
            otherIcon.GetComponent<Icon>().column += 1;
            column -= 1;
        }
        else if (swipeAngle < -45 && swipeAngle >= -135 && row > 0)
        {
            //Down Swipe
            otherIcon = board.allDots[column, row - 1];
            previousRow = row;
            previousColumn = column;
            otherIcon.GetComponent<Icon>().row += 1;
            row -= 1;
        }

        StartCoroutine(CheckMoveCo());
    }

    void FindMatches()
    {
        if (column > 0 && column < board.width - 1)
        {
            GameObject leftIcon1 = board.allDots[column - 1, row];
            GameObject rightIcon1 = board.allDots[column + 1, row];
            if (leftIcon1 != null && rightIcon1 != null)
            {
                if (leftIcon1.tag == this.gameObject.tag && rightIcon1.tag == this.gameObject.tag)
                {
                    leftIcon1.GetComponent<Icon>().isMatched = true;
                    rightIcon1.GetComponent<Icon>().isMatched = true;
                    isMatched = true;
                }
            }
        }
        if (row > 0 && row < board.height - 1)
        {
            GameObject upIcon1 = board.allDots[column, row + 1];
            GameObject downIcon1 = board.allDots[column, row - 1];
            if (upIcon1 != null && downIcon1 != null)
            {
                if (upIcon1.tag == this.gameObject.tag && downIcon1.tag == this.gameObject.tag)
                {
                    upIcon1.GetComponent<Icon>().isMatched = true;
                    downIcon1.GetComponent<Icon>().isMatched = true;
                    isMatched = true;
                }
            }
        }

    }

    public void MakeRowBomb()
    {
        isRowBomb = true;
        GameObject arrow = Instantiate(rowArrow, transform.position, Quaternion.identity);
        arrow.transform.parent = this.transform;
    }

    public void MakeColumnBomb()
    {
        isColumnBomb = true;
        GameObject arrow = Instantiate(columnArrow, transform.position, Quaternion.identity);
        arrow.transform.parent = this.transform;
    }

    public void MakeColorBomb()
    {
        isColorBomb = true;
        GameObject color = Instantiate(colorBomb, transform.position, Quaternion.identity);
        color.transform.parent = this.transform;
    }
    public void MakeAdjacentBomb()
    {
        if (!isTypeBomb && !isStarBomb)
        {
            isAdjacentBomb = true;
            GameObject marker = Instantiate(adjacentMarker, transform.position, Quaternion.identity);
            marker.transform.parent = this.transform;

        }
    }
    public void MakeStarBomb()
    {
        if (!isTypeBomb && !isAdjacentBomb)
        {
            isStarBomb = true;
            GameObject star = Instantiate(starBomb, transform.position, Quaternion.identity);
            //1. There is an issue here with the Bomb object being instaniated on the wrong tile.
            star.transform.parent = this.transform;


        }
    }

    public void MakeTypeBomb()
    {
        if (!isStarBomb && !isAdjacentBomb)
        {
            isTypeBomb = true;
            GameObject tBomb = Instantiate(typeBomb, transform.position, Quaternion.identity);
            tBomb.transform.parent = this.transform;
            this.gameObject.tag = "TypeBomb";
        }
    }
}
