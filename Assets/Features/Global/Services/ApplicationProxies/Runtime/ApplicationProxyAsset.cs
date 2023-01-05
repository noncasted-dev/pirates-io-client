using Common.DiContainer.Abstract;
using Cysharp.Threading.Tasks;
using Global.Common;
using Global.Services.ApplicationProxies.Logs;
using Global.Services.Common.Abstract;
using Global.Services.Common.Abstract.Scenes;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Global.Services.ApplicationProxies.Runtime
{
    [InlineEditor]
    [CreateAssetMenu(fileName = GlobalAssetsPaths.ServicePrefix + "ApplicationProxy",
        menuName = GlobalAssetsPaths.ApplicationProxy + "Service")]
    public class ApplicationProxyAsset : GlobalServiceAsset
    {
        [SerializeField] [Indent] private ApplicationProxyLogSettings _logSettings;
        [SerializeField] [Indent] private ApplicationProxy _prefab;

        public override async UniTask Create(
            IDependencyRegister builder,
            IGlobalServiceBinder serviceBinder,
            IGlobalSceneLoader sceneLoader,
            IGlobalCallbacks callbacks)
        {
            var applicationProxy = Instantiate(_prefab);
            applicationProxy.name = "ApplicationProxy";

            builder.Register<ApplicationProxyLogger>()
                .WithParameter(_logSettings);

            builder.RegisterComponent(applicationProxy)
                .As<IApplicationFlow>();
        }
    }
}