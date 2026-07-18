using Mirror;
using UnityEngine;

public class NetworkPlayer : NetworkBehaviour
{
    private Player battlePlayer;

    private void Awake()
    {
        battlePlayer = GetComponent<Player>();
    }

    public override void OnStartClient()
    {
        RoomPlayerManager.Instance.AddPlayer(this);

        Debug.Log("参加プレイヤー追加");
    }
}

//public override void OnStartLocalPlayer()
//    {
//        Debug.Log("自分のプレイヤーが生成されました");

//        RoomPlayerManager.Instance.AddPlayer(this);
//    }