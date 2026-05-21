//カードの使用制限とかのコード

using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CardUI : MonoBehaviour
{
    public CardData cardData;

    public TMP_Text text;

    // このカードの持ち主
    private Player ownerPlayer;

    void Start()
    {
        GetComponent<Button>().onClick.AddListener(UseCard);
    }

    // カード情報セット
    public void Setup(CardData card, Player player)
    {
        cardData = card;

        ownerPlayer = player;

        text.text = card.cardName;
    }

    // カード使用
    public void UseCard()
    {
        // 使用制限
        if (TurnManager.Instance.canUseCard == false)
        {
            Debug.Log("このターンはもう使えない");

            return;
        }

        TurnManager.Instance.canUseCard = false;

        Debug.Log(cardData.cardName + " を使用");

        // 効果発動
        CardEffectManager.Instance.UseCardEffect(
        cardData,
        ownerPlayer,
        TurnManager.Instance.enemyPlayer
        );

        Destroy(gameObject);
    }
}