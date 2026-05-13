using UnityEngine;

public class TurnManager : MonoBehaviour
{
    public static TurnManager Instance;

    public bool canUseCard = true;

    private int turn = 1;

    void Awake()
    {
        Instance = this;
    }

    public void EndTurn()
    {
        turn++;

        canUseCard = true;

        Debug.Log("ƒ^[ƒ“ : " + turn);
    }
}