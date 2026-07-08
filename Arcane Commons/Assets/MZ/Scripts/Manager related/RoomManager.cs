//ルーム画面全体を管理するコード

using UnityEngine;
using UnityEngine.SceneManagement;

public class RoomManager : MonoBehaviour
{
    //バトル開始
    public void StartBattle()
    {
        SceneManager.LoadScene("BattleScene");
    }

    //ロビーへ戻る
    public void BackLobby()
    {
        SceneManager.LoadScene("LobbyScene");
    }
}