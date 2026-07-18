using Mirror;
using UnityEngine;

public class NetworkRoomManager : NetworkManager
{
    public override void OnServerAddPlayer(NetworkConnectionToClient conn)
    {
        base.OnServerAddPlayer(conn);

        Debug.Log("プレイヤー参加 : " + numPlayers);
    }

    public override void OnServerDisconnect(NetworkConnectionToClient conn)
    {
        Debug.Log("プレイヤー退出");

        base.OnServerDisconnect(conn);
    }
}