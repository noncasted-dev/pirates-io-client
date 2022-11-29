using System;
using GamePlay.Items.Abstract;

namespace GamePlay.Cities.Instance.Storage.Runtime
{
    [Serializable]
    public class GeneratableTradesDictionary : SerializableDictionary<ItemType, GeneratableTradeConfig>
    {
        
    }
}