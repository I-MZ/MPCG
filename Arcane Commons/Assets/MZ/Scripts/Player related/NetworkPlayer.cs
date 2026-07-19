using Mirror;
using UnityEngine;

public class NetworkPlayer : NetworkBehaviour
{
    [SyncVar]
    public string playerName;

    private Player battlePlayer;

    private void Awake()
    {
        battlePlayer = GetComponent<Player>();
    }

    public override void OnStartServer()
    {
        playerName = "Player " + netId;
    }

    public override void OnStartClient()
    {
        RoomPlayerManager.Instance.AddPlayer(this);

        Debug.Log("参加プレイヤー追加");
    }
}