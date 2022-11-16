#region

using GamePlay.Common.Paths;
using UnityEngine;
using UnityEngine.AddressableAssets;

#endregion

namespace GamePlay.Services.DroppedObjects.Presenter.Runtime
{
    [CreateAssetMenu(fileName = GamePlayAssetsPaths.ConfigPrefix + "ObjectDropped",
        menuName = GamePlayAssetsPaths.ObjectsDropper + "Config")]
    public class ObjectDropperConfigAsset : ScriptableObject
    {
        [SerializeField] private AssetReference _droppedItemPrefab;
        [SerializeField] private float _dropFromPlayerYOffset = 2f;

        public AssetReference DroppedItemPrefab => _droppedItemPrefab;
        public float DropFromPlayerYOffset => _dropFromPlayerYOffset;
    }
}