using System.Collections.Generic;
using UnityEngine;

public class BattleManager : MonoBehaviour
{
    public static BattleManager Instance;

    [Header("スポーン位置")]
    public Transform[] spawnPoints;

    [Header("参加プレイヤー")]
    public List<Player> players = new();

    public Player currentPlayer;

    private int currentPlayerIndex = 0;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        RegisterPlayers();

        ShuffleTurnOrder();

        StartBattle();
    }

    void RegisterPlayers()
    {
        players.Clear();

        NetworkPlayer[] networkPlayers =
            FindObjectsByType<NetworkPlayer>(FindObjectsSortMode.None);

        foreach (NetworkPlayer networkPlayer in networkPlayers)
        {
            Player player = networkPlayer.GetComponent<Player>();

            if (player != null)
            {
                players.Add(player);
            }
        }

        Debug.Log("参加人数 : " + players.Count);
    }

    void ShuffleTurnOrder()
    {
        for (int i = 0; i < players.Count; i++)
        {
            int randomIndex = Random.Range(i, players.Count);

            Player temp = players[i];

            players[i] = players[randomIndex];

            players[randomIndex] = temp;
        }

        Debug.Log("ターン順シャッフル完了");
    }

    void StartBattle()
    {
        if (players.Count == 0)
            return;

        currentPlayerIndex = 0;

        currentPlayer = players[0];

        Debug.Log("最初のターン : " + currentPlayer.playerName);
    }
}