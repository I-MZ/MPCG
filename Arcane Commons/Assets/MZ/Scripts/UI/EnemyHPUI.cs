using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHPUI : MonoBehaviour
{
    public TMP_Text hpText;
    public GameObject arrow;
    public Button button;

    [HideInInspector]
    public Player player;

    public void Setup(Player target)
    {
        player = target;

        Refresh();

        arrow.SetActive(false);
    }

    public void Refresh()
    {
        hpText.text =
            $"{player.playerName}\nHP : {player.hp}";
    }

    public void SetSelected(bool value)
    {
        arrow.SetActive(value);
    }
}