using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="Icon Scriptable Object", menuName = "Icon")]
public class IconSO : ScriptableObject
{
    public enum EffectType
    {
        Health,
        Mana,
        Attack,
        Currency,
        Defend
    }
    public int EffectValue;
    public int EffectModifier = 1;
    public EffectType effectType;


}
