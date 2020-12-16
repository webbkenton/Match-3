using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HintManager : MonoBehaviour
{

    private Board board;
    public float hintDelay;
    private float hintDelaySeconds;
    public GameObject hintParticle;
    public GameObject currenthint;
    // Start is called before the first frame update
    void Start()
    {
        board = FindObjectOfType<Board>();
        hintDelaySeconds = hintDelay;
    }

    // Update is called once per frame
    void Update()
    {
        hintDelaySeconds -= Time.deltaTime;
        if (hintDelaySeconds <= 0 && currenthint == null)
        {
            MarkHint();
            hintDelaySeconds = hintDelay;
        }
    }

    List<GameObject> FindAllMatches()
    {
        List<GameObject> possibleMoves = new List<GameObject>();
        for (int i = 0; i < board.width; i++)
        {
            for (int j = 0; j < board.height; j++)
            {
                if (board.allIcons[i, j] != null)
                {
                    if (i < board.width - 1)
                    {
                        if (board.SwitchAndCheck(i, j, Vector2.right))
                        {
                            possibleMoves.Add(board.allIcons[i, j]);
                        }
                    }
                    if (j < board.height - 1)
                    {
                        if (board.SwitchAndCheck(i, j, Vector2.up))
                        {
                            possibleMoves.Add(board.allIcons[i, j]);
                        }
                    }
                }
            }
        }
        return possibleMoves;
    }

    GameObject PickOneRandomly()
    {
        List<GameObject> possibleMoves = new List<GameObject>();
        possibleMoves = FindAllMatches();
        if (possibleMoves.Count > 0)
        {
            int peiceToUse = Random.Range(0, possibleMoves.Count);
            return possibleMoves[peiceToUse];
        }
        return null;
    }

    private void MarkHint()
    {
        GameObject hint = PickOneRandomly();
        if (hint != null)
        {
            currenthint = Instantiate(hintParticle, hint.transform.position, Quaternion.identity);
        }
    }
    public void DestoryHint()
    {
        if (currenthint != null)
        {
            Destroy(currenthint);
            currenthint = null;
            hintDelaySeconds = hintDelay;
        }
    }
}
