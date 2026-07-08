//ロビー画面全体を管理するコード

using UnityEngine;
using UnityEngine.SceneManagement;

public class LobbyManager : MonoBehaviour
{
    //デッキ編集画面へ
    public void OpenDeckSelect()
    {
        SceneManager.LoadScene("DeckSelectScene");
    }

    //ルーム画面へ
    public void OpenRoomScene()
    {
        SceneManager.LoadScene("RoomScene");
    }

    //ゲーム終了
    public void QuitGame()
    {
        Debug.Log("ゲーム終了");

        Application.Quit();
    }
}