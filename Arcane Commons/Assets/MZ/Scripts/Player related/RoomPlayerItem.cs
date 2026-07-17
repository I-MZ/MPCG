using TMPro;
using UnityEngine;

public class RoomPlayerItem : MonoBehaviour
{
    public TMP_Text playerNameText;

    public void SetPlayerName(string playerName)
    {
        playerNameText.text = playerName;
    }
}