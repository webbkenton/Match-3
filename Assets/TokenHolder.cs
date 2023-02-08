using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TokenHolder : MonoBehaviour
{
    private void Start()
    {
        gameObject.GetComponentInParent<IconIdle>().tokenHolder = this.gameObject;
    }
}
