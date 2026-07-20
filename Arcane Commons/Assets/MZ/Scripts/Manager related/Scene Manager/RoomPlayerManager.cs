using System.Collections.Generic;
using UnityEngine;

public class RoomPlayerManager : MonoBehaviour
{
    public static RoomPlayerManager Instance;

    public NetworkPlayer localPlayer;

    [Header("参加プレイヤー")]
    public List<NetworkPlayer> players = new List<NetworkPlayer>();

    [Header("UI管理")]
    public RoomManager roomManager;

    private void Awake()
    {
        Instance = this;
    }

    public void AddPlayer(NetworkPlayer player)
    {
        if (players.Contains(player))
            return;

        players.Add(player);

        Debug.Log("プレイヤー参加 : " + player.netId);

        roomManager.RefreshPlayerList(players);
    }

    public void RefreshRoom()
    {
        roomManager.RefreshPlayerList(players);
    }

    public void ToggleReady()
    {
        if (localPlayer == null)
            return;

        localPlayer.CmdToggleReady();
    }
}