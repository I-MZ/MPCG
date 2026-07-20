//設計上不要になった。ありがとう、お疲れ様

//using System.Collections.Generic;
//using UnityEngine;

//public class RoomPlayerManager : MonoBehaviour
//{
//    public static RoomPlayerManager Instance;

//    public NetworkPlayer localPlayer;

//    public RoomNetworkUI roomNetworkUI;

//    [Header("UI管理")]
//    public RoomManager roomManager;

//    [Header("参加プレイヤー")]
//    public List<NetworkPlayer> players = new List<NetworkPlayer>();

//    //private void Start()
//    //{
//    //    RefreshPlayerListFromNetwork();
//    //}
//    private void OnEnable()
//    {
//        Invoke(nameof(RefreshPlayerListFromNetwork), 0.1f);
//    }

//    public void RefreshPlayerListFromNetwork()
//    {
//        players.Clear();

//        NetworkPlayer[] allPlayers =
//            FindObjectsOfType<NetworkPlayer>();

//        foreach (NetworkPlayer player in allPlayers)
//        {
//            if (!players.Contains(player))
//            {
//                players.Add(player);

//                if (player.isLocalPlayer)
//                {
//                    localPlayer = player;
//                }
//            }
//        }

//        Debug.Log("プレイヤー数 : " + players.Count);

//        roomManager.RefreshPlayerList(players);

//        UpdateReadyButton();
//    }

//    private void Awake()
//    {
//        Instance = this;
//    }

//    public void AddPlayer(NetworkPlayer player)
//    {
//        if (players.Contains(player))
//            return;

//        players.Add(player);

//        Debug.Log("プレイヤー参加 : " + player.netId);

//        roomManager.RefreshPlayerList(players);
//    }

//    public void RefreshRoom()
//    {
//        roomManager.RefreshPlayerList(players);

//        UpdateReadyButton();
//    }

//    public void ToggleReady()
//    {
//        if (localPlayer == null)
//            return;

//        localPlayer.CmdToggleReady();
//    }

//    public void UpdateReadyButton()
//    {
//        if (localPlayer == null)
//            return;

//        if (roomNetworkUI.readyButtonText == null)
//            return;

//        roomNetworkUI.readyButtonText.text = localPlayer.isReady ? "キャンセル" : "準備完了";
//    }

//    //一旦不要になったみたい
//    //public void RefreshPlayerListFromNetwork()

//    //public List<NetworkPlayer> Players
//    //{
//    //    get
//    //    {
//    //        return ArcaneNetworkManager.Instance.players;
//    //    }
//    //}
//}