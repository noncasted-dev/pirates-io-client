using GamePlay.Common.Paths;
using UnityEngine;

namespace GamePlay.Services.Projectiles.Mover
{
    [CreateAssetMenu(fileName = GamePlayAssetsPaths.ConfigPrefix + "ProjectilesMover",
        menuName = GamePlayAssetsPaths.Projectiles + "Config")]
    public class ProjectilesMoverConfigAsset : ScriptableObject
    {
        [SerializeField] private LayerMask _hitBoxMask;
        [SerializeField] [Min(0)] private int _bufferSize;

        public LayerMask HitBoxMask => _hitBoxMask;
        public int BufferSize => _bufferSize;
    }
}