using Mirror;
using TMPro;
using System.Net;
using System.Net.Sockets;
using UnityEngine;

public class RoomNetworkUI : MonoBehaviour
{
    [Header("ホストIP表示")]
    public TMP_Text hostIPText;

    [Header("ゲーム開始ボタン")]
    public GameObject startButton;

    public TMP_Text readyButtonText;

    private void Start()
    {
        if (hostIPText != null &&
            NetworkSession.Instance != null)
        {
            hostIPText.text = "ホストIP : " + NetworkSession.Instance.hostIP;
        }

        if (NetworkServer.active)
        {
            startButton.SetActive(true);
        }
        else
        {
            startButton.SetActive(false);
        }

        RoomManager.Instance.RefreshPlayerList(ArcaneNetworkManager.Instance.players);
    }

    //ルーム終了
    public void StopRoom()
    {
        NetworkManager.singleton.StopHost();

        Debug.Log("Host終了");
    }
}





//[Header("IP入力欄")]
//public TMP_InputField ipInputField;


//private void Start()
//{
//    startButton.SetActive(false);
//}

//private void Start()
//{
//    if (startButton != null)
//    {
//        startButton.SetActive(false);
//    }
//}


//ルーム終了の上
////ルーム作成
//public void CreateRoom()
//{
//    if (NetworkServer.active)
//    {
//        return;
//    }

//    NetworkManager.singleton.StartHost();

//    string ip = GetLocalIPAddress();

//    Debug.Log("取得したIP : " + ip);

//    if (hostIPText != null)
//    {
//        hostIPText.text = "ホストIP : " + ip;
//    }

//    //startButton.SetActive(true);
//    if (startButton != null)
//    {
//        startButton.SetActive(true);
//    }

//    Debug.Log("Host開始");
//}

////ルーム参加
//public void JoinRoom()
//{
//    if (!string.IsNullOrEmpty(ipInputField.text))
//    {
//        NetworkManager.singleton.networkAddress = ipInputField.text;
//    }

//    NetworkManager.singleton.StartClient();

//    Debug.Log("Client開始");
//}

//終了の下
////自分のIPv4アドレス取得
//string GetLocalIPAddress()
//{
//    try
//    {
//        foreach (IPAddress address in Dns.GetHostAddresses(Dns.GetHostName()))
//        {
//            if (address.AddressFamily == AddressFamily.InterNetwork &&
//                !address.ToString().StartsWith("127."))
//            {
//                return address.ToString();
//            }
//        }
//    }
//    catch (System.Exception e)
//    {
//        Debug.LogError("IP取得失敗 : " + e.Message);
//    }

//    return "取得失敗";
//}