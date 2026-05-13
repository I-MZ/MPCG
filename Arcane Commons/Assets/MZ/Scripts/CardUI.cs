using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CardUI : MonoBehaviour
{
    public CardData cardData;

    public TMP_Text text;

    private PlayerHealth playerHealth;

    void Start()
    {
        playerHealth = FindObjectOfType<PlayerHealth>();

        GetComponent<Button>().onClick.AddListener(UseCard);
    }

    public void Setup(CardData card)
    {
        cardData = card;

        text.text = card.cardName;
    }

    public void UseCard()
    {
        Debug.Log(cardData.cardName + " ‚ðŽg—p");

        playerHealth.TakeDamage(cardData.attack);

        Destroy(gameObject);
    }
}