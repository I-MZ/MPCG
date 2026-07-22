using UnityEngine;

public class RoomDeckSelectPanel : MonoBehaviour
{
    public static RoomDeckSelectPanel Instance;

    public GameObject panel;

    public Transform content;

    public GameObject deckItemPrefab;

    private void Awake()
    {
        Instance = this;
    }

    public void OpenPanel()
    {
        panel.SetActive(true);

        RefreshList();
    }

    public void ClosePanel()
    {
        panel.SetActive(false);
    }

    void RefreshList()
    {
        foreach (Transform child in content)
        {
            Destroy(child.gameObject);
        }

        for (int i = 0; i < DeckDataManager.Instance.savedDecks.Count; i++)
        {
            DeckSaveData deck = DeckDataManager.Instance.savedDecks[i];

            GameObject obj =
                Instantiate(deckItemPrefab, content);

            RoomDeckSelectItem item =
                obj.GetComponent<RoomDeckSelectItem>();

            item.Setup(deck, i);
        }
    }
}