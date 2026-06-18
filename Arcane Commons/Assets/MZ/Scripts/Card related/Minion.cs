//場の使い魔のデータを扱うコード

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

        // UI更新
        nameText.text = data.minionName;

        attackText.text = data.attack.ToString();

        hpText.text = currentHp.ToString();

        // 将来画像を使う時
        // artworkImage.sprite = data.artwork;

        Debug.Log(data.minionName + " を召喚");
    }

    //クリック
    public void OnClickMinion()
    {
        //対象選択中じゃない
        if (!TurnManager.Instance.isSelectingTarget)
        {
            return;
        }

        Debug.Log(data.minionName + " が選択された");

        //ここは後で Player.OnClickPlayer() と
        //共通化しながら実装予定
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

//なんかあった時のために残す

//using UnityEngine;
//using TMPro;

//public class Minion : MonoBehaviour, IDamageable
//{
//    [Header("元データ")]
//    public MinionData data;

//    [Header("現在HP")]
//    public int currentHp;

//    [HideInInspector]
//    public Player owner;

//    //初期化
//    public void Setup(MinionData minionData, Player ownerPlayer)
//    {
//        data = minionData;

//        owner = ownerPlayer;

//        currentHp = data.hp;

//        Debug.Log(data.minionName + "を召喚");
//    }

//    public void OnClickMinion()
//    {
//        // 対象選択中じゃない
//        if (!TurnManager.Instance.isSelectingTarget)
//        {
//            return;
//        }

//        Debug.Log(data.minionName + " が選択された");
//    }

//    //ダメージを受ける
//    public void TakeDamage(int damage)
//    {
//        currentHp -= damage;

//        Debug.Log(data.minionName + " HP : " + currentHp);

//        if (currentHp <= 0)
//        {
//            Die();
//        }
//    }

//    //死亡
//    void Die()
//    {
//        Debug.Log(data.minionName + " は倒れた");

//        // プレイヤーの場から削除
//        owner.minions.Remove(this);

//        Destroy(gameObject);
//    }
//}