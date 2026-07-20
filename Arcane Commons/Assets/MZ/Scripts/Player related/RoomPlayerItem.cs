using TMPro;
using UnityEngine;

public class RoomPlayerItem : MonoBehaviour
{
    public TMP_Text playerNameText;
    public TMP_Text readyStateText;

    public void SetData(string playerName, bool isReady)
    {
        playerNameText.text = playerName;

        readyStateText.text = isReady ? "€”õŠ®—¹" : "€”õ’†";
    }

}