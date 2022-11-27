using GamePlay.Common.Paths;
using GamePlay.Items.Abstract;
using UnityEngine;

namespace GamePlay.Items.Implementation
{
    [CreateAssetMenu(fileName = GamePlayAssetsPaths.ItemPrefix + "Fishnet",
        menuName = GamePlayAssetsPaths.Items + "Fishnet")]
    public class CannonFishnetAsset : ItemAsset
    {
        public override ItemType Type => ItemType.CannonFishnet;
    
        protected override IItem BuildItem(BaseItemData data, int count)
        {
            return new Item(data, count);
        }
    }
}