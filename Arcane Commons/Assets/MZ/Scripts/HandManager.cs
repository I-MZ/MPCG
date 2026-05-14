//èDŠÖŒW‚ÌƒR[ƒh

using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HandManager : MonoBehaviour
{
    public List<CardData> hand = new List<CardData>();

    public GameObject cardPrefab;

    public Transform handArea;

    public void AddCard(CardData card)
    {
        hand.Add(card);

        Debug.Log(card.cardName + " ‚ğèD‚É’Ç‰Á");

        GameObject cardObject = Instantiate(cardPrefab, handArea);

        CardUI cardUI = cardObject.GetComponent<CardUI>();

        cardUI.Setup(card);
    }
}