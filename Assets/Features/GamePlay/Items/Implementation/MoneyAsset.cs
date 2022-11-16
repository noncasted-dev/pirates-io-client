#region

using GamePlay.Common.Paths;
using GamePlay.Items.Abstract;
using UnityEngine;

#endregion

namespace GamePlay.Items.Implementation
{
    [CreateAssetMenu(fileName = GamePlayAssetsPaths.ItemPrefix + "Money",
        menuName = GamePlayAssetsPaths.Items + "Money")]
    public class MoneyAsset : ItemAsset
    {
        protected override ItemType Type => ItemType.Money;

        protected override IItem BuildItem(BaseItemData data, int count)
        {
            return new Item(data, count);
        }
    }
}