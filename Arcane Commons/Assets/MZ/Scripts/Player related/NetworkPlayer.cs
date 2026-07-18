using Mirror;
using UnityEngine;

public class NetworkPlayer : NetworkBehaviour
{
    private Player battlePlayer;

    private void Awake()
    {
        battlePlayer = GetComponent<Player>();
    }

    public override void OnStartLocalPlayer()
    {
        Debug.Log("©•ª‚ÌƒvƒŒƒCƒ„[‚ª¶¬‚³‚ê‚Ü‚µ‚½");
    }
}