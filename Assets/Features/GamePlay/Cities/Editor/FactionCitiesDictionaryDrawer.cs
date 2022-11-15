using GamePlay.Cities.Global.Registry.Runtime;
using UnityEditor;

namespace GamePlay.Cities.Editor
{
    [CustomPropertyDrawer(typeof(FactionCitiesDictionary))]
    public class FactionCitiesDictionaryDrawer : SerializableDictionaryPropertyDrawer
    {
    }
}