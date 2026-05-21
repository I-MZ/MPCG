//ターン進行を管理するコード

using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;

public class TurnManager : MonoBehaviour
{
    public static TurnManager Instance;

    [Header("カード使用制限")]
    public bool canUseCard = true;

    [Header("プレイヤー")]
    public Player player;

    [Header("敵")]
    public Player enemyPlayer;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        StartTurn();
    }

    //ターン開始
    public void StartTurn()
    {
        Debug.Log("プレイヤーターン開始");

        canUseCard = true;

        DeckManager.Instance.DrawCard(player);
    }

    //ターン終了
    public void EndTurn()
    {
        Debug.Log("プレイヤーターン終了");

        EnemyTurn();
    }

    //敵ターン(動作確認用・多分NPC用になる)
    void EnemyTurn()
    {
        Debug.Log("敵ターン");

        DeckManager.Instance.DrawCard(enemyPlayer);

        StartTurn();
    }
}