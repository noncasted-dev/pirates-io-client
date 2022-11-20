using GamePlay.Cities.Instance.Storage.Runtime;
using UnityEditor;

namespace GamePlay.Cities.Editor
{
    [CustomPropertyDrawer(typeof(TradableItemDictionary))]
    public class TradableItemDictionaryDrawer : SerializableDictionaryPropertyDrawer
    {
    }
}