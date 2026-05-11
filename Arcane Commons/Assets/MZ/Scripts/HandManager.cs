using System.Collections.Generic;
using UnityEngine;

public class HandManager : MonoBehaviour
{
    public List<CardData> hand = new List<CardData>();

    public void AddCard(CardData card)
    {
        hand.Add(card);

        Debug.Log(card.cardName + " ‚ğèD‚É’Ç‰Á");
    }
}