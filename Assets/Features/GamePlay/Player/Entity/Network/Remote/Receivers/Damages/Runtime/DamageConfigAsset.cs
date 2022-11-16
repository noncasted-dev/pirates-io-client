#region

using GamePlay.Player.Entity.Setup.Path;
using UnityEngine;
using UnityEngine.AddressableAssets;

#endregion

namespace GamePlay.Player.Entity.Network.Remote.Receivers.Damages.Runtime
{
    [CreateAssetMenu(fileName = PlayerAssetsPaths.ConfigPrefix + "Damage",
        menuName = PlayerAssetsPaths.Damage + "Config")]
    public class DamageConfigAsset : ScriptableObject
    {
        [SerializeField] private float _flashTime;
        [SerializeField] private AssetReference _explosion;

        public float FlashTime => _flashTime;
        public AssetReference Explosion => _explosion;
    }
}