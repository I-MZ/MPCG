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

    // クライアント参加
    public override void OnStartClient()
    {
        base.OnStartClient();

        Debug.Log("参加プレイヤー追加");

        RefreshRoom();

        // Player取得
        //Player battlePlayer = GetComponent<Player>();

        // ←ここでデッキを読み込む
        battlePlayer.LoadDeck();

        // バトルシーンならBattlePlayerManagerへ登録
        if (BattlePlayerManager.Instance != null)
        {
            BattlePlayerManager.Instance.RegisterPlayer(battlePlayer);
        }
    }

    // クライアント退出
    public override void OnStopClient()
    {
        base.OnStopClient();

        if (BattlePlayerManager.Instance != null)
        {
            BattlePlayerManager.Instance.UnregisterPlayer(battlePlayer);
        }
    }

    void OnPlayerNameChanged(string oldName, string newName)
    {
        RefreshRoom();
    }

    void OnReadyChanged(bool oldValue, bool newValue)
    {
        RefreshRoom();

        if (isLocalPlayer && RoomNetworkUI.Instance != null)
        {
            RoomNetworkUI.Instance.UpdateReadyButton(newValue);
        }

        if (NetworkServer.active && RoomNetworkUI.Instance != null)
        {
            RoomNetworkUI.Instance.RefreshStartButton();
        }
    }

    void RefreshRoom()
    {
        if (RoomManager.Instance == null)
            return;

        RoomManager.Instance.RefreshPlayerList(
            ArcaneNetworkManager.Instance.players);
    }

    [Command]
    public void CmdToggleReady()
    {
        isReady = !isReady;
    }
}

//public class NetworkPlayer : NetworkBehaviour
//{
//    private Player battlePlayer;

//    [SyncVar(hook = nameof(OnPlayerNameChanged))]
//    public string playerName = "Player";

//    [SyncVar(hook = nameof(OnReadyChanged))]
//    public bool isReady = false;

//    private void Awake()
//    {
//        battlePlayer = GetComponent<Player>();
//    }

//    //public override void OnStartServer()
//    //{
//    //    base.OnStartServer();

//    //    Debug.Log("===== OnStartServer =====");

//    //    ArcaneNetworkManager.Instance.RegisterPlayer(this);
//    //}

//    //public override void OnStopServer()
//    //{
//    //    base.OnStopServer();

//    //    Debug.Log("===== OnStopServer =====");

//    //    ArcaneNetworkManager.Instance.UnregisterPlayer(this);
//    //}

//    public override void OnStartClient()
//    {
//        base.OnStartClient();

//        Debug.Log("参加プレイヤー追加");

//        RefreshRoom();

//        Player battlePlayer = GetComponent<Player>();

//        BattlePlayerManager.Instance.RegisterPlayer(battlePlayer);
//    }

//    void OnPlayerNameChanged(string oldName, string newName)
//    {
//        RefreshRoom();
//    }

//    void OnReadyChanged(bool oldValue, bool newValue)
//    {
//        RefreshRoom();

//        if (isLocalPlayer && RoomNetworkUI.Instance != null)
//        {
//            RoomNetworkUI.Instance.UpdateReadyButton(newValue);
//        }

//        if (NetworkServer.active)
//        {
//            RoomNetworkUI.Instance.RefreshStartButton();
//        }
//    }

//    void RefreshRoom()
//    {
//        if (RoomManager.Instance == null)
//            return;

//        RoomManager.Instance.RefreshPlayerList(
//            ArcaneNetworkManager.Instance.players);
//    }

//    [Command]
//    public void CmdToggleReady()
//    {
//        isReady = !isReady;
//    }
//}


//using Mirror;
//using UnityEngine;

//public class NetworkPlayer : NetworkBehaviour
//{
//    private Player battlePlayer;

//    [SyncVar(hook = nameof(OnPlayerNameChanged))]
//    public string playerName = "Player";

//    [SyncVar(hook = nameof(OnReadyChanged))]
//    public bool isReady = false;

//    private void Awake()
//    {
//        battlePlayer = GetComponent<Player>();
//    }

//    public override void OnStartClient()
//    {
//        ArcaneNetworkManager.Instance.RegisterPlayer(this);

//        if (RoomPlayerManager.Instance != null)
//        {
//            RoomPlayerManager.Instance.RefreshRoom();
//        }

//        Debug.Log("参加プレイヤー追加");
//    }

//    public override void OnStopClient()
//    {
//        if (ArcaneNetworkManager.Instance != null)
//        {
//            ArcaneNetworkManager.Instance.UnregisterPlayer(this);
//        }
//    }

//    void OnPlayerNameChanged(string oldName, string newName)
//    {
//        if (RoomPlayerManager.Instance != null)
//        {
//            RoomPlayerManager.Instance.RefreshRoom();
//        }
//    }

//    void OnReadyChanged(bool oldValue, bool newValue)
//    {
//        if (RoomPlayerManager.Instance != null)
//        {
//            RoomPlayerManager.Instance.RefreshRoom();
//        }
//    }

//    [Command]
//    public void CmdToggleReady()
//    {
//        isReady = !isReady;
//    }

//    public override void OnStartServer()
//    {
//        base.OnStartServer();

//        ArcaneNetworkManager.Instance.RegisterPlayer(this);
//    }

//using Mirror;
//using UnityEngine;

//public class NetworkPlayer : NetworkBehaviour
//{
//    private Player battlePlayer;

//    [SyncVar(hook = nameof(OnPlayerNameChanged))]
//    public string playerName = "Player";

//    [SyncVar(hook = nameof(OnReadyChanged))]
//    public bool isReady = false;

//    private void Awake()
//    {
//        battlePlayer = GetComponent<Player>();
//    }

//    public override void OnStartClient()
//    {
//        ArcaneNetworkManager.Instance.RegisterPlayer(this);

//        RoomPlayerManager.Instance.AddPlayer(this);

//        if (isLocalPlayer)
//        {
//            RoomPlayerManager.Instance.localPlayer = this;

//            Debug.Log("ローカルプレイヤー登録");
//        }

//        Debug.Log("参加プレイヤー追加");
//    }

//    public override void OnStopClient()
//    {
//        ArcaneNetworkManager.Instance.UnregisterPlayer(this);
//    }

//    void OnPlayerNameChanged(string oldName, string newName)
//    {
//        RoomPlayerManager.Instance.RefreshRoom();
//    }

//    void OnReadyChanged(bool oldValue, bool newValue)
//    {
//        RoomPlayerManager.Instance.RefreshRoom();
//    }

//    [Command]
//    public void CmdToggleReady()
//    {
//        isReady = !isReady;
//    }
//}


//public override void OnStartServer()
//{
//    base.OnStartServer();

//    ArcaneNetworkManager.Instance.RegisterPlayer(this);
//}

//public override void OnStopServer()
//{
//    base.OnStopServer();

//    ArcaneNetworkManager.Instance.UnregisterPlayer(this);
//}

//public override void OnStartClient()
//{
//    base.OnStartClient();

//    Debug.Log("参加プレイヤー追加");

//    // ルーム更新
//    RefreshRoom();

//    // バトルシーンならBattlePlayerManagerへ登録
//    if (BattlePlayerManager.Instance != null)
//    {
//        BattlePlayerManager.Instance.RegisterPlayer(battlePlayer);
//    }
//}