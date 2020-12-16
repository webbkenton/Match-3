using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundTile : MonoBehaviour
{
    public int breakPoints;

    private void Update()
    {
        if (breakPoints <= 0)
        {
            Destroy(this.gameObject);
        }
    }

    public void TakeBreak(int damage)
    {
        breakPoints -= damage;
    }

  
}
