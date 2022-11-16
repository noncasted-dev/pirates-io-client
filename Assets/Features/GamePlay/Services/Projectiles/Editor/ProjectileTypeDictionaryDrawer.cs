using Common.ReadOnlyDictionaries.Editor;
using GamePlay.Services.Projectiles.Entity;
using UnityEditor;

namespace GamePlay.Services.Projectiles.Editor
{
    [CustomPropertyDrawer(typeof(ProjectileTypeDictionary))]
    public class ProjectileTypeDictionaryDrawer : ReadonlyDictionaryPropertyDrawer
    {
        protected override bool IsCollapsed => false;
    }
}