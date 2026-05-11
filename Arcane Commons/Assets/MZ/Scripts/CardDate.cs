using UnityEngine;

[CreateAssetMenu(fileName = "NewCard", menuName = "Card Game/Card")]
public class CardData : ScriptableObject
{
    public string cardName;

    public int attack;

    public int defense;

    public int cost;
}