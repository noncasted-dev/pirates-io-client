using Common.DiContainer.Abstract;
using Cysharp.Threading.Tasks;
using Global.Common;
using Global.Services.Common.Abstract;
using Global.Services.Common.Abstract.Scenes;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Global.Services.DebugConsoles.Runtime
{
    [InlineEditor]
    [CreateAssetMenu(fileName = GlobalAssetsPaths.ServicePrefix + "DebugConsole",
        menuName = GlobalAssetsPaths.DebugConsole + "Service")]
    public class DebugConsoleAsset : GlobalServiceAsset
    {
        [SerializeField] [Indent] private DebugConsole _prefab;

        public override async UniTask Create(
            IDependencyRegister builder,
            IGlobalServiceBinder serviceBinder,
            IGlobalSceneLoader sceneLoader,
            IGlobalCallbacks callbacks)
        {
            var debugConsole = Instantiate(_prefab);
            debugConsole.name = "DebugConsole";

            builder.RegisterComponent(debugConsole)
                .AsCallbackListener();

            serviceBinder.AddToModules(debugConsole);
        }
    }
}