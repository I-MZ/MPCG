using TMPro;
using UnityEngine;

public class BattleUIManager : MonoBehaviour
{
    public static BattleUIManager Instance;

    [System.Serializable]
    public class BattleArea
    {
        public TMP_Text hpText;

        public Transform handArea;

        public Transform minionArea;
    }

    [Header("Ž©•ªUI")]
    public BattleArea selfUI;

    [Header("‘ŠŽèUI")]
    public BattleArea enemyUI;

    [Header("“GHPˆê——")]
    public EnemyHPUI[] enemyHPUI;

    private void Awake()
    {
        Instance = this;
    }
}

//using TMPro;
//using UnityEngine;
//public class BattleUIManager : MonoBehaviour
//{
//    public static BattleUIManager Instance;

//    private int currentViewIndex = 1;

//    //public EnemyHPUI[] enemyHPUI;

//    [System.Serializable]
//    public class PlayerUI
//    {
//        public GameObject root;

//        public TMP_Text hpText;

//        public Transform handArea;

//        public Transform minionArea;
//    }

//    public PlayerUI[] playerUI = new PlayerUI[8];

//    private void Awake()
//    {
//        Instance = this;
//    }

//    public void BindPlayer(Player player, int index)
//    {
//        if (player == null)
//            return;

//        if (index >= playerUI.Length)
//            return;

//        player.handArea = playerUI[index].handArea;

//        player.minionArea = playerUI[index].minionArea;

//        player.playerName = $"Player {index + 1}";

//        UpdateHP(player, index);
//    }

//    public void UpdateHP(Player player, int index)
//    {
//        playerUI[index].hpText.text = $"{player.playerName}\nHP : {player.hp}";
//    }

//    public void ShowOpponent(int index)
//    {
//        for (int i = 0; i < playerUI.Length; i++)
//        {
//            if (playerUI[i].root == null)
//                continue;

//            // Ž©•ª‚Í•\Ž¦
//            if (i == 0)
//            {
//                playerUI[i].root.SetActive(true);
//            }
//            else
//            {
//                playerUI[i].root.SetActive(i == index);
//            }
//        }

//        currentViewIndex = index;
//    }

//    private void Start()
//    {
//        ShowOpponent(1);
//    }
//}