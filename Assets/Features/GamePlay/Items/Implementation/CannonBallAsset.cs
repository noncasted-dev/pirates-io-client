using GamePlay.Common.Paths;
using GamePlay.Items.Abstract;
using UnityEngine;

namespace GamePlay.Items.Implementation
{
    [CreateAssetMenu(fileName = GamePlayAssetsPaths.ItemPrefix + "CannonBall",
        menuName = GamePlayAssetsPaths.Items + "CannonBall")]
    public class CannonBallAsset : ItemAsset
    {
        protected override ItemType Type => ItemType.CannonBall;

        protected override IItem BuildItem(BaseItemData data, int count)
        {
            return new Item(data, count);
        }
    }
}