using Mirror;
using TMPro;
using UnityEngine;
using System.Net;
using System.Net.Sockets;

public class MatchingNetworkUI : MonoBehaviour
{
    [Header("IP入力欄")]
    public TMP_InputField ipInputField;

    [Header("SceneLoader")]
    public SceneLoader sceneLoader;

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

    public void CreateRoom()
    {
        NetworkManager.singleton.StartHost();

        NetworkSession.Instance.hostIP = GetLocalIPAddress();

        Debug.Log("Host開始");

        //sceneLoader.LoadRoom();
    }

    public void JoinRoom()
    {
        if (!string.IsNullOrEmpty(ipInputField.text))
        {
            NetworkManager.singleton.networkAddress = ipInputField.text;
        }

        NetworkManager.singleton.StartClient();

        Debug.Log("Client開始");

        //sceneLoader.LoadRoom();
    }
}