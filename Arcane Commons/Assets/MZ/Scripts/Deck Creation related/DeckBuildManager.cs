//デッキ編集画面全体

using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using static DeckDataManager;

public class DeckBuildManager : MonoBehaviour
{
    [Header("現在作成中デッキ")]
    public DeckSaveData currentDeckData = new DeckSaveData();

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
        //編集なら読み込む
        if (DeckDataManager.Instance.currentDeckIndex >= 0)
        {
            currentDeckData = DeckDataManager.Instance.LoadDeck(DeckDataManager.Instance.currentDeckIndex);
        }

        //新規なら初期化
        else
        {
            CreateNewDeck();
        }

        UpdateDeckUI();

        RefreshCardList();
    }

    //カード追加
    public void AddCard(CardData card)
    {
        currentDeckData.cards.Add(card);

        Debug.Log(card.cardName + " を追加");

        RefreshCardList();

        UpdateDeckUI();
    }

    //カード削除
    public void RemoveCard(CardData card, GameObject itemObject)
    {
        currentDeckData.cards.Remove(card);

        RefreshCardList();

        UpdateDeckUI();

        Debug.Log(card.cardName + " を削除");
    }

    //カード一覧更新
    public void RefreshCardList()
    {
        //一覧を一旦空にする
        foreach (Transform child in deckListContent)
        {
            Destroy(child.gameObject);
        }

        //カード一覧生成
        foreach (CardData card in currentDeckData.cards)
        {
            GameObject textObj = Instantiate(cardNameTextPrefab, deckListContent);

            DeckListItem item = textObj.GetComponent<DeckListItem>();

            item.Setup(card, this);
        }
    }

    //デッキUI更新
    public void UpdateDeckUI()
    {
        deckCountText.text = "デッキ枚数 : " + currentDeckData.cards.Count;
    }

    //Sword追加
    public void AddSword()
    {
        AddCard(swordCard);
    }

    //Heal追加
    public void AddHeal()
    {
        AddCard(healCard);
    }

    //新規デッキ作成
    public void CreateNewDeck()
    {
        currentDeckData = new DeckSaveData();

        currentDeckData.deckName = "新しいデッキ";

        DeckDataManager.Instance.currentDeckIndex = -1;

        RefreshCardList();

        UpdateDeckUI();

        Debug.Log("新規デッキ作成");
    }

    //デッキ保存
    public void SaveDeck()
    {
        if (DeckDataManager.Instance.currentDeckIndex == -1)
        {
            //新規デッキ
            DeckDataManager.Instance.SaveNewDeck(currentDeckData);
        }
        else
        {
            //既存デッキを更新
            DeckDataManager.Instance.UpdateDeck(currentDeckData);
        }
    }

    //保存して戻る

    public void SaveAndBack()
    {
        SaveDeck();

        SceneLoader sceneLoader = FindFirstObjectByType<SceneLoader>();

        sceneLoader.LoadDeckSelect();
    }
    //public void SaveAndBack()
    //{
    //    SaveDeck();

    //    SceneLoader sceneLoader = FindFirstObjectByType<SceneLoader>();

    //    if (DeckDataManager.Instance.deckSelectMode == DeckSelectMode.SelectForRoom)
    //    {
    //        sceneLoader.LoadRoom();
    //    }
    //    else
    //    {
    //        sceneLoader.LoadDeckSelect();
    //    }
    //}

    //バトル開始
    public void StartBattle()
    {
        SaveDeck();

        SceneManager.LoadScene("BattleScene");
    }

    //デッキ保存
    //public void SaveDeck()
    //{
    //    if (DeckDataManager.Instance.currentDeckIndex == -1)
    //    {
    //        //新規デッキ
    //        DeckDataManager.Instance.SaveNewDeck(currentDeckData);
    //    }
    //    else
    //    {
    //        //既存デッキを更新
    //        DeckDataManager.Instance.UpdateDeck(currentDeckData);
    //    }

    //    SceneLoader sceneLoader = FindFirstObjectByType<SceneLoader>();

    //    sceneLoader.LoadDeckSelect();
    //}

    //public void StartBattle()
    //{
    //    SaveDeck();

    //    SceneManager.LoadScene("BattleScene");
    //}
}