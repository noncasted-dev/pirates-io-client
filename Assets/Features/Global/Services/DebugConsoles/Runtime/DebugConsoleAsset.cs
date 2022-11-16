using Global.Common;
using Global.Services.Common.Abstract;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Global.Services.DebugConsoles.Runtime
{
    [CreateAssetMenu(fileName = GlobalAssetsPaths.ServicePrefix + "DebugConsole",
        menuName = GlobalAssetsPaths.DebugConsole + "Service")]
    public class DebugConsoleAsset : GlobalServiceAsset
    {
        [SerializeField] private DebugConsole _prefab;

        public override void Create(IContainerBuilder builder, IServiceBinder serviceBinder)
        {
            var debugConsole = Instantiate(_prefab);
            debugConsole.name = "DebugConsole";

            builder.RegisterComponent(debugConsole);

            serviceBinder.AddToModules(debugConsole);
            serviceBinder.ListenCallbacks(debugConsole);
        }
    }
}