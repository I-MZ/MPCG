//場の使い魔のデータを扱うコード

using UnityEngine;

public class Minion : MonoBehaviour
{
    [Header("元データ")]
    public MinionData data;

    [Header("現在HP")]
    public int currentHp;

    //初期化
    public void Setup(MinionData minionData)
    {
        data = minionData;

        currentHp = data.hp;

        Debug.Log(data.minionName + "を召喚");
    }
}