using Global.Common;
using Global.Services.Common.Abstract;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Global.Services.Loggers.Runtime
{
    [CreateAssetMenu(fileName = GlobalAssetsPaths.ServicePrefix + "Logger",
        menuName = GlobalAssetsPaths.Logger + "Service", order = 1)]
    public class LoggerAsset : GlobalServiceAsset
    {
        [SerializeField] private Logger _prefab;

        public override void Create(IContainerBuilder builder, IServiceBinder serviceBinder)
        {
            var logger = Instantiate(_prefab);
            logger.name = "Logger";

            builder.RegisterComponent(logger).AsImplementedInterfaces();

            serviceBinder.AddToModules(logger);
            serviceBinder.ListenCallbacks(logger);
        }
    }
}