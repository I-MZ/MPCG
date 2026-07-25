using TMPro;
using UnityEngine;

public class BattleUIManager : MonoBehaviour
{
    public static BattleUIManager Instance;

    [System.Serializable]
    public class PlayerUI
    {
        public GameObject root;

        public TMP_Text hpText;

        public Transform handArea;

        public Transform minionArea;
    }

    public PlayerUI[] playerUI = new PlayerUI[8];

    private void Awake()
    {
        Instance = this;
    }

    public void BindPlayer(Player player, int index)
    {
        if (player == null)
            return;

        if (index >= playerUI.Length)
            return;

        player.handArea = playerUI[index].handArea;

        player.minionArea = playerUI[index].minionArea;

        player.playerName = $"Player {index + 1}";

        UpdateHP(player, index);
    }

    public void UpdateHP(Player player, int index)
    {
        playerUI[index].hpText.text = $"{player.playerName}\nHP : {player.hp}";
    }
}