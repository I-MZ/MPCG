using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BattleUIManager : MonoBehaviour
{
    public static BattleUIManager Instance;

    private Player selfPlayer;
    private Player viewingEnemy;

    [System.Serializable]
    public class BattleArea
    {
        public TMP_Text hpText;
        public Transform handArea;
        public Transform minionArea;
    }

    [Header("自分UI")]
    public BattleArea selfUI;

    [Header("相手UI")]
    public BattleArea enemyUI;

    [Header("敵HP一覧")]
    public EnemyHPUI[] enemyHPUI;

    private void Awake()
    {
        Instance = this;
    }

    public void InitializeUI(Player self)
    {
        selfPlayer = self;

        self.handArea = selfUI.handArea;
        self.minionArea = selfUI.minionArea;

        selfUI.hpText.text =
            $"{self.playerName}\nHP : {self.hp}";
    }

    public void CreateEnemyList(List<Player> players)
    {
        int uiIndex = 0;

        foreach (Player player in players)
        {
            if (player == selfPlayer)
                continue;

            enemyHPUI[uiIndex].gameObject.SetActive(true);

            enemyHPUI[uiIndex].Setup(player);

            int capture = uiIndex;

            enemyHPUI[uiIndex].button.onClick.RemoveAllListeners();

            enemyHPUI[uiIndex].button.onClick.AddListener(() =>
            {
                ShowEnemy(enemyHPUI[capture].player);
            });

            uiIndex++;
        }

        for (; uiIndex < enemyHPUI.Length; uiIndex++)
        {
            enemyHPUI[uiIndex].gameObject.SetActive(false);
        }

        if (players.Count > 1)
        {
            foreach (Player player in players)
            {
                if (player != selfPlayer)
                {
                    ShowEnemy(player);
                    break;
                }
            }
        }
    }

    public void ShowEnemy(Player enemy)
    {
        viewingEnemy = enemy;

        RefreshEnemyHand();

        foreach (EnemyHPUI ui in enemyHPUI)
        {
            if (!ui.gameObject.activeSelf)
                continue;

            ui.SetSelected(ui.player == enemy);
        }
    }

    public void RefreshSelfHP()
    {
        if (selfPlayer == null)
            return;

        selfUI.hpText.text =
            $"{selfPlayer.playerName}\nHP : {selfPlayer.hp}";
    }

    public void RefreshEnemyList()
    {
        foreach (EnemyHPUI ui in enemyHPUI)
        {
            if (!ui.gameObject.activeSelf)
                continue;

            ui.Refresh();
        }
    }

    public void RefreshSelfHand()
    {
        if (selfPlayer == null)
            return;

        foreach (Transform child in selfUI.handArea)
            Destroy(child.gameObject);

        foreach (CardData card in selfPlayer.hand)
        {
            GameObject obj =
                Instantiate(
                    DeckManager.Instance.cardPrefab,
                    selfUI.handArea);

            CardUI ui = obj.GetComponent<CardUI>();

            ui.Setup(card, selfPlayer);
        }
    }

    public void RefreshEnemyHand()
    {
        if (viewingEnemy == null)
            return;

        foreach (Transform child in enemyUI.handArea)
            Destroy(child.gameObject);

        foreach (CardData card in viewingEnemy.hand)
        {
            GameObject obj =
                Instantiate(
                    DeckManager.Instance.cardPrefab,
                    enemyUI.handArea);

            CardUI ui = obj.GetComponent<CardUI>();

            ui.Setup(card, viewingEnemy);
        }
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

//            // 自分は表示
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


//public void ShowEnemy(Player enemy)
//{
//    // 前に表示していた相手を元へ戻す
//    if (viewingEnemy != null)
//    {
//        MoveChildren(enemyUI.handArea, viewingEnemy.handArea);
//        MoveChildren(enemyUI.minionArea, viewingEnemy.minionArea);
//    }

//    // 今見る相手を保存
//    viewingEnemy = enemy;

//    //enemy.handArea = enemyUI.handArea;
//    //enemy.minionArea = enemyUI.minionArea;

//    //新しい相手をEnemyUIへ表示
//    MoveChildren(enemy.handArea, enemyUI.handArea);
//    MoveChildren(enemy.minionArea, enemyUI.minionArea);

//    //マーク更新
//    foreach (EnemyHPUI ui in enemyHPUI)
//    {
//        if (!ui.gameObject.activeSelf)
//            continue;

//        ui.SetSelected(ui.player == enemy);
//    }
//}

//カードUIを移動する関数
//private void MoveChildren(Transform from, Transform to)
//{
//    while (from.childCount > 0)
//    {
//        from.GetChild(0).SetParent(to, false);
//    }
//}