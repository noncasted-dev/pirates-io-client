using GamePlay.Cities.Instance.Storage.Runtime;
using UnityEditor;

namespace GamePlay.Cities.Editor
{
    [CustomPropertyDrawer(typeof(GeneratableTradesDictionary))]
    public class GeneratableTradesDictionaryDrawer : SerializableDictionaryPropertyDrawer
    {
    }
}