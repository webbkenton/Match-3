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

    private HintManager hintManager;
    private FindMatches findMatches;
    private Board board;
    public GameObject otherIcon;
    private Vector2 firstTouchPosition = Vector2.zero;
    private Vector2 finalTouchPosition = Vector2.zero;
    private Vector2 tempPosition;



    public float swipeAngle = 0;
    public float swipeResist = .5f;

    public bool isTypeBomb;
    //public bool isColumnBomb;
    //public bool isRowBomb;
    public bool isStarBomb;
    public bool isAdjacentBomb;
    //public GameObject rowArrow;
    //public GameObject columnArrow;
    public GameObject starBomb;
    public GameObject typeBomb;
    public GameObject adjacentMarker;

    // Start is called before the first frame update
    void Start()
    {
        hintManager = FindObjectOfType<HintManager>();
        board = GameObject.FindWithTag("Board").GetComponent<Board>();
        findMatches = FindObjectOfType<FindMatches>();
        //isColumnBomb = false;
        //isRowBomb = false;]
        isStarBomb = false;
        isTypeBomb = false;
        isAdjacentBomb = false;
        //board = FindObjectOfType<Board>();
        //targetX = (int)transform.position.x;
        //targetY = (int)transform.position.y;
        //row = targetY;
        //column = targetX;
        //previousColumn = column;
        //previousRow = row;
    }

    //private void OnMouseOver()
    //{
    //    if (Input.GetMouseButtonDown(1))
    //    {
    //        isStarBomb = true;
    //        GameObject marker = Instantiate(starBomb, transform.position, Quaternion.identity);
    //        marker.transform.parent = this.transform;
    //    }
    //}

    //this is for testing and debug only

    // Update is called once per frame
    void Update()
    {
        /*
        if (isMatched)
        {
            SpriteRenderer mySprite = GetComponent<SpriteRenderer>();
            mySprite.color = new Color(1f, 1f, 1f, .2f);
        }
        */
        targetX = column;
        targetY = row;
        if (Mathf.Abs(targetX - transform.position.x) > .1)
        {
            //Move Towards Target
            tempPosition = new Vector2(targetX, transform.position.y);
            transform.position = Vector2.Lerp(transform.position, tempPosition, .30f);
            if (board.allIcons[column, row] != this.gameObject)
            {
                board.allIcons[column, row] = this.gameObject;
                findMatches.FindAllMatches();
            }

        }
        else
        {
            //Directly Set Position
            tempPosition = new Vector2(targetX, transform.position.y);
            transform.position = tempPosition;
            //board.allIcons[column, row] = this.gameObject;
        }

        if (Mathf.Abs(targetY - transform.position.y) > .1)
        {
            //Move Towards Target
            tempPosition = new Vector2(transform.position.x, targetY);
            transform.position = Vector2.Lerp(transform.position, tempPosition, .30f);
            if (board.allIcons[column, row] != this.gameObject)
            {
                board.allIcons[column, row] = this.gameObject;
                findMatches.FindAllMatches();
            }
            

        }
        else
        {
            //Directly Set Position
            tempPosition = new Vector2(transform.position.x, targetY);
            transform.position = tempPosition;
        }
    }

    public IEnumerator CheckMoveCo()
    {
        if (isTypeBomb)
        {
            findMatches.MatchPiecesOfColor(otherIcon.tag);
            isMatched = true;
        }
        else if (otherIcon.GetComponent<Icon>().isTypeBomb)
        {
            findMatches.MatchPiecesOfColor(this.gameObject.tag);
            otherIcon.GetComponent<Icon>().isMatched = true;
        }
        //This is how long before the game confirms your move
        yield return new WaitForSeconds(.15f);
        if (otherIcon != null)
        {
            if (!isMatched && !otherIcon.GetComponent<Icon>().isMatched)
            {
                otherIcon.GetComponent<Icon>().row = row;
                otherIcon.GetComponent<Icon>().column = column;
                row = previousRow;
                column = previousColumn;
                //This is how long before the tiles switch back with no match
                yield return new WaitForSeconds(.15f);
                board.currentIcon = null;
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
        if (hintManager != null)
        {
            hintManager.DestoryHint();
        }
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
            board.currentState = GameState.wait;
            swipeAngle = Mathf.Atan2(finalTouchPosition.y - firstTouchPosition.y, finalTouchPosition.x - firstTouchPosition.x) * 180 / Mathf.PI;
            MoveTile();
            board.currentIcon = this;
        }
        else
        {
            board.currentState = GameState.move;

        }
    }

    void MovePiecesActual(Vector2 direction)
    {
        otherIcon = board.allIcons[column + (int)direction.x, row + (int)direction.y];
        previousColumn = column;
        previousRow = row;
        if (otherIcon != null)
        {
            otherIcon.GetComponent<Icon>().column += -1 * (int)direction.x;
            otherIcon.GetComponent<Icon>().row += -1 * (int)direction.y;
            column += (int)direction.x;
            row += (int)direction.y;
            StartCoroutine(CheckMoveCo());
        }
        else
        {
            board.currentState = GameState.move;
        }
    }

    void MoveTile()
    {
        if (swipeAngle > -45 && swipeAngle <= 45 && column < board.width - 1)// right swipe
        {
            MovePiecesActual(Vector2.right);
            /*otherIcon = board.allIcons[column + 1, row];
            previousColumn = column;
            previousRow = row;
            otherIcon.GetComponent<Icon>().column -= 1;
            column += 1;*/
        }
        else

        if (swipeAngle > 45 && swipeAngle <= 135 && row < board.height - 1)// up swipe
        {
            MovePiecesActual(Vector2.up);
            /*
            otherIcon = board.allIcons[column, row +1 ];
            previousColumn = column;
            previousRow = row;
            otherIcon.GetComponent<Icon>().row -= 1;
            row += 1;
            */
        }

        else

        if ((swipeAngle > 135 || swipeAngle <= -135) && column > 0)// left swipe
        {
            MovePiecesActual(Vector2.left);
            /*
            otherIcon = board.allIcons[column - 1, row];
            previousColumn = column;
            previousRow = row;
            otherIcon.GetComponent<Icon>().column += 1;
            column -= 1;
            */
        }

        else

        if (swipeAngle < -45 && swipeAngle >= -135 && row > 0) // down swipe
        {
            MovePiecesActual(Vector2.down);
            /*
            otherIcon = board.allIcons[column, row - 1];
            previousColumn = column;
            previousRow = row;
            otherIcon.GetComponent<Icon>().row += 1;
            row -= 1;
            */
        }
        else
        {
            board.currentState = GameState.move;
        }
        //StartCoroutine(CheckMoveCo());

    }

    void FindMatches()
    {
        if (column > 0 && column < board.width - 1)
        {
            GameObject leftIcon1 = board.allIcons[column - 1, row];
            GameObject rightIcon1 = board.allIcons[column + 1, row];
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
            GameObject upIcon1 = board.allIcons[column, row + 1];
            GameObject downIcon1 = board.allIcons[column, row - 1];
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

    public void MakeAdjacentBomb()
    {
        if (!isTypeBomb && !isStarBomb)
        {
            isAdjacentBomb = true;
            GameObject marker = Instantiate(adjacentMarker, transform.position, Quaternion.identity);
            marker.transform.parent = this.transform;

        }
    }



}
