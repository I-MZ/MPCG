//カードの使用制限とかのコード

using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CardUI : MonoBehaviour
{
    public CardData cardData;
    public TMP_Text text;
    private Player player;

    void Start()
    {
        player = FindObjectOfType<Player>();
        GetComponent<Button>().onClick.AddListener(UseCard);
    }

    public void Setup(CardData card)
    {
        cardData = card;
        text.text = card.cardName;
    }

    public void UseCard()
    {
        if (TurnManager.Instance.canUseCard == false)
        {
            Debug.Log("このターンはもう使えない");
            return;
        }

        TurnManager.Instance.canUseCard = false;

        Debug.Log(cardData.cardName + " を使用");
        CardEffectManager.Instance.UseCardEffect(cardData, player, player);
        Destroy(gameObject);
    }
}