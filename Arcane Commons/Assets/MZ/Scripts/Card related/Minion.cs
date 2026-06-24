//場の使い魔のデータを扱うコード

using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Minion : MonoBehaviour, IDamageable
{
    [Header("元データ")]
    public MinionData data;

    [Header("現在HP")]
    public int currentHp;

    [HideInInspector]
    public Player owner;

    [Header("UI")]
    public TMP_Text nameText;
    public TMP_Text attackText;
    public TMP_Text hpText;

    [Header("攻撃状態")]
    public bool canAttack = false;
    public bool hasAttacked = false;

    // 将来イラストを表示する用
    //public Image artworkImage;

    //初期化
    public void Setup(
        MinionData minionData,
        Player ownerPlayer)
    {
        data = minionData;

        owner = ownerPlayer;

        currentHp = data.hp;

        canAttack = false;

        hasAttacked = false;

        //疾走
        if (HasAbility(AbilityTrigger.OnPlay, AbilityEffect.Rush))
        {
            canAttack = true;
        }

        // UI更新
        nameText.text = data.minionName;

        attackText.text = data.attack.ToString();

        hpText.text = currentHp.ToString();

        // 将来画像を使う時
        // artworkImage.sprite = data.artwork;

        Debug.Log(data.minionName + " を召喚");
    }

    //能力を持っているか
    public bool HasAbility(AbilityTrigger trigger,AbilityEffect effect)
    {
        Debug.Log(data.minionName + " の能力チェック開始");
        Debug.Log(data.minionName + " abilities数 = " + data.abilities.Count);

        foreach (AbilityData ability in data.abilities)
        {
            Debug.Log("所持能力 : " + ability.trigger + " / " + ability.effect);

            if (ability.trigger == trigger &&ability.effect == effect)
            {
                Debug.Log("能力一致 : " + trigger + " / " + effect);

                return true;
            }
        }

        Debug.Log("能力なし : " + trigger + " / " + effect);

        return false;
    }

    //クリック
    public void OnClickMinion()
    {
        //使い魔攻撃開始
        if (!TurnManager.Instance.isSelectingTarget &&
            !TurnManager.Instance.isSelectingMinionTarget)
        {
            if (!canAttack)
            {
                Debug.Log("この使い魔はまだ攻撃できません");
                return;
            }

            if (hasAttacked)
            {
                Debug.Log("この使い魔は既に攻撃しました");
                return;
            }

            TurnManager.Instance.isSelectingMinionTarget = true;

            TurnManager.Instance.selectedMinion = this;

            Debug.Log(data.minionName + " の攻撃対象を選んでください");

            return;
        }

        //使い魔の攻撃対象選択中
        if (TurnManager.Instance.isSelectingMinionTarget)
        {
            Debug.Log("攻撃対象選択処理に入った");

            //同じ使い魔を押したらキャンセル
            if (TurnManager.Instance.selectedMinion == this)
            {
                TurnManager.Instance.isSelectingMinionTarget = false;
                TurnManager.Instance.selectedMinion = null;

                Debug.Log("使い魔の攻撃をキャンセルしました");

                return;
            }

            //自分の使い魔は攻撃できない
            if (owner == TurnManager.Instance.selectedMinion.owner)
            {
                Debug.Log("自分の使い魔は攻撃できません");

                return;
            }

            Minion attacker = TurnManager.Instance.selectedMinion;

            //相手に守護がいるか
            bool hasGuard = false;

            foreach (Minion minion in owner.minions)
            {
                if (minion.HasAbility(AbilityTrigger.OnPlay,AbilityEffect.Guard))
                {
                    hasGuard = true;

                    break;
                }
            }

            Debug.Log("hasGuard = " + hasGuard);

            //守護がいるのに守護以外を攻撃
            //if (hasGuard && !HasAbility(AbilityTrigger.OnPlay, AbilityEffect.Guard))
            if (hasGuard && !this.HasAbility(AbilityTrigger.OnPlay, AbilityEffect.Guard))
            {
                Debug.Log("守護がいるためこの使い魔は攻撃できません");

                return;
            }

            //相手にダメージ
            TakeDamage(attacker.data.attack);

            //反撃ダメージ
            attacker.TakeDamage(data.attack);

            //吸血
            Debug.Log("吸血チェック開始");

            if (TurnManager.Instance.selectedMinion.HasAbility(AbilityTrigger.OnAttack,AbilityEffect.Lifesteal))
            {
                Debug.Log("吸血発動");

                TurnManager.Instance.selectedMinion.owner.Heal
                (
                    TurnManager.Instance.selectedMinion.data.attack
                );
            }
            else
            {
                Debug.Log("吸血なし");
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

        //自分の使い魔は選べない
        //(今は敵対象のみ、将来 TargetType 実装時に置き換える)
        if (owner == TurnManager.Instance.selectedUser)
        {
            Debug.Log("自分の使い魔は選べません");

            return;
        }

        Debug.Log(data.minionName + " が選択された");

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

        //対象選択終了
        TurnManager.Instance.isSelectingTarget = false;
        TurnManager.Instance.selectedCard      = null;
        TurnManager.Instance.selectedUser      = null;
        TurnManager.Instance.selectedCardUI    = null;
    }

    //ダメージを受ける
    public void TakeDamage(int damage)
    {
        currentHp -= damage;

        //HP表示更新
        hpText.text = currentHp.ToString();

        Debug.Log(data.minionName + " HP : " + currentHp);

        if (currentHp <= 0)
        {
            Die();
        }
    }

    //死亡
    void Die()
    {
        Debug.Log(data.minionName + " は倒れた");

        //プレイヤーの場から削除
        owner.minions.Remove(this);

        Destroy(gameObject);
    }
}