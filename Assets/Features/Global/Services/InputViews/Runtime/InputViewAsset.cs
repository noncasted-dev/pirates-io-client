using Common.EditableScriptableObjects.Attributes;
using Global.Common;
using Global.Services.Common.Abstract;
using Global.Services.InputViews.ConstraintsStorage;
using Global.Services.InputViews.Logs;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Global.Services.InputViews.Runtime
{
    [CreateAssetMenu(fileName = GlobalAssetsPaths.ServicePrefix + "InputView",
        menuName = GlobalAssetsPaths.InputView + "Service", order = 1)]
    public class InputViewAsset : GlobalServiceAsset
    {
        [SerializeField] [EditableObject] private InputViewLogSettings _logSettings;
        [SerializeField] private InputView _prefab;

        public override void Create(IContainerBuilder builder, IServiceBinder serviceBinder)
        {
            var inputView = Instantiate(_prefab);
            inputView.name = "InputView";

            builder.Register<InputViewLogger>(Lifetime.Scoped)
                .WithParameter(_logSettings);
            builder.RegisterComponent(inputView).AsImplementedInterfaces();
            builder.Register<InputConstraintsStorage>(Lifetime.Singleton)
                .As<IInputConstraintsStorage>();

            serviceBinder.AddToModules(inputView);
            serviceBinder.ListenCallbacks(inputView);
        }
    }
}