//作成したデッキの保存・読込・更新・削除を管理するコード

using System.Collections.Generic;
using UnityEngine;

public class DeckDataManager : MonoBehaviour
{
    public static DeckDataManager Instance;

    [Header("保存済みデッキ一覧")]
    public List<DeckSaveData> savedDecks = new List<DeckSaveData>();

    //現在編集中のデッキ番号
    public int currentDeckIndex = -1;

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
    public void SaveNewDeck(DeckSaveData deck)
    {
        DeckSaveData copy = new DeckSaveData();

        copy.deckName = deck.deckName;

        copy.classType = deck.classType;

        copy.cards = new List<CardData>(deck.cards);

        savedDecks.Add(copy);

        Debug.Log(copy.deckName + " を保存");

        Debug.Log("保存デッキ数 : " + savedDecks.Count);
    }

    //読み込み
    public DeckSaveData LoadDeck(int index)
    {
        if (index < 0 || index >= savedDecks.Count)
        {
            return null;
        }

        currentDeckIndex = index;

        return savedDecks[index];
    }

    //上書き保存
    public void UpdateDeck(DeckSaveData deck)
    {
        if (currentDeckIndex < 0)
        {
            return;
        }

        DeckSaveData copy = new DeckSaveData();

        copy.deckName = deck.deckName;

        copy.classType = deck.classType;

        copy.cards = new List<CardData>(deck.cards);

        savedDecks[currentDeckIndex] = copy;

        Debug.Log(copy.deckName + " を更新");
    }

    //削除
    public void DeleteDeck(int index)
    {
        if (index < 0 || index >= savedDecks.Count)
        {
            return;
        }

        savedDecks.RemoveAt(index);

        currentDeckIndex = -1;

        Debug.Log("デッキ削除");
    }
}