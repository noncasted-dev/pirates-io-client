using Common.EditableScriptableObjects.Attributes;
using Global.Common;
using Global.Services.Common.Abstract;
using Global.Services.UiStateMachines.Logs;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Global.Services.UiStateMachines.Runtime
{
    [CreateAssetMenu(fileName = GlobalAssetsPaths.ServicePrefix + "UiStateMachine",
        menuName = GlobalAssetsPaths.UiStateMachine + "Service")]
    public class UiStateMachineAsset : GlobalServiceAsset
    {
        [SerializeField] [EditableObject] private UiStateMachineLogSettings _logSettings;
        [SerializeField] private UiStateMachine _prefab;

        public override void Create(IContainerBuilder builder, IServiceBinder serviceBinder)
        {
            var stateMachine = Instantiate(_prefab);
            stateMachine.name = "UiStateMachine";

            builder.Register<UiStateMachineLogger>(Lifetime.Scoped)
                .WithParameter(_logSettings);
            
            builder.RegisterComponent(stateMachine)
                .As<IUiStateMachine>();

            serviceBinder.AddToModules(stateMachine);
        }
    }
}