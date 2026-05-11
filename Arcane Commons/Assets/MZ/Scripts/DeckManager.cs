//デッキ・山札関係

using System.Collections.Generic;
using UnityEngine;

public class DeckManager : MonoBehaviour
{
    public List<CardData> deck = new List<CardData>();

    public HandManager handManager;

    void Start()
    {
        DrawCard();
    }

    public void DrawCard()
    {
        if (deck.Count <= 0)
        {
            Debug.Log("Deck Empty");
            return;
        }

        CardData drawCard = deck[0];

        Debug.Log("Draw : " + drawCard.cardName);

        handManager.AddCard(drawCard);

        deck.RemoveAt(0);
    }
}