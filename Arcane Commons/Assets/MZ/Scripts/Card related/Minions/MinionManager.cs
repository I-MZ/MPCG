//使い魔関係のマネージャー

using UnityEngine;

public class MinionManager : MonoBehaviour
{
    public static MinionManager Instance;

    [Header("使い魔プレハブ")]
    public GameObject minionPrefab;

    private void Awake()
    {
        Instance = this;
    }

    //使い魔召喚
    public void SummonMinion(Player owner,MinionData data)
    {
        GameObject obj =Instantiate(minionPrefab,owner.minionArea);

        Minion minion = obj.GetComponent<Minion>();

        minion.Setup(data, owner);

        owner.minions.Add(minion);

        Debug.Log(owner.playerName + "が" + data.minionName + "を召喚");
    }
}
