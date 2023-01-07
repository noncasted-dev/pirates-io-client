using GamePlay.Common.Paths;
using GamePlay.Items.Abstract;
using UnityEngine;

namespace GamePlay.Items.Implementation
{
    [CreateAssetMenu(fileName = GamePlayAssetsPaths.ItemPrefix + "Knuppel",
        menuName = GamePlayAssetsPaths.Items + "Knuppel")]
    public class CannonKnuppelAsset : ItemAsset
    {
        public override ItemType Type => ItemType.CannonKnuppel;

        protected override IItem BuildItem(BaseItemData data, int count)
        {
            return new Item(data, count);
        }
    }
}