using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Navigation;

public class KeyPressByUser 
{
    Dictionary<eDirection, bool> PairDirectionKeys;
    public eDirection DirectionChosen { get; set; }

    public KeyPressByUser(Dictionary<eDirection, bool> pairDirectionKeys)
    {
        PairDirectionKeys = pairDirectionKeys;
        DirectionChosen = GetDirectionChosen();
    }

    private eDirection GetDirectionChosen()
    {
        foreach (KeyValuePair<eDirection, bool> item in PairDirectionKeys)
        {
            if (item.Value)
            {
                return item.Key;
            }
        }

        return eDirection.NONE;
    }   
}
