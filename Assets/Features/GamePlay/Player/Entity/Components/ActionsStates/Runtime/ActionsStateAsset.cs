using Common.DiContainer.Abstract;
using GamePlay.Player.Entity.Components.Abstract;
using GamePlay.Player.Entity.Setup.Path;
using UnityEngine;

namespace GamePlay.Player.Entity.Components.ActionsStates.Runtime
{
    [CreateAssetMenu(fileName = PlayerAssetsPaths.ComponentPrefix + "ActionsState",
        menuName = PlayerAssetsPaths.ActionsState + "Component")]
    public class ActionsStateAsset : PlayerComponentAsset
    {
        public override void Register(IDependencyRegister builder)
        {
            builder.Register<ActionsState>()
                .As<IActionsStatePresenter>()
                .As<IActionsStateProvider>();
        }
    }
}