//カードの使用制限とかのコード

using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CardUI : MonoBehaviour
{
    //カード情報
    public CardData cardData;

    //カード名表示
    public TMP_Text text;

    //このカードを持っているプレイヤー
    private Player ownerPlayer;

    void Start()
    {
        GetComponent<Button>().onClick.AddListener(UseCard);
    }

    //カード情報セット
    public void Setup(CardData card, Player player)
    {
        cardData = card;

        ownerPlayer = player;

        text.text = card.cardName;
    }

    //カード使用
    public void UseCard()
    {
        //自分のターンじゃない
        if (ownerPlayer != TurnManager.Instance.currentPlayer)
        {
            Debug.Log("相手ターンなので使えない");

            return;
        }

        //カード使用制限
        if (TurnManager.Instance.canUseCard == false)
        {
            Debug.Log("このターンはもう使えない");

            return;
        }

        TurnManager.Instance.canUseCard = false;

        Debug.Log(cardData.cardName + " を使用");

        //カード効果発動
        CardEffectManager.Instance.UseCardEffect(
            cardData,
            ownerPlayer,
            TurnManager.Instance.GetEnemyPlayer()
        );

        // 手札から削除
        ownerPlayer.hand.Remove(cardData);

        Debug.Log(cardData.cardName + " を手札から削除");

        // UI削除
        Destroy(gameObject);
    }
}