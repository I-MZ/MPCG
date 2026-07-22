using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RoomDeckSelectItem : MonoBehaviour
{
    public TMP_Text deckNameText;

    public TMP_Text classText;

    int deckIndex;

    private void Start()
    {
        GetComponent<Button>().onClick.AddListener(SelectDeck);
    }

    public void Setup(DeckSaveData deck, int index)
    {
        deckIndex = index;

        deckNameText.text = deck.deckName;

        classText.text = deck.classType.ToString();
    }

    void SelectDeck()
    {
        DeckDataManager.Instance.LoadDeck(deckIndex);

        RoomManager.Instance.UpdateDeckUI();

        RoomDeckSelectPanel.Instance.ClosePanel();
    }
}
