using UnityEngine;

public class TestSummon : MonoBehaviour
{
    public Player targetPlayer;

    public MinionData summonData;

    public void Test()
    {
        Debug.Log("Instance = " + MinionManager.Instance);
        Debug.Log("targetPlayer = " + targetPlayer);
        Debug.Log("summonData = " + summonData);

        MinionManager.Instance.SummonMinion(
            targetPlayer,
            summonData
        );
    }
}