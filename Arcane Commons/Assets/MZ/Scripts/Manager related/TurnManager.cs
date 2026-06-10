//ターン進行を管理するコード

//ターン進行を管理するコード

using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TurnManager : MonoBehaviour
{
    public static TurnManager Instance;

    [Header("カード使用制限")]
    public bool canUseCard = true;

    [Header("参加プレイヤー")]
    public List<Player> players = new List<Player>();

    [Header("現在のターンプレイヤー")]
    public Player currentPlayer;

    //現在のプレイヤー番号
    private int currentPlayerIndex = 0;

    [Header("ターン表示")]
    public TMP_Text turnText;

    [Header("ゲーム終了表示")]
    public TMP_Text gameOverText;

    [HideInInspector]
    public bool isGameOver = false;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        //参加者がいない
        if (players.Count == 0)
        {
            Debug.LogError("参加プレイヤーがいません");
            return;
        }

        //Noneが混ざっていないか確認
        foreach (Player player in players)
        {
            if (player == null)
            {
                Debug.LogError("Playersリストに None が設定されています");
                return;
            }
        }

        currentPlayerIndex = 0;
        currentPlayer = players[currentPlayerIndex];

        StartTurn();
    }

    //ターン開始
    public void StartTurn()
    {
        if (isGameOver)
        {
            return;
        }

        Debug.Log(currentPlayer.playerName + " のターン開始");

        turnText.text =
            currentPlayer.playerName + " のターン";

        canUseCard = true;

        DeckManager.Instance.DrawCard(currentPlayer);
    }

    //ターン終了
    public void EndTurn()
    {
        Debug.Log(currentPlayer.playerName + " のターン終了");

        canUseCard = false;

        ChangeTurn();

        StartTurn();
    }

    //ターン変更
    void ChangeTurn()
    {
        currentPlayerIndex++;

        if (currentPlayerIndex >= players.Count)
        {
            currentPlayerIndex = 0;
        }

        currentPlayer = players[currentPlayerIndex];
    }

    //敵取得（現状は2人戦用）
    public Player GetEnemyPlayer()
    {
        foreach (Player player in players)
        {
            if (player != currentPlayer)
            {
                return player;
            }
        }

        return null;
    }

    //ゲーム終了
    public void GameOver(Player loser)
    {
        isGameOver = true;

        foreach (Player player in players)
        {
            if (player != loser)
            {
                gameOverText.text =
                    player.playerName + " WIN";

                break;
            }
        }
    }
}