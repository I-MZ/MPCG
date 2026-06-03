using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DeckListItem : MonoBehaviour
{
    public TMP_Text text;

    private CardData cardData;

    private DeckBuildManager deckBuildManager;

    private void Start()
    {
        GetComponent<Button>().onClick.AddListener(RemoveCard);
    }

    public void Setup(CardData card, DeckBuildManager manager)
    {
        cardData = card;

        deckBuildManager = manager;

        text.text = card.cardName;
    }

    public void RemoveCard()
    {
        deckBuildManager.RemoveCard(cardData, gameObject);
    }
}