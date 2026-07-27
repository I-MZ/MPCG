//そのプレイヤー自身の情報を管理するコード

using TMPro;
using System.Collections.Generic;
using UnityEngine;
//using Mirror;

public class Player : MonoBehaviour, IDamageable
//public class Player : NetworkBehaviour, IDamageable
{
    [Header("基本情報")]
    //[SyncVar]
    public string playerName;

    [Header("HP")]
    public int hp = 20;

    [Header("所持デッキ")]
    public List<CardData> deck = new List<CardData>();

    [Header("手札UI")]
    public Transform handArea;

    [Header("手札")]
    public List<CardData> hand = new List<CardData>();

    [Header("使い魔UI")]
    public Transform minionArea;

    [Header("場の使い魔")]
    public List<Minion> minions = new List<Minion>();

    //ダメージを受ける
    public void TakeDamage(int damage)//koko
    {
        hp -= damage;

        Debug.Log(playerName + " HP : " + hp);

        BattleUIManager.Instance.RefreshSelfHP();
        BattleUIManager.Instance.RefreshEnemyList();

        //UpdateHPUI();
        //PlayerUI.Instance.UpdateHPUI(this);

        //敗北
        if (hp <= 0)
        {
            Debug.Log(playerName + " は敗北しました");

            TurnManager.Instance.GameOver(this);
        }
    }

    //回復
    public void Heal(int value)//koko
    {
        hp += value;
        Debug.Log(playerName + " が " + value + " 回復");
        //UpdateHPUI();
        //PlayerUI.Instance.UpdateHPUI(this);
    }

    //HP表示がクリックされた
    public void OnClickPlayer()
    {
        //能力対象選択中
        if (TurnManager.Instance.isSelectingAbilityTarget)
        {
            AbilityData ability = TurnManager.Instance.selectedAbility;

            TakeDamage(ability.value);

            Debug.Log(playerName + " に " + ability.value + " ダメージ");

            TurnManager.Instance.abilityUser.DestroyMinion();

            TurnManager.Instance.isSelectingAbilityTarget = false;
            TurnManager.Instance.selectedAbility = null;
            TurnManager.Instance.abilityUser = null;

            return;
        }

        if (TurnManager.Instance.isSelectingAbilityTarget)
        {
            //自分は選べない
            if (this == TurnManager.Instance.abilityUser.owner)
            {
                Debug.Log("自分は選べません");
                return;
            }

            TakeDamage(TurnManager.Instance.selectedAbility.value);

            TurnManager.Instance.isSelectingAbilityTarget = false;
            TurnManager.Instance.abilityUser = null;
            TurnManager.Instance.selectedAbility = null;

            Debug.Log("能力が発動しました");

            return;
        }

        //使い魔の攻撃対象選択中
        if (TurnManager.Instance.isSelectingMinionTarget)
        {
            //自分は攻撃できない
            if (this == TurnManager.Instance.selectedMinion.owner)
            {
                Debug.Log("自分は攻撃できません");

                return;
            }

            //相手に守護がいるか
            foreach (Minion minion in minions)
            {
                if (minion.HasAbility(
                    AbilityTrigger.OnPlay,
                    AbilityEffect.Guard))
                {
                    Debug.Log("守護がいるためプレイヤーを攻撃できません");

                    return;
                }
            }

            TakeDamage(TurnManager.Instance.selectedMinion.data.attack);

            //吸血
            if (TurnManager.Instance.selectedMinion.HasAbility(
                AbilityTrigger.OnAttack,
                AbilityEffect.Lifesteal))
            {
                Debug.Log("吸血発動");

                TurnManager.Instance.selectedMinion.owner.Heal(
                    TurnManager.Instance.selectedMinion.data.attack
                );
            }

            TurnManager.Instance.selectedMinion.hasAttacked = true;

            TurnManager.Instance.isSelectingMinionTarget = false;

            TurnManager.Instance.selectedMinion = null;

            Debug.Log("使い魔が攻撃しました");

            return;
        }

        //対象選択中じゃない
        if (!TurnManager.Instance.isSelectingTarget)
        {
            return;
        }

        //自分は選べない
        if (this == TurnManager.Instance.selectedUser)
        {
            Debug.Log("自分は選べません");

            return;
        }

        //武器攻撃なら守護チェック
        if (TurnManager.Instance.selectedCard.cardType == CardType.Weapon)
        {
            foreach (Minion minion in minions)
            {
                if (minion.HasAbility(AbilityTrigger.OnPlay, AbilityEffect.Guard))
                {
                    Debug.Log("守護がいるためプレイヤーを攻撃できません");

                    return;
                }
            }
        }

        Debug.Log(playerName + " が選択された");

        //カード効果発動
        CardEffectManager.Instance.UseCardEffect
        (
            TurnManager.Instance.selectedCard,
            TurnManager.Instance.selectedUser,
            this
        );

        //手札から削除
        TurnManager.Instance.selectedUser.hand.Remove
        (
            TurnManager.Instance.selectedCard
        );

        //捨て札へ送る
        DeckManager.Instance.discardPile.Add
        (
            TurnManager.Instance.selectedCard
        );

        //UI削除
        Destroy
        (
            TurnManager.Instance.selectedCardUI.gameObject
        );

        Debug.Log("捨て札枚数 : " + DeckManager.Instance.discardPile.Count);

        Debug.Log(TurnManager.Instance.selectedCard.cardName + " を捨て札へ送った");

        //対象選択終了
        TurnManager.Instance.isSelectingTarget = false;

        TurnManager.Instance.selectedCard = null;

        TurnManager.Instance.selectedUser = null;

        TurnManager.Instance.selectedCardUI = null;
    }

    //さらにエラー回避のためいったん即席版に
    //デッキセーブ
    private void Start()
    {
        //if (DeckDataManager.Instance != null &&
        //    DeckDataManager.Instance.savedDecks.Count > 0)
        //{
        //    deck = new List<CardData>(DeckDataManager.Instance.savedDecks[0].cards);

        //    Debug.Log(playerName + " デッキ読込 : " + deck.Count);
        //}
    }

    public void LoadDeck()
    {
        if (DeckDataManager.Instance == null)
            return;

        if (DeckDataManager.Instance.savedDecks.Count == 0)
            return;

        deck = new List<CardData>(
            DeckDataManager.Instance.savedDecks[0].cards
        );

        Debug.Log(playerName + " デッキ読込 : " + deck.Count);
    }
}

    //エラー回避のためいったん即席版に
    //デッキセーブ
    //private void Start()
    //{
    //    UpdateHPUI();

    //    if (DeckDataManager.Instance != null &&DeckDataManager.Instance.savedDecks.Count > 0)
    //    {
    //        deck = new List<CardData>(DeckDataManager.Instance.savedDecks[0].cards);

    //        Debug.Log(playerName + " デッキ読込 : " + deck.Count);
    //    }
    //}

    //一旦コメントアウト
    //デッキセーブ
    //    private void Start()
    //    {
    //        UpdateHPUI();

    //        if (DeckDataManager.Instance != null)
    //        {
    //            deck =
    //                new List<CardData>
    //                (
    //                    DeckDataManager.Instance.savedDeck
    //                );

    //            Debug.Log
    //            (
    //                playerName + " デッキ読込 : " + deck.Count
    //            );
    //        }
    //    }
//}