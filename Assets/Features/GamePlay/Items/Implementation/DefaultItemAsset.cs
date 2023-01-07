using GamePlay.Common.Paths;
using GamePlay.Items.Abstract;
using UnityEngine;

namespace GamePlay.Items.Implementation
{
    [CreateAssetMenu(fileName = GamePlayAssetsPaths.ItemPrefix + "Item",
        menuName = GamePlayAssetsPaths.Items + "Item")]
    public class DefaultItemAsset : ItemAsset
    {
        [SerializeField] private ItemType _type;

        public override ItemType Type => _type;

        protected override IItem BuildItem(BaseItemData data, int count)
        {
            return new Item(data, count);
        }
    }
}