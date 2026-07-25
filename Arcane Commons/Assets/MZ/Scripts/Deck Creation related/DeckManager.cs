//デッキ・山札関係

using System.Collections.Generic;
using UnityEngine;

public class DeckManager : MonoBehaviour
{
    public static DeckManager Instance;

    [Header("戦闘用共通デッキ")]
    public List<CardData> battleDeck = new List<CardData>();

    [Header("捨て札")]
    public List<CardData> discardPile = new List<CardData>();

    [Header("参加プレイヤー")]
    public List<Player> players = new List<Player>();　//通信用に変更

    [Header("カードプレハブ")]
    public GameObject cardPrefab;

    private bool initialized = false;

    //手札表示をプレイヤーcs側に任せてみてるから一旦コメントアウト
    //[Header("手札表示場所")]
    //public Transform handArea;

    private void Awake()
    {
        Instance = this;
    }

    //ゲーム開始時
    //private void Start()
    //{
    //    players = BattlePlayerManager.Instance.players;

    //    CreateBattleDeck();
    //}

    //ゲーム開始時
    public void InitializeDeck()
    {
        if (initialized)
            return;

        initialized = true;

        players = BattlePlayerManager.Instance.players;

        CreateBattleDeck();
    }

    //共通デッキ作成
    public void CreateBattleDeck()
    {
        //一旦空にする
        battleDeck.Clear();

        //全プレイヤーのデッキを追加
        foreach (Player player in players) //通信用に変更したけどこっちに戻す
        //foreach (Player player in BattlePlayerManager.Instance.players)
        {
            // ← ここだけ追加
            Debug.Log(player.playerName + " deck = " + player.deck.Count);

            foreach (CardData card in player.deck)
            {
                battleDeck.Add(card);
            }
        }

        //シャッフル
        ShuffleDeck();

        Debug.Log("共通デッキ作成完了");
        Debug.Log("デッキ枚数 : " + battleDeck.Count);

        //初手5枚配布
        foreach (Player player in players)
        {
            for (int i = 0; i < 5; i++)
            {
                DrawCard(player);
            }
        }
    }

    //シャッフル
    public void ShuffleDeck()
    {
        for (int i = 0; i < battleDeck.Count; i++)
        {
            int randomIndex =
                Random.Range(i, battleDeck.Count);

            CardData temp = battleDeck[i];

            battleDeck[i] = battleDeck[randomIndex];

            battleDeck[randomIndex] = temp;
        }

        Debug.Log("デッキをシャッフル");
    }

    //ドロー
    public void DrawCard(Player targetPlayer)
    {
        //デッキ切れ
        if (battleDeck.Count <= 0)
        {
            Debug.Log("デッキ切れ");
            return;
        }

        //一番上のカード取得
        CardData drawCard = battleDeck[0];

        //手札へ追加
        targetPlayer.hand.Add(drawCard);

        //デッキから削除
        battleDeck.RemoveAt(0);

        Debug.Log
        (
            targetPlayer.playerName + "は" +drawCard.cardName +"を引いた"
        );

        //UI生成
        GameObject cardObj =
          //Instantiate(cardPrefab, handArea);  //新しいのを作ったから一旦コメントアウト
            Instantiate(cardPrefab, targetPlayer.handArea);

        CardUI cardUI =cardObj.GetComponent<CardUI>();

        cardUI.Setup(drawCard, targetPlayer);
    }
}