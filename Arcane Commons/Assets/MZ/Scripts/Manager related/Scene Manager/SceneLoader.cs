//シーン移動全体を管理するコード

using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    //タイトルへ
    public void LoadTitle()
    {
        SceneManager.LoadScene("TitleScene");
    }

    //ロビーへ
    public void LoadLobby()
    {
        SceneManager.LoadScene("LobbyScene");
    }

    //デッキ一覧へ
    public void LoadDeckSelect()
    {
        SceneManager.LoadScene("DeckSelectScene");
    }

    //デッキ編集へ
    public void LoadDeckBuild()
    {
        SceneManager.LoadScene("DeckBuildScene");
    }

    //新規デッキ作成へ
    public void LoadNewDeckBuild()
    {
        DeckDataManager.Instance.currentDeckIndex = -1;

        SceneManager.LoadScene("DeckBuildScene");
    }

    //ルームからデッキ編集へ
    public void LoadDeckSelectForRoom()
    {
        DeckDataManager.Instance.isSelectingDeckForRoom = true;

        SceneManager.LoadScene("DeckSelectScene");
    }

    //ルームへ
    public void LoadRoom()
    {
        SceneManager.LoadScene("RoomScene");
    }

    //バトルへ
    public void LoadBattle()
    {
        SceneManager.LoadScene("BattleScene");
    }

    //ゲーム終了
    public void QuitGame()
    {
        Debug.Log("ゲーム終了");

        Application.Quit();
    }
}

//1シーン配置で済ませようとしたけどテストプレイの時に毎回タイトルスタートは面倒だったからいったんコメントアウト
//public class SceneLoader : MonoBehaviour
//{
//    public static SceneLoader Instance;

//    private void Awake()
//    {
//        if (Instance == null)
//        {
//            Instance = this;

//            DontDestroyOnLoad(gameObject);
//        }
//        else
//        {
//            Destroy(gameObject);
//        }
//    }

//    //タイトルへ
//    public void LoadTitle()
//    {
//        SceneManager.LoadScene("TitleScene");
//    }

//    //ロビーへ
//    public void LoadLobby()
//    {
//        SceneManager.LoadScene("LobbyScene");
//    }

//    //デッキ一覧へ
//    public void LoadDeckSelect()
//    {
//        SceneManager.LoadScene("DeckSelectScene");
//    }

//    //デッキ編集へ
//    public void LoadDeckBuild()
//    {
//        SceneManager.LoadScene("DeckBuildScene");
//    }

//    //ルームへ
//    public void LoadRoom()
//    {
//        SceneManager.LoadScene("RoomScene");
//    }

//    //バトルへ
//    public void LoadBattle()
//    {
//        SceneManager.LoadScene("BattleScene");
//    }

//    //ゲーム終了
//    public void QuitGame()
//    {
//        Debug.Log("ゲーム終了");

//        Application.Quit();
//    }
//}