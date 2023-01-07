using System;
using Common.ReadOnlyDictionaries.Runtime;
using GamePlay.Common.Damages;
using UnityEngine.AddressableAssets;

namespace GamePlay.Services.Projectiles.Entity
{
    [Serializable]
    public class ProjectileTypeDictionary : ReadOnlyDictionary<ProjectileType, AssetReference>
    {
    }
}