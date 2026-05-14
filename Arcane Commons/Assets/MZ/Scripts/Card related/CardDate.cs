//カードのデータ

using UnityEngine;

[CreateAssetMenu(fileName = "NewCard", menuName = "Card Game/Card")]
public class CardData : ScriptableObject
{
    public string cardName;

    public int attack;  //攻撃力
    public int defense; //防御力
    public int cost;    //コスト

    public CardType cardType;

    public EffectType effectType;
    public int value;

}