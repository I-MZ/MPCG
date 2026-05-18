//デッキ・山札関係

using System.Collections.Generic;
using UnityEngine;

public class DeckManager : MonoBehaviour
{
    public static DeckManager Instance;

    [Header("戦闘用共通デッキ")]
    public List<CardData> battleDeck = new List<CardData>();

    [Header("参加プレイヤー")]
    public List<Player> players = new List<Player>();

    [Header("カードプレハブ")]
    public GameObject cardPrefab;

    [Header("手札表示場所")]
    public Transform handArea;

    private void Awake()
    {
        Instance = this;
    }

    //ゲーム開始時
    private void Start()
    {
        CreateBattleDeck();
    }

    //共通デッキ作成
    public void CreateBattleDeck()
    {
        //一旦空にする
        battleDeck.Clear();

        //全プレイヤーのデッキを追加
        foreach (Player player in players)
        {
            foreach (CardData card in player.deck)
            {
                battleDeck.Add(card);
            }
        }

        //シャッフル
        ShuffleDeck();

        Debug.Log("共通デッキ作成完了");
        Debug.Log("デッキ枚数 : " + battleDeck.Count);
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

        Debug.Log(
            targetPlayer.playerName +
            " は " +
            drawCard.cardName +
            " を引いた"
        );

        //UI生成
        GameObject cardObj =
            Instantiate(cardPrefab, handArea);

        CardUI cardUI =
            cardObj.GetComponent<CardUI>();

        cardUI.Setup(drawCard);
    }
}


//using System.Collections.Generic;
//using UnityEngine;

//public class DeckManager : MonoBehaviour
//{
//    public List<CardData> deck = new List<CardData>();

//    public HandManager handManager;
//    /* ドローボタンの為にコメントアウト中
//    void Start()
//    {
//        DrawCard();//デッキからカードをドローする
//    }
//    */
//    public void DrawCard()
//    {
//        if (deck.Count <= 0)
//        {
//            Debug.Log("Deck Empty");
//            return;
//        }

//        CardData drawCard = deck[0];

//        Debug.Log("Draw : " + drawCard.cardName);

//        handManager.AddCard(drawCard);

//        deck.RemoveAt(0);
//    }
//}