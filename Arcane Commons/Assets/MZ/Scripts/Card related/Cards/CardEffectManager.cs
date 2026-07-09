//カードを見て処理を分岐するコード

using UnityEngine;

public class CardEffectManager : MonoBehaviour
{
    public static CardEffectManager Instance;

    void Awake()
    {
        Instance = this;
    }

    //public void UseCardEffect(CardData card, Player user, Player target)
    public void UseCardEffect(CardData card, Player user, IDamageable target)
    {
        switch (card.effectType)
        {
            case EffectType.Damage:

                target.TakeDamage(card.value);

                Debug.Log(card.cardName + " で攻撃");
                break;

            case EffectType.Heal:

                user.Heal(card.value);

                Debug.Log(user.playerName + " が回復");

                break;

            case EffectType.Draw:

                for (int i = 0; i < card.value; i++)
                {
                    DeckManager.Instance.DrawCard(user);
                }

                Debug.Log(user.playerName + " がドロー");
                break;

            case EffectType.Summon:　//召喚

                MinionManager.Instance.SummonMinion(user, card.summonMinion);

                break;
        }
    }
}