using GamePlay.Common.Paths;
using GamePlay.Items.Abstract;
using UnityEngine;

namespace GamePlay.Items.Implementation
{
    [CreateAssetMenu(fileName = GamePlayAssetsPaths.ItemPrefix + "Team",
        menuName = GamePlayAssetsPaths.Items + "Team")]
    public class TeamAsset : ItemAsset
    {
        protected override ItemType Type => ItemType.Team;

        protected override IItem BuildItem(BaseItemData data, int count)
        {
            return new Item(data, count);
        }
    }
}