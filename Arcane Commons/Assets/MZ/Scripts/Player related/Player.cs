//そのプレイヤー自身の情報を管理するコード

using TMPro;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour, IDamageable
{
    [Header("基本情報")]
    public string playerName;

    [Header("HP")]
    public int hp = 20;

    [Header("HP表示")]
    public TMP_Text hpText;

    [Header("所持デッキ")]
    public List<CardData> deck = new List<CardData>();

    [Header("手札")]
    public List<CardData> hand = new List<CardData>();

    [Header("使い魔UI")]
    public Transform minionArea;

    [Header("場の使い魔")]
    public List<Minion> minions = new List<Minion>();

    [Header("手札UI")]
    public Transform handArea;

    //ダメージを受ける
    public void TakeDamage(int damage)
    {
        hp -= damage;

        Debug.Log(playerName + " HP : " + hp);

        UpdateHPUI();



        //敗北
        if (hp <= 0)
        {
            Debug.Log(playerName + " は敗北しました");

            TurnManager.Instance.GameOver(this);
        }
    }

    //回復
    public void Heal(int value)
    {
        hp += value;
        Debug.Log(playerName + " が " + value + " 回復");
        UpdateHPUI();
    }

    //HP表示更新
    public void UpdateHPUI()
    {
        hpText.text =playerName + " HP : " + hp;
    }

    //HP表示がクリックされた
    public void OnClickPlayer()
    {
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
        if (TurnManager.Instance.selectedCard.cardType== CardType.Weapon)
        {
            foreach (Minion minion in minions)
            {
                if (minion.HasAbility( AbilityTrigger.OnPlay,AbilityEffect.Guard))
                {
                    Debug.Log( "守護がいるためプレイヤーを攻撃できません");

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

        Debug.Log("捨て札枚数 : " +DeckManager.Instance.discardPile.Count);

        Debug.Log(TurnManager.Instance.selectedCard.cardName +" を捨て札へ送った");

        //対象選択終了
        TurnManager.Instance.isSelectingTarget = false;

        TurnManager.Instance.selectedCard = null;

        TurnManager.Instance.selectedUser = null;

        TurnManager.Instance.selectedCardUI = null;
    }

    private void Start()
    {
        UpdateHPUI();

        if (DeckDataManager.Instance != null)
        {
            deck =
                new List<CardData>
                (
                    DeckDataManager.Instance.savedDeck
                );

            Debug.Log
            (
                playerName + " デッキ読込 : " + deck.Count
            );
        }
    }
}