using Common.DiContainer.Abstract;
using GamePlay.Player.Entity.Components.Abstract;
using GamePlay.Player.Entity.Setup.Path;
using UnityEngine;

namespace Features.GamePlay.Player.Entity.States.PathFollowing.Runtime
{
    [CreateAssetMenu(fileName = PlayerAssetsPaths.ComponentPrefix + "PathFollower",
        menuName = PlayerAssetsPaths.PathFollow + "Component")]
    public class PathFollowerAsset : PlayerComponentAsset
    {
        [SerializeField] private PathFollowDefinition _definition;
        
        public override void Register(IDependencyRegister builder)
        {
            builder.Register<PathFollower>()
                .WithParameter(_definition)
                .AsCallbackListener();
        }
    }
}