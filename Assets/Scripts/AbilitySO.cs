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

    public Sprite abilityIcon;
    public int abilityCost;
    public string abilityName;
    public AbilityType abilityType;


}
