using GamePlay.Player.Entity.Setup.Path;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace GamePlay.Player.Entity.Weapons.Bow.Components.Shooter
{
    [CreateAssetMenu(fileName = PlayerAssetsPaths.ConfigPrefix + "BowShooter",
        menuName = PlayerAssetsPaths.BowShooter + "Config")]
    public class ShooterConfigAsset : ScriptableObject
    {
        [SerializeField] [Min(0f)] private int _damage;
        [SerializeField] [Min(0f)] private float _pushForce;
        [SerializeField] [Min(0f)] private float _speed;
        [SerializeField] [Min(0f)] private LayerMask _layerMask;

        [SerializeField] private AssetReference _reference;

        public int Damage => _damage;
        public float PushForce => _pushForce;
        public float Speed => _speed;
        public LayerMask LayerMask => _layerMask;
        public AssetReference Reference => _reference;
    }
}