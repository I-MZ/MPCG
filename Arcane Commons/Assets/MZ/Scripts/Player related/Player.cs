using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("基本情報")]
    public string playerName;

    [Header("HP")]
    public int hp = 20;

    [Header("デッキ")]
    public List<CardData> deck = new List<CardData>();

    [Header("手札")]
    public List<CardData> hand = new List<CardData>();

    // ダメージを受ける
    public void TakeDamage(int damage)
    {
        hp -= damage;

        Debug.Log(playerName + " HP : " + hp);

        if (hp <= 0)
        {
            Debug.Log(playerName + " は敗北しました");
        }
    }

    // カードを引く
    public void DrawCard()
    {
        if (deck.Count <= 0)
        {
            Debug.Log(playerName + " のデッキ切れ");
            return;
        }

        CardData drawCard = deck[0];

        hand.Add(drawCard);

        deck.RemoveAt(0);

        Debug.Log(playerName + " は " + drawCard.cardName + " を引いた");
    }
}