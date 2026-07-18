using Mirror;
using UnityEngine;

public class ArcaneNetworkManager : NetworkManager
{
    public override void OnServerAddPlayer(NetworkConnectionToClient conn)
    {
        base.OnServerAddPlayer(conn);

        Debug.Log("プレイヤー参加！");
    }
}