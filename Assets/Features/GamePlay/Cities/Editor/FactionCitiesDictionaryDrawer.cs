#region

using GamePlay.Cities.Global.Registry.Runtime;
using UnityEditor;

#endregion

namespace GamePlay.Cities.Editor
{
    [CustomPropertyDrawer(typeof(FactionCitiesDictionary))]
    public class FactionCitiesDictionaryDrawer : SerializableDictionaryPropertyDrawer
    {
    }
}