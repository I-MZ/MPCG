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
        //対象選択中
        if (TurnManager.Instance.isSelectingTarget)
        {
            if (TurnManager.Instance.selectedCardUI == this)
            {
                Debug.Log("対象選択をキャンセルしました");

                TurnManager.Instance.isSelectingTarget = false;

                TurnManager.Instance.canUseCard = true;

                TurnManager.Instance.selectedCard = null;

                TurnManager.Instance.selectedUser = null;

                TurnManager.Instance.selectedCardUI = null;

                return;
            }

            Debug.Log("現在、対象を選択中です");

            return;
        }

        //ゲーム終了中
        if (TurnManager.Instance.isGameOver)
        {
            return;
        }

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

        //攻撃カードなら対象選択モード
        if (cardData.effectType == EffectType.Damage)
        {
            TurnManager.Instance.isSelectingTarget = true;

            TurnManager.Instance.selectedCard = cardData;

            TurnManager.Instance.selectedUser = ownerPlayer;

            TurnManager.Instance.selectedCardUI = this;

            Debug.Log("攻撃対象を選択してください");

            return;
        }

        //攻撃以外は即発動
        CardEffectManager.Instance.UseCardEffect(
            cardData,
            ownerPlayer,
            TurnManager.Instance.GetEnemyPlayer()
        );

        //手札から削除
        ownerPlayer.hand.Remove(cardData);

        Debug.Log(cardData.cardName + " を手札から削除");

        //現在の手札枚数
        Debug.Log(ownerPlayer.hand.Count);

        //捨て札へ送る
        DeckManager.Instance.discardPile.Add(cardData);

        Debug.Log(cardData.cardName + " を捨て札へ送った");

        //UI削除
        Destroy(gameObject);
    }
}