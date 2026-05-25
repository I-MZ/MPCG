//そのプレイヤー自身の情報を管理するコード

using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("基本情報")]
    public string playerName;

    [Header("HP")]
    public int hp = 20;

    [Header("所持デッキ")]
    public List<CardData> deck = new List<CardData>();

    [Header("手札")]
    public List<CardData> hand = new List<CardData>();

    [Header("手札UI")]
    public Transform handArea;

    //ダメージを受ける
    public void TakeDamage(int damage)
    {
        hp -= damage;

        Debug.Log(playerName + " HP : " + hp);

        //敗北
        if (hp <= 0)
        {
            Debug.Log(playerName + " は敗北しました");
        }
    }

    //回復
    public void Heal(int value)
    {
        hp += value;
        Debug.Log(playerName + " が " + value + " 回復");
    }
}