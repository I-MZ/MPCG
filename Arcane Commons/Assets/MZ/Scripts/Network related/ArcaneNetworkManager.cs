
using System.Collections.Generic;
using Mirror;
using UnityEngine;

public class ArcaneNetworkManager : NetworkManager
{
    public static ArcaneNetworkManager Instance;

    // 接続中プレイヤー一覧
    public readonly List<NetworkPlayer> players = new();

    public override void Awake()
    {
        base.Awake();

        Instance = this;
    }

    public override void OnServerAddPlayer(NetworkConnectionToClient conn)
    {
        base.OnServerAddPlayer(conn);

        Debug.Log("プレイヤー参加！");
    }

    public void RegisterPlayer(NetworkPlayer player)
    {
        if (players.Contains(player))
            return;

        players.Add(player);

        Debug.Log($"登録 : {player.playerName}");

        //if (RoomPlayerManager.Instance != null)
        //{
        //    RoomPlayerManager.Instance.RefreshRoom();
        //}
        if (RoomManager.Instance != null)
        {
            RoomManager.Instance.RefreshPlayerList(players);
        }
    }

    public void UnregisterPlayer(NetworkPlayer player)
    {
        if (!players.Contains(player))
            return;

        players.Remove(player);

        Debug.Log($"退出 : {player.playerName}");

        //if (RoomPlayerManager.Instance != null)
        //{
        //    RoomPlayerManager.Instance.RefreshRoom();
        //}
        if (RoomManager.Instance != null)
        {
            RoomManager.Instance.RefreshPlayerList(players);
        }
    }
}

//using Mirror;
//using UnityEngine;
//using System.Collections.Generic;

//public class ArcaneNetworkManager : NetworkManager
//{
//    public static ArcaneNetworkManager Instance;

//    public List<NetworkPlayer> players = new List<NetworkPlayer>();

//    public override void Awake()
//    {
//        base.Awake();

//        Instance = this;
//    }

//    public override void OnServerAddPlayer(NetworkConnectionToClient conn)
//    {
//        base.OnServerAddPlayer(conn);

//        Debug.Log("プレイヤー参加！");
//    }

//    public void RegisterPlayer(NetworkPlayer player)
//    {
//        if (!players.Contains(player))
//        {
//            players.Add(player);

//            Debug.Log("登録 : " + player.playerName);
//        }
//    }

//    public void UnregisterPlayer(NetworkPlayer player)
//    {
//        if (players.Contains(player))
//        {
//            players.Remove(player);

//            Debug.Log("退出 : " + player.playerName);
//        }
//    }
//}