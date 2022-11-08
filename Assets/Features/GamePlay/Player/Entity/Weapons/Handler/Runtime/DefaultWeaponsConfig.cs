using GamePlay.Player.Entity.Setup.Path;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace GamePlay.Player.Entity.Weapons.Handler.Runtime
{
    [CreateAssetMenu(fileName = PlayerAssetsPaths.ConfigPrefix + "DefaultWeapons",
        menuName = PlayerAssetsPaths.WeaponsHandler + "DefaultWeapons")]
    public class DefaultWeaponsConfig : ScriptableObject
    {
        [SerializeField] private AssetReference _range;
        [SerializeField] private AssetReference _melee;

        public AssetReference Range => _range;
        public AssetReference Melee => _melee;
    }
}