using System.Collections.Generic;
using UnityEngine;

public class RoomPlayerManager : MonoBehaviour
{
    public static RoomPlayerManager Instance;

    public NetworkPlayer localPlayer;

    public RoomNetworkUI roomNetworkUI;

    [Header("参加プレイヤー")]
    public List<NetworkPlayer> players = new List<NetworkPlayer>();

    [Header("UI管理")]
    public RoomManager roomManager;

    private void Start()
    {
        RefreshPlayerListFromNetwork();
    }

    public void RefreshPlayerListFromNetwork()
    {
        players.Clear();

        NetworkPlayer[] allPlayers =
            FindObjectsOfType<NetworkPlayer>();

        foreach (NetworkPlayer player in allPlayers)
        {
            if (!players.Contains(player))
            {
                players.Add(player);

                if (player.isLocalPlayer)
                {
                    localPlayer = player;
                }
            }
        }

        roomManager.RefreshPlayerList(players);

        UpdateReadyButton();
    }

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

        UpdateReadyButton();
    }

    public void ToggleReady()
    {
        if (localPlayer == null)
            return;

        localPlayer.CmdToggleReady();
    }

    public void UpdateReadyButton()
    {
        if (localPlayer == null)
            return;

        if (roomNetworkUI.readyButtonText == null)
            return;

        roomNetworkUI.readyButtonText.text = localPlayer.isReady ? "キャンセル" : "準備完了";
    }
}