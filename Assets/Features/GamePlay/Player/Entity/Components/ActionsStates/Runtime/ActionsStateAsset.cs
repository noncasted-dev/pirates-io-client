#region

using GamePlay.Player.Entity.Components.Abstract;
using GamePlay.Player.Entity.Setup.Path;
using UnityEngine;
using VContainer;

#endregion

namespace GamePlay.Player.Entity.Components.ActionsStates.Runtime
{
    [CreateAssetMenu(fileName = PlayerAssetsPaths.ComponentPrefix + "ActionsState",
        menuName = PlayerAssetsPaths.ActionsState + "Component")]
    public class ActionsStateAsset : PlayerComponentAsset
    {
        public override void Register(IContainerBuilder builder)
        {
            builder.Register<ActionsState>(Lifetime.Scoped)
                .As<IActionsStatePresenter>()
                .As<IActionsStateProvider>();
        }
    }
}