using System;
using Common.ReadOnlyDictionaries.Runtime;
using UnityEngine.AddressableAssets;

namespace GamePlay.Services.Projectiles.Entity
{
    [Serializable]
    public class ProjectileTypeDictionary : ReadOnlyDictionary<ProjectileType, AssetReference>
    {
        
    }
}