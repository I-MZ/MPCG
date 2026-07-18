using Mirror;
using TMPro;
using System.Net;
using System.Net.Sockets;
using UnityEngine;

public class RoomNetworkUI : MonoBehaviour
{
    [Header("IP入力欄")]
    public TMP_InputField ipInputField;

    [Header("ホストIP表示")]
    public TMP_Text hostIPText;

    //ルーム作成
    public void CreateRoom()
    {
        NetworkManager.singleton.StartHost();

        string ip = GetLocalIPAddress();

        Debug.Log("取得したIP : " + ip);

        if (hostIPText != null)
        {
            hostIPText.text = "ホストIP : " + ip;
        }
    }

    //ルーム参加
    public void JoinRoom()
    {
        if (!string.IsNullOrEmpty(ipInputField.text))
        {
            NetworkManager.singleton.networkAddress = ipInputField.text;
        }

        NetworkManager.singleton.StartClient();

        Debug.Log("Client開始");
    }

    //ルーム終了
    public void StopRoom()
    {
        NetworkManager.singleton.StopHost();

        Debug.Log("Host終了");
    }

    //自分のIPv4アドレス取得
    string GetLocalIPAddress()
    {
        try
        {
            foreach (IPAddress address in Dns.GetHostAddresses(Dns.GetHostName()))
            {
                if (address.AddressFamily == AddressFamily.InterNetwork &&
                    !address.ToString().StartsWith("127."))
                {
                    return address.ToString();
                }
            }
        }
        catch (System.Exception e)
        {
            Debug.LogError("IP取得失敗 : " + e.Message);
        }

        return "取得失敗";
    }
}