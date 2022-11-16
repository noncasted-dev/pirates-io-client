#region

using Common.EditableScriptableObjects.Attributes;
using GamePlay.Player.Entity.Components.Abstract;
using GamePlay.Player.Entity.Components.StateMachines.Logs;
using GamePlay.Player.Entity.Setup.Path;
using UnityEngine;
using VContainer;

#endregion

namespace GamePlay.Player.Entity.Components.StateMachines.Runtime
{
    [CreateAssetMenu(fileName = PlayerAssetsPaths.ComponentPrefix + "StateMachine",
        menuName = PlayerAssetsPaths.StateMachine + "Component")]
    public class StateMachineAsset : PlayerComponentAsset
    {
        [SerializeField] [EditableObject] private StateMachineLogSettings _logSettings;

        public override void Register(IContainerBuilder builder)
        {
            builder.Register<StateMachineLogger>(Lifetime.Scoped).WithParameter("settings", _logSettings);
            builder.Register<StateMachine>(Lifetime.Scoped).As<IStateMachine>();
        }
    }
}