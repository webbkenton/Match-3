using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName ="AbilityScriptableObject", menuName = "new Ability")]
public class AbilitySO : ScriptableObject
{
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
        GreaterSacrifice
    }

    public string abilityDescription;
    public Sprite abilityIcon;
    public int abilityCost;
    public string abilityName;
    public AbilityType abilityType;
    public bool cooldown;
    public int coolDownTime;
}
