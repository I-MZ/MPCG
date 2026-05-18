using UnityEngine;

public class CardEffectManager : MonoBehaviour
{
    public static CardEffectManager Instance;

    void Awake()
    {
        Instance = this;
    }

    public void UseCardEffect(CardData card, Player user, Player target)
    {
        switch (card.effectType)
        {
            case EffectType.Attack:

                target.TakeDamage(card.value);

                Debug.Log(card.cardName + " ‚ÅUŒ‚");
                break;

            case EffectType.Heal:

                user.hp += card.value;

                Debug.Log(user.playerName + " ‚ª‰ñ•œ");
                break;

            case EffectType.Draw:

                for (int i = 0; i < card.value; i++)
                {
                    user.DrawCard();
                }

                Debug.Log(user.playerName + " ‚ªƒhƒ[");
                break;
        }
    }
}