using Mirror;
using UnityEngine;

public class NetworkPlayer : NetworkBehaviour
{
    [SyncVar]
    public string playerName;

    [SyncVar]
    public bool isReady = false;

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

    [Command]
    public void CmdSetReady(bool ready)
    {
        isReady = ready;
    }
}