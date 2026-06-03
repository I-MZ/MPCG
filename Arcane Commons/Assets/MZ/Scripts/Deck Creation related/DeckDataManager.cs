using System.Collections.Generic;
using UnityEngine;

public class DeckDataManager : MonoBehaviour
{
    public static DeckDataManager Instance;

    //現在保存中のデッキ
    public List<CardData> savedDeck = new List<CardData>();

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;

            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    //デッキ保存
    public void SaveDeck(List<CardData> deck)
    {
        savedDeck = new List<CardData>(deck);

        Debug.Log("デッキ保存");
        Debug.Log("枚数 : " + savedDeck.Count);
    }
}
