using Global.Common;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Local.ComposedSceneConfig
{
    [CreateAssetMenu(fileName = GlobalAssetsPaths.ConfigPrefix + "ComposedScene",
        menuName = GlobalAssetsPaths.Config + "ComposedScene")]
    public class ComposedScenesConfig : ScriptableObject
    {
        [SerializeField] private AssetReference _servicesScene;

        public AssetReference ServicesScene => _servicesScene;
    }
}