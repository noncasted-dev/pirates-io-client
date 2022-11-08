using Local.ComposedSceneConfig;
using Menu.Common;
using Menu.Flow;
using UnityEngine;
using UnityEngine.AddressableAssets;
using VContainer.Unity;

namespace Menu.Config
{
    [CreateAssetMenu(fileName = "Menu", menuName = MenuAssetsPaths.Root + "Scene")]
    public class MenuAsset : ComposedSceneAsset
    {
        [SerializeField] private AssetReference _scene;
        [SerializeField] private MenuScope _scopePrefab;

        protected override AssetReference[] AssignScenes()
        {
            return new[] { _scene };
        }

        protected override LifetimeScope AssignScope()
        {
            return _scopePrefab;
        }
    }
}