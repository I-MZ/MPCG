//使い魔のデータを扱うコード

using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewMinion",
                 menuName = "CardGame/Minion")]
public class MinionData : ScriptableObject
{
    [Header("名前")]
    public string minionName;

    [Header("攻撃力")]
    public int attack;

    [Header("体力")]
    public int hp;

    [Header("能力")]
    public List<AbilityData> abilities = new List<AbilityData>();
}