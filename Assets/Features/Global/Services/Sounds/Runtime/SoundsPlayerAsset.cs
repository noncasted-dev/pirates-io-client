using Common.DiContainer.Abstract;
using Cysharp.Threading.Tasks;
using Global.Common;
using Global.Services.Common.Abstract;
using Global.Services.Common.Abstract.Scenes;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Global.Services.Sounds.Runtime
{
    [InlineEditor]
    [CreateAssetMenu(fileName = GlobalAssetsPaths.ServicePrefix + "SoundsPlayer",
        menuName = GlobalAssetsPaths.SoundsPlayer + "Service")]
    public class SoundsPlayerAsset : GlobalServiceAsset
    {
        [SerializeField] private SoundsPlayer _prefab;

        public override async UniTask Create(
            IDependencyRegister builder,
            IGlobalServiceBinder serviceBinder,
            IGlobalSceneLoader sceneLoader,
            IGlobalCallbacks callbacks)
        {
            var soundsPlayer = Instantiate(_prefab);
            soundsPlayer.name = "SoundsPlayer";

            serviceBinder.AddToModules(soundsPlayer);
            callbacks.Listen(soundsPlayer);
        }
    }
}