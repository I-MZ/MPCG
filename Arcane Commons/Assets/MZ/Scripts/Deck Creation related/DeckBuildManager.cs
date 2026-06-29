//デッキ編集画面全体

using System.Collections.Generic;
using TMPro;
using UnityEngine;

using UnityEngine.SceneManagement;

public class DeckBuildManager : MonoBehaviour
{
    [Header("現在作成中デッキ")]
    //public DeckSaveData currentDeckData =new DeckSaveData(); エラー回避
    public List<CardData> currentDeck = new List<CardData>();

    [Header("デッキ枚数表示")]
    public TMP_Text deckCountText;

    [Header("追加可能カード")]
    public CardData swordCard;

    public CardData healCard;

    [Header("デッキ一覧表示場所")]
    public Transform deckListContent;

    [Header("カード名表示プレハブ")]
    public GameObject cardNameTextPrefab;

    private void Start()
    {
        UpdateDeckUI();
    }

    // カード追加
    public void AddCard(CardData card)
    {
        //currentDeckData.cards.Add(card); エラー回避
        currentDeck.Add(card);

        Debug.Log(card.cardName + " を追加");

        // 一覧表示生成
        GameObject textObj =Instantiate
            (
                cardNameTextPrefab,
                deckListContent
            );

        //TMP_Text text =textObj.GetComponent<TMP_Text>(); よくわからん奴
        //text.text = card.cardName;

        DeckListItem item = textObj.GetComponent<DeckListItem>();

        item.Setup(card, this);

        UpdateDeckUI();
    }

    //カード削除
    public void RemoveCard(CardData card, GameObject itemObject)
    {
        //currentDeckData.cards.Remove(card); エラー回避
        currentDeck.Remove(card);

        Destroy(itemObject);

        UpdateDeckUI();

        Debug.Log(card.cardName + " を削除");
    }

    // デッキUI更新
    public void UpdateDeckUI()
    {
        deckCountText.text ="デッキ枚数 : " + currentDeck.Count;
    }

    // Sword追加ボタン
    public void AddSword()
    {
        AddCard(swordCard);
    }

    // Heal追加ボタン
    public void AddHeal()
    {
        AddCard(healCard);
    }

    //デッキ保存
    public void SaveDeck()
    {
        DeckDataManager.Instance.SaveDeck(currentDeck);
    }

    public void StartBattle()
    {
        SaveDeck();

        SceneManager.LoadScene("BattleScene");
    }
}