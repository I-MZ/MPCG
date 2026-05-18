using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Šî–{î•ñ")]
    public string playerName;

    [Header("HP")]
    public int hp = 20;

    [Header("ŠƒfƒbƒL")]
    public List<CardData> deck = new List<CardData>();

    [Header("èD")]
    public List<CardData> hand = new List<CardData>();

    //ƒ_ƒ[ƒW‚ğó‚¯‚é
    public void TakeDamage(int damage)
    {
        hp -= damage;

        Debug.Log(playerName + " HP : " + hp);

        //”s–k
        if (hp <= 0)
        {
            Debug.Log(playerName + " ‚Í”s–k‚µ‚Ü‚µ‚½");
        }
    }

    //‰ñ•œ
    public void Heal(int value)
    {
        hp += value;
        Debug.Log(playerName + " ‚ª " + value + " ‰ñ•œ");
    }
}