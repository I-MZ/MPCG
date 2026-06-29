using System.Collections.Generic;
using UnityEngine.Rendering;

[System.Serializable]
public class DeckSaveData
{
    //デッキ名
    public string deckName = "新しいデッキ";

    //クラス
    public ClassType classType;

    //カード一覧
    public List<CardData> cards = new List<CardData>();
}