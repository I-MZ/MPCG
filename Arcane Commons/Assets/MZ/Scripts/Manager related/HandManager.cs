//手札関係のコード

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

        Debug.Log(card.cardName + " を手札に追加");

        GameObject cardObject = Instantiate(cardPrefab, handArea);

        CardUI cardUI = cardObject.GetComponent<CardUI>();

        //cardUI.Setup(card);//エラーが出てるから一旦コメントアウト
    }
}