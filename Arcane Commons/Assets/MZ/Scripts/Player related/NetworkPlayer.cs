using Mirror;
using UnityEngine;

public class NetworkPlayer : NetworkBehaviour
{
    private Player battlePlayer;

    [SyncVar(hook = nameof(OnPlayerNameChanged))]
    public string playerName = "Player";

    [SyncVar(hook = nameof(OnReadyChanged))]
    public bool isReady = false;

    private void Awake()
    {
        battlePlayer = GetComponent<Player>();
    }

    public override void OnStartClient()
    {
        RoomPlayerManager.Instance.AddPlayer(this);

        if (isLocalPlayer)
        {
            RoomPlayerManager.Instance.localPlayer = this;

            Debug.Log("ローカルプレイヤー登録");
        }

        Debug.Log("参加プレイヤー追加");
    }

    void OnPlayerNameChanged(string oldName, string newName)
    {
        RoomPlayerManager.Instance.RefreshRoom();
    }

    void OnReadyChanged(bool oldValue, bool newValue)
    {
        RoomPlayerManager.Instance.RefreshRoom();
    }

    [Command]
    public void CmdToggleReady()
    {
        isReady = !isReady;
    }
}

//using Mirror;
//using UnityEngine;

//public class NetworkPlayer : NetworkBehaviour
//{
//    [SyncVar]
//    public string playerName;

//    [SyncVar]
//    public bool isReady = false;

//    private Player battlePlayer;

//    private void Awake()
//    {
//        battlePlayer = GetComponent<Player>();
//    }

//    public override void OnStartServer()
//    {
//        playerName = "Player " + netId;
//    }

//    public override void OnStartClient()
//    {
//        RoomPlayerManager.Instance.AddPlayer(this);

//        Debug.Log("参加プレイヤー追加");
//    }

//    [Command]
//    public void CmdSetReady(bool ready)
//    {
//        isReady = ready;
//    }
//}