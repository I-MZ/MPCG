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

    //対象選択中か
    [HideInInspector]
    public bool isSelectingTarget = false;

    //使用中のカード
    [HideInInspector]
    public CardData selectedCard;

    //カード使用者
    [HideInInspector]
    public Player selectedUser;

    //選択中のカードUI
    [HideInInspector]
    public CardUI selectedCardUI;

    [HideInInspector]
    public bool isSelectingMinionTarget = false;

    [HideInInspector]
    public Minion selectedMinion;

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

        foreach (Minion minion in currentPlayer.minions)
        {
            minion.canAttack = true;

            minion.hasAttacked = false;
        }

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

        Debug.Log("現在Index : " + currentPlayerIndex);
        Debug.Log("現在Player : " + currentPlayer.playerName);
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

    public List<Player> GetEnemies(Player owner)
    {
        List<Player> enemies = new List<Player>();

        foreach (Player player in players)
        {
            if (player != owner)
            {
                enemies.Add(player);
            }
        }

        return enemies;
    }

    //敗北プレイヤーをターン順から除外
    public void RemovePlayer(Player player)
    {
        //何番目にいるか取得
        int removeIndex = players.IndexOf(player);

        if (removeIndex == -1)
        {
            return;
        }

        //リストから削除
        players.Remove(player);

        Debug.Log(player.playerName + " をターン順から除外");

        //現在のインデックス調整
        if (removeIndex < currentPlayerIndex)
        {
            currentPlayerIndex--;
        }

        //範囲外になったら先頭へ
        if (currentPlayerIndex >= players.Count)
        {
            currentPlayerIndex = 0;
        }
    }

    //ゲーム終了判定
    public void GameOver(Player loser)
    {
        //ターン順から除外
        RemovePlayer(loser);

        //残り1人なら勝利
        if (players.Count == 1)
        {
            isGameOver = true;

            gameOverText.text =
                players[0].playerName + " WIN";
        }
    }
}