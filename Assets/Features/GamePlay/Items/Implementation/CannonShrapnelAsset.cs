using GamePlay.Common.Paths;
using GamePlay.Items.Abstract;
using UnityEngine;

namespace GamePlay.Items.Implementation
{
    [CreateAssetMenu(fileName = GamePlayAssetsPaths.ItemPrefix + "Shrapnel",
        menuName = GamePlayAssetsPaths.Items + "Shrapnel")]
    public class CannonShrapnelAsset : ItemAsset
    {
        public override ItemType Type => ItemType.CannonShrapnel;
    
        protected override IItem BuildItem(BaseItemData data, int count)
        {
            return new Item(data, count);
        }
    }
}