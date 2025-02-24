﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CurrencyManager : MonoBehaviour
{
    public static CurrencyManager currency;

    private Board board;
    public Text currencyText;
    public int currencyAmount;
    // Start is called before the first frame update
    void Start()
    {
        board = FindObjectOfType<Board>();
        currencyText.text = PersistantData.data.currency.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        currencyText.text = PersistantData.data.currency.ToString();
    }

    public void IncreaseCurrency(int currency)
    {
        currencyAmount += currency;
    }
}
