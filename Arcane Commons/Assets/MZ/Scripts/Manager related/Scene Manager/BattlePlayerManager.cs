using System.Collections.Generic;
using UnityEngine;

public class BattlePlayerManager : MonoBehaviour
{
    public static BattlePlayerManager Instance;

    [Header("プレイヤー出現位置")]
    public Transform[] spawnPoints;

    [Header("参加プレイヤー")]
    public List<Player> players = new();

    private bool battleInitialized = false;

    private void Awake()
    {
        Instance = this;
    }

    // 通信で生成されたPlayerを登録
    public void RegisterPlayer(Player player)
    {
        if (players.Contains(player))
            return;

        players.Add(player);

        Debug.Log($"BattlePlayer登録 : {player.playerName}");
        Debug.Log($"現在人数 : {players.Count}");

        ArrangePlayers();

        Debug.Log("===== Manager確認 =====");
        Debug.Log("<1>");
        Debug.Log("DeckManager = " + DeckManager.Instance);
        Debug.Log("TurnManager = " + TurnManager.Instance);
        Debug.Log("BattleUIManager = " + BattleUIManager.Instance);
        Debug.Log("<2>");

        Player localPlayer = null;

        Debug.Log("===== Player一覧 =====");

        foreach (Player p in players)
        {
            NetworkPlayer np = p.GetComponent<NetworkPlayer>();

            Debug.Log( $"Player = {p.playerName}  Local = {(np != null ? np.isLocalPlayer.ToString() : "NetworkPlayer無し")}");

            if (np != null && np.isLocalPlayer)
            {
                localPlayer = p;
            }
        }

        if (localPlayer != null)
        {
            Debug.Log("ローカルプレイヤー発見 : " + localPlayer.playerName);

            BattleUIManager.Instance.InitializeUI(localPlayer);
            BattleUIManager.Instance.CreateEnemyList(players);
        }
        else
        {
            Debug.LogWarning("ローカルプレイヤーが見つかりません");
        }

        // 確認用（後でBattleInitializerへ移動予定）
        if (!battleInitialized &&
            players.Count >= 2 &&
            DeckManager.Instance != null &&
            TurnManager.Instance != null)
        {
            battleInitialized = true;

            Debug.Log("===== バトル初期化開始 =====");

            DeckManager.Instance.InitializeDeck();

            TurnManager.Instance.InitializeBattle();

            Debug.Log("===== バトル初期化終了 =====");
        }
    }

    // プレイヤー退出
    public void UnregisterPlayer(Player player)
    {
        if (!players.Contains(player))
            return;

        players.Remove(player);

        Debug.Log($"BattlePlayer退出 : {player.playerName}");

        ArrangePlayers();
    }

    // 配置
    private void ArrangePlayers()
    {
        for (int i = 0; i < players.Count; i++)
        {
            if (i >= spawnPoints.Length)
                break;

            players[i].transform.position = spawnPoints[i].position;

            //BattleUIManager.Instance.BindPlayer(players[i], i);
        }
    }


}