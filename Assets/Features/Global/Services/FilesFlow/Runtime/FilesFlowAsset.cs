using Common.DiContainer.Abstract;
using Cysharp.Threading.Tasks;
using Global.Common;
using Global.Services.Common.Abstract;
using Global.Services.Common.Abstract.Scenes;
using Global.Services.FilesFlow.Logs;
using Global.Services.FilesFlow.Runtime.Abstract;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Global.Services.FilesFlow.Runtime
{
    [InlineEditor]
    [CreateAssetMenu(fileName = GlobalAssetsPaths.ServicePrefix + "FilesFlow",
        menuName = GlobalAssetsPaths.FilesFlow + "Service")]
    public class FilesFlowAsset : GlobalServiceAsset
    {
        [SerializeField] [Indent] private FilesFlowLogSettings _logSettings;

        [SerializeField] [Indent] private FileDeleter _prefab;

        public override async UniTask Create(
            IDependencyRegister builder,
            IGlobalServiceBinder serviceBinder,
            IGlobalSceneLoader sceneLoader,
            IGlobalCallbacks callbacks)
        {
            var deleter = Instantiate(_prefab);
            deleter.name = "FilesFlow";

            var loader = deleter.GetComponent<FileLoader>();
            var saver = deleter.GetComponent<FileSaver>();

            builder.Register<FilesFlowLogger>()
                .WithParameter(_logSettings);

            builder.Register<FilesDirectory>()
                .As<IDirectoryProvider>();

            builder.RegisterComponent(deleter)
                .As<IFileDeleter>();

            builder.RegisterComponent(loader)
                .As<IFileLoader>();

            builder.RegisterComponent(saver)
                .As<IFileSaver>();

            serviceBinder.AddToModules(deleter);
        }
    }
}