using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public Player player1;
    public Player player2;

    private Player currentPlayer;

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        StartGame();
    }

    void StartGame()
    {
        currentPlayer = player1;

        StartTurn();
    }

    void StartTurn()
    {
        Debug.Log(currentPlayer.playerName + " のターン開始");

        currentPlayer.DrawCard();
    }

    public void EndTurn()
    {
        Debug.Log(currentPlayer.playerName + " のターン終了");

        if (currentPlayer == player1)
        {
            currentPlayer = player2;
        }
        else
        {
            currentPlayer = player1;
        }

        StartTurn();
    }

    public bool IsCurrentPlayer(Player player)
    {
        return currentPlayer == player;
    }
}