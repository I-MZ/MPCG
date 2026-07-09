//カードの効果一覧のコード

using UnityEngine;

/// カード効果の種類
public enum EffectType
{
    None,       //効果なし

    Damage,     //対象にダメージ
    Heal,       //HP回復
    Draw,       //カードを引く
    Shield,     //防御値を得る
    DamageAll,  //全体にダメージ
    Summon,     //使い魔を召喚
    Trap,       //罠を発動
}