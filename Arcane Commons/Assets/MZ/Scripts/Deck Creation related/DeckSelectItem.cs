//デッキ一覧に表示する1つのデッキ情報を管理するコード

using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class DeckSelectItem : MonoBehaviour
{
    [Header("デッキ名")]
    public TMP_Text deckNameText;

    [Header("クラス")]
    public TMP_Text classText;

    //保存デッキ番号
    private int deckIndex;

    private void Start()
    {
        GetComponent<Button>().onClick.AddListener(OpenDeck);
    }

    //表示更新
    public void Setup(DeckSaveData deck, int index)
    {
        deckNameText.text = deck.deckName;

        classText.text = deck.classType.ToString();

        deckIndex = index;
    }

    //デッキを開く
    public void OpenDeck()
    {
        DeckDataManager.Instance.LoadDeck(deckIndex);

        SceneManager.LoadScene("DeckBuildScene");
    }
}