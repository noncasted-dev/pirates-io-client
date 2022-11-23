using GamePlay.Common.Paths;
using GamePlay.Items.Abstract;
using UnityEngine;

namespace GamePlay.Items.Implementation
{
    [CreateAssetMenu(fileName = GamePlayAssetsPaths.ItemPrefix + "Cannon",
        menuName = GamePlayAssetsPaths.Items + "Cannon")]
    public class CannonAsset : ItemAsset
    {
        public override ItemType Type => ItemType.Cannon;

        protected override IItem BuildItem(BaseItemData data, int count)
        {
            return new Item(data, count);
        }
    }
}