using GamePlay.Common.Paths;
using GamePlay.Items.Abstract;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace GamePlay.Player.Entity.Components.Definition
{
    [CreateAssetMenu(fileName = GamePlayAssetsPaths.ItemPrefix + "ShipConfig",
        menuName = GamePlayAssetsPaths.Items + "Ship")]
    public class TradableShipConfig : ItemAsset
    {
        [SerializeField] private int _maxTeam;
        [SerializeField] private int _maxWeight;
        [SerializeField] private int _maxCannons;
        [SerializeField] private int _maxHealth;
        [SerializeField] private int _price;
        [SerializeField] private int _maxSpeed;
        [SerializeField] private int _requiredReputation;

        [SerializeField] private ItemType _type;

        [SerializeField] private AssetReference _local;
        [SerializeField] private AssetReference _remote;
        [SerializeField] private AssetReference _dead;
        
        public override ItemType Type => _type;
        public AssetReference Local => _local;
        public AssetReference Remote => _remote;
        public AssetReference Dead => _dead;

        protected override IItem BuildItem(BaseItemData data, int count)
        {
            return new ShipItem(
                data,
                count,
                _maxTeam,
                _maxWeight,
                _maxCannons,
                _maxHealth,
                _maxSpeed,
                _price,
                _requiredReputation);
        }
    }
}