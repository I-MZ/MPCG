using UnityEngine;

public class BattleInitializer : MonoBehaviour
{
    private bool initialized = false;

    private void Update()
    {
        if (initialized)
            return;

        // BattlePlayerManagerが存在しない
        if (BattlePlayerManager.Instance == null)
            return;

        // ArcaneNetworkManagerが存在しない
        if (ArcaneNetworkManager.Instance == null)
            return;

        // まだ全員揃っていない
        if (BattlePlayerManager.Instance.players.Count !=
            ArcaneNetworkManager.Instance.players.Count)
        {
            return;
        }

        Debug.Log("全プレイヤー登録完了");

        // 全員デッキ読込
        foreach (Player player in BattlePlayerManager.Instance.players)
        {
            player.LoadDeck();
        }

        DeckManager.Instance.InitializeDeck();

        TurnManager.Instance.InitializeBattle();

        initialized = true;

        Debug.Log("Battle初期化完了");
    }
}