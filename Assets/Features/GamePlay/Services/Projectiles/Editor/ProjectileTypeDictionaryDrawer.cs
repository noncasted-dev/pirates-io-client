#region

using Common.ReadOnlyDictionaries.Editor;
using GamePlay.Services.Projectiles.Entity;
using UnityEditor;

#endregion

namespace GamePlay.Services.Projectiles.Editor
{
    [CustomPropertyDrawer(typeof(ProjectileTypeDictionary))]
    public class ProjectileTypeDictionaryDrawer : ReadonlyDictionaryPropertyDrawer
    {
        protected override bool IsCollapsed => false;
    }
}