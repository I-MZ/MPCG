using UnityEngine;

using System;

public enum AbilityTrigger
{
    OnPlay,        //使用時
    OnAttack,      //攻撃時
    OnDeath,       //死亡時

    OnTurnStart,   //ターン開始時
    OnTurnEnd,     //ターン終了時

    Aura           //場にいる間
}

public enum AbilityEffect
{
    Damage,        //ダメージ
    Heal,          //回復
    Draw,          //ドロー
    Summon,        //使い魔召喚

    Lifesteal,     //吸血
    Rush,          //疾走
    Guard          //守護
}

[Serializable]
public class AbilityData
{
    public AbilityTrigger trigger;

    public AbilityEffect effect;

    public int value;
}