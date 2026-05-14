//ƒvƒŒƒCƒ„[‚Ì‘Ì—ÍŠÖŒW

using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public int hp = 20;

    public void TakeDamage(int damage)
    {
        hp -= damage;

        Debug.Log("Player HP : " + hp);
    }
}