//ルーム画面全体を管理するコード

using TMPro;
using UnityEngine;

public class RoomManager : MonoBehaviour
{
    [Header("現在使用中デッキ")]
    public TMP_Text deckNameText;

    [Header("プレイヤー一覧")]
    public Transform playerListContent;

    public GameObject roomPlayerItemPrefab;

    private void Start()
    {
        UpdateDeckUI();

        //AddPlayer("Player1");
    }

    //使用デッキ表示更新
    public void UpdateDeckUI()
    {
        if (DeckDataManager.Instance.currentDeckIndex < 0)
        {
            deckNameText.text = "デッキ未選択";
            return;
        }

        DeckSaveData deck =
            DeckDataManager.Instance.LoadDeck(
                DeckDataManager.Instance.currentDeckIndex);

        deckNameText.text = deck.deckName;
    }

    //使わなくなったよ
    //public void AddPlayer(string playerName)
    //{
    //    Debug.Log("プレイヤー追加 : " + playerName);

    //    GameObject obj =
    //        Instantiate(roomPlayerItemPrefab, playerListContent);

    //    RoomPlayerItem item =
    //        obj.GetComponent<RoomPlayerItem>();

    //    item.SetPlayerName(playerName);
    //}

    public void RefreshPlayerList(System.Collections.Generic.List<NetworkPlayer> players)
    {
        // 今ある一覧を全部削除
        foreach (Transform child in playerListContent)
        {
            Destroy(child.gameObject);
        }

        // 最新の一覧を生成
        foreach (NetworkPlayer player in players)
        {
            GameObject obj =
                Instantiate(roomPlayerItemPrefab, playerListContent);

            RoomPlayerItem item =
                obj.GetComponent<RoomPlayerItem>();

            //item.SetPlayerName("Player " + player.netId);
            item.SetData(player.playerName,player.isReady);
        }
    }
}