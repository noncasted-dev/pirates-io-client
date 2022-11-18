using Common.EditableScriptableObjects.Attributes;
using Global.Common;
using Global.Services.Common.Abstract;
using Global.Services.FilesFlow.Logs;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Global.Services.FilesFlow.Runtime
{
    [CreateAssetMenu(fileName = GlobalAssetsPaths.ServicePrefix + "FilesFlow",
        menuName = GlobalAssetsPaths.FilesFlow + "Service", order = 1)]
    public class FilesFlowAsset : GlobalServiceAsset
    {
        [SerializeField] [EditableObject] private FilesFlowLogSettings _logSettings;

        [SerializeField] private FileDeleter _prefab;

        public override void Create(IContainerBuilder builder, IServiceBinder serviceBinder)
        {
            var deleter = Instantiate(_prefab);
            deleter.name = "FilesFlow";

            var loader = deleter.GetComponent<FileLoader>();
            var saver = deleter.GetComponent<FileSaver>();

            builder.Register<FilesFlowLogger>(Lifetime.Scoped)
                .WithParameter(_logSettings);
            builder.Register<FilesDirectory>(Lifetime.Scoped)
                .AsImplementedInterfaces();

            builder.RegisterComponent(deleter).AsImplementedInterfaces();
            builder.RegisterComponent(loader).AsImplementedInterfaces();
            builder.RegisterComponent(saver).AsImplementedInterfaces();

            serviceBinder.AddToModules(deleter);

            serviceBinder.ListenCallbacks(deleter);
            serviceBinder.ListenCallbacks(loader);
            serviceBinder.ListenCallbacks(saver);
        }
    }
}