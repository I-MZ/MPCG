using System.Collections.Generic;
using UnityEngine;

public class BattlePlayerManager : MonoBehaviour
{
    public static BattlePlayerManager Instance;

    [Header("プレイヤー出現位置")]
    public Transform[] spawnPoints;

    [Header("参加プレイヤー")]
    public List<Player> players = new();

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
        Player localPlayer = null;

        foreach (Player p in players)
        {
            NetworkPlayer np = p.GetComponent<NetworkPlayer>();

            if (np != null && np.isLocalPlayer)
            {
                localPlayer = p;
                break;
            }
        }

        if (localPlayer != null)
        {
            BattleUIManager.Instance.InitializeUI(localPlayer);
            BattleUIManager.Instance.CreateEnemyList(players);
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