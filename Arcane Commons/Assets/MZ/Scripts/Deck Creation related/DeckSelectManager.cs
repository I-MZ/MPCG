//保存済みデッキ一覧画面を管理するコード

using UnityEngine;

public class DeckSelectManager : MonoBehaviour
{
    [Header("一覧表示場所")]
    public Transform content;

    [Header("デッキ一覧プレハブ")]
    public GameObject deckItemPrefab;

    private void Start()
    {
        RefreshList();
    }

    //デッキ一覧更新
    public void RefreshList()
    {
        //一覧を一旦空にする
        foreach (Transform child in content)
        {
            Destroy(child.gameObject);
        }

        //保存済みデッキを生成
        for (int i = 0; i < DeckDataManager.Instance.savedDecks.Count; i++)
        {
            DeckSaveData deck = DeckDataManager.Instance.savedDecks[i];

            GameObject obj = Instantiate(deckItemPrefab, content);

            DeckSelectItem item = obj.GetComponent<DeckSelectItem>();

            item.Setup(deck, i);
        }
    }
}