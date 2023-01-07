using System.Collections.Generic;
using GamePlay.Common.Damages;
using GamePlay.Common.Paths;
using GamePlay.Services.Projectiles.Entity;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace GamePlay.Services.Projectiles.Replicator.Runtime
{
    [CreateAssetMenu(fileName = GamePlayAssetsPaths.ConfigPrefix + "ProjectilesReplicator",
        menuName = GamePlayAssetsPaths.ProjectilesReplicator + "Config")]
    public class ProjectileReplicatorConfigAsset : ScriptableObject
    {
        [SerializeField] private AssetReference _fire;
        [SerializeField] private ProjectileTypeDictionary _projectiles;
        [SerializeField] [Min(0f)] private float _replicateDistance = 40f;

        public float ReplicateDistance => _replicateDistance;
        public AssetReference Fire => _fire;
        public IReadOnlyDictionary<ProjectileType, AssetReference> Projectiles => _projectiles;
    }
}