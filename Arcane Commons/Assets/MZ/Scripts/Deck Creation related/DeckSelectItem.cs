//デッキ一覧に表示する1つのデッキ情報を管理するコード

using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using static DeckDataManager;

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
        Debug.Log("DeckSelectItem Start");

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

        Debug.Log("現在のモード : " + DeckDataManager.Instance.deckSelectMode);

        if (DeckDataManager.Instance.deckSelectMode == DeckSelectMode.SelectForRoom)
        {
            Debug.Log("ルームへ戻ります");

            SceneManager.LoadScene("RoomScene");
        }
        else
        {
            Debug.Log("デッキ編集へ行きます");

            SceneManager.LoadScene("DeckBuildScene");
        }
    }
}


//public void OpenDeck()
//{
//    DeckDataManager.Instance.LoadDeck(deckIndex);

//    if (DeckDataManager.Instance.deckSelectMode == DeckSelectMode.SelectForRoom)
//    {
//        SceneManager.LoadScene("RoomScene");
//    }
//    else
//    {
//        SceneManager.LoadScene("DeckBuildScene");
//    }
//}
//public void OpenDeck()
//{
//    DeckDataManager.Instance.LoadDeck(deckIndex);

//    if (DeckDataManager.Instance.isSelectingDeckForRoom)
//    {
//        DeckDataManager.Instance.isSelectingDeckForRoom = false;

//        SceneManager.LoadScene("RoomScene");
//    }
//    else
//    {
//        SceneManager.LoadScene("DeckBuildScene");
//    }
//}