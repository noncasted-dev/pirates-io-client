using Common.DiContainer.Abstract;
using Cysharp.Threading.Tasks;
using GamePlay.Common.SceneObjects.Logs;
using Global.Common;
using Global.Services.Common.Abstract;
using Global.Services.Common.Abstract.Scenes;
using Sirenix.OdinInspector;
using UnityEngine;

namespace GamePlay.Common.SceneObjects.Global
{
    [InlineEditor]
    [CreateAssetMenu(fileName = GlobalAssetsPaths.ServicePrefix + "SceneObjects",
        menuName = GlobalAssetsPaths.SceneObjects + "Service")]
    public class SceneObjectsAsset : GlobalServiceAsset
    {
        [SerializeField] private SceneObjectLogSettings _logSettings;

        public override async UniTask Create(
            IDependencyRegister builder,
            IGlobalServiceBinder serviceBinder,
            IGlobalSceneLoader sceneLoader,
            IGlobalCallbacks callbacks)
        {
            builder.Register<SceneObjectLogger>()
                .WithParameter(_logSettings);
        }
    }
}