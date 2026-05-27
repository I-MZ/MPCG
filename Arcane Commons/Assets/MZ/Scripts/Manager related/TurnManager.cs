//ターン進行を管理するコード

using TMPro;
using UnityEngine;

public class TurnManager : MonoBehaviour
{
    public static TurnManager Instance;

    [Header("カード使用制限")]
    public bool canUseCard = true;

    [Header("プレイヤー")]
    public Player player1;

    public Player player2;

    [Header("現在のターンプレイヤー")]
    public Player currentPlayer;

    [Header("ターン表示")]
    public TMP_Text turnText;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        // 最初はPlayer1ターン
        currentPlayer = player1;

        StartTurn();
    }

    // ターン開始
    public void StartTurn()
    {
        Debug.Log(currentPlayer.playerName + " のターン開始");

        //現在ターンテキスト
        turnText.text =currentPlayer.playerName + " のターン";

        canUseCard = true;

        // 現在ターンのプレイヤーがドロー
        DeckManager.Instance.DrawCard(currentPlayer);
    }

    // ターン終了
    public void EndTurn()
    {
        Debug.Log(currentPlayer.playerName + " のターン終了");

        canUseCard = false;

        ChangeTurn();

        StartTurn();
    }

    // ターン変更
    void ChangeTurn()
    {
        // Player1 → Player2
        if (currentPlayer == player1)
        {
            currentPlayer = player2;
        }
        // Player2 → Player1
        else
        {
            currentPlayer = player1;
        }
    }

    // 現在ターンプレイヤーの敵を取得
    public Player GetEnemyPlayer()
    {
        if (currentPlayer == player1)
        {
            return player2;
        }
        else
        {
            return player1;
        }
    }
}