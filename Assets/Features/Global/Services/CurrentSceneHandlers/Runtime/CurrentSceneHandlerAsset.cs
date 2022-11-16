#region

using Common.EditableScriptableObjects.Attributes;
using Global.Common;
using Global.Services.Common.Abstract;
using Global.Services.CurrentSceneHandlers.Logs;
using UnityEngine;
using UnityEngine.SceneManagement;
using VContainer;
using VContainer.Unity;

#endregion

namespace Global.Services.CurrentSceneHandlers.Runtime
{
    [CreateAssetMenu(fileName = GlobalAssetsPaths.ServicePrefix + "CurrentSceneHandler",
        menuName = GlobalAssetsPaths.CurrentSceneHandler + "Service",
        order = 1)]
    public class CurrentSceneHandlerAsset : GlobalServiceAsset
    {
        [SerializeField] [EditableObject] private CurrentSceneHandlerLogSettings _logSettings;
        [SerializeField] private CurrentSceneHandler _prefab;

        public override void Create(IContainerBuilder builder, IServiceBinder serviceBinder)
        {
            var sceneHandler = Instantiate(_prefab);
            sceneHandler.name = "CurrentSceneHandler";

            var global = SceneManager.GetActiveScene();

            builder.Register<CurrentSceneHandlerLogger>(Lifetime.Scoped)
                .WithParameter("settings", _logSettings);

            builder.RegisterComponent(sceneHandler)
                .WithParameter("global", global)
                .As<ICurrentSceneHandler>();

            serviceBinder.AddToModules(sceneHandler);
            serviceBinder.ListenCallbacks(sceneHandler);
        }
    }
}