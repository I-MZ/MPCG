//カードのデータを扱うコードを

using UnityEngine;

[CreateAssetMenu(fileName = "New Card", menuName = "Card Game/Card")]
public class CardData : ScriptableObject
{
    [Header("基本情報")]
    public string cardName;

    [TextArea]
    public string description;

    [Header("見た目")]
    public Sprite cardImage;

    [Header("コスト")]
    public int cost;

    [Header("カード種類")]
    public CardType cardType;

    [Header("効果")]
    public EffectType effectType;

    [Header("召喚する使い魔")]
    public MinionData summonMinion;

    [Header("召喚する数")]
    public int summonCount = 1;

    public int value;
}

//ダメージを別規格で扱ってみるから一旦保留(役に立ったぜ！)
//using UnityEngine;

//[CreateAssetMenu(fileName = "NewCard", menuName = "Card Game/Card")]
//public class CardData : ScriptableObject
//{
//    public string cardName;

//    public int attack;  //攻撃力
//    public int defense; //防御力
//    public int cost;    //コスト

//    public CardType cardType;

//    public EffectType effectType;
//    public int value;

//}