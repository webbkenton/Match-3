using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LichTapAbility : MonoBehaviour
{
    private int turnCounter = 0;
    private int lastTurnUsed = 0;
    public Sprite abilityIcon;
    private BattleManger battleManger;
    public string abilityDescription;

    private void Start()
    {
        turnCounter = 0;
        //battleManger = GameObject.FindGameObjectWithTag("BattleManager").GetComponent<BattleManger>();
        abilityDescription = "Every 10 Turns This Enemy Will Drain 20 Life From an Openent";
    }

    private void LateUpdate()
    {
        if (battleManger == null && PersistantData.data.inEvent == true)
        {
            battleManger = GameObject.FindGameObjectWithTag("BattleManager").GetComponent<BattleManger>();
        }
        if (battleManger != null)
        {
            turnCounter = battleManger.turnCounter;
        }
        LichTap();
    }

    public void LichTap()
    {
        if (turnCounter >= lastTurnUsed + 10)
        {
            PersistantData.data.health -= 20f;
            lastTurnUsed = turnCounter;
        }
    }

}
