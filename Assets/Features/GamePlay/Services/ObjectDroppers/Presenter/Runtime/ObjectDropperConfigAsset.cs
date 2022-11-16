#region

using GamePlay.Common.Paths;
using UnityEngine;
using UnityEngine.AddressableAssets;

#endregion

namespace GamePlay.Services.ObjectDroppers.Presenter.Runtime
{
    [CreateAssetMenu(fileName = GamePlayAssetsPaths.ConfigPrefix + "ObjectDropped",
        menuName = GamePlayAssetsPaths.ObjectsDropper + "Config")]
    public class ObjectDropperConfigAsset : ScriptableObject
    {
        [SerializeField] private AssetReference _droppedItemPrefab;

        public AssetReference DroppedItemPrefab => _droppedItemPrefab;
    }
}