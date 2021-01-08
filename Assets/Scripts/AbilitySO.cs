using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName ="AbilityScriptableObject", menuName = "new Ability")]
public class AbilitySO : ScriptableObject
{
    public enum AbilityFamily
    {
        Rage,
        Heal,
        Sac,
        Monster
    }
    public enum AbilityType
    {
        lesserRage,
        lesserHeal,
        lesserSacrifice,
        Rage,
        Heal,
        Sacrifice,
        GreaterRage,
        GreaterHeal,
        GreaterSacrifice,
        PenUltimateRage,
        PenUltimateHeal,
        PenUltimateSacrifice,
        UltimateRage,
        UltimateHeal,
        UltimateSacrifice,

        MonsterLesserHeal
    }

    public string abilityDescription;
    public Sprite abilityIcon;
    public int abilityCost;
    public string abilityName;
    public AbilityType abilityType;
    public AbilityFamily abilityFamily;
    public int pointsRequired;
    public bool cooldown;
    public int coolDownTime;
    public int coolDownStartTurn;
}
