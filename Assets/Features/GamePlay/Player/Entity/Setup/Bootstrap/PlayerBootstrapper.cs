using System.Linq;
using Cysharp.Threading.Tasks;
using GamePlay.Player.Entity.Setup.Flow;
using GamePlay.Player.Entity.Setup.Root;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace GamePlay.Player.Entity.Setup.Bootstrap
{
    [DisallowMultipleComponent]
    [RequireComponent(typeof(PlayerScope))]
    public class PlayerBootstrapper : MonoBehaviour, IPlayerBootstrapper
    {
        private IPlayerContainerBuilder[] _builders;

        public async UniTask Bootstrap(LifetimeScope parent)
        {
            var rootBuilders = GetComponentsInParent<IPlayerContainerBuilder>().ToList();
            var bodyBuilders = GetComponents<IPlayerContainerBuilder>().ToList();

            foreach (var builder in bodyBuilders)
                rootBuilders.Remove(builder);
            
            _builders = rootBuilders.Concat(bodyBuilders).ToArray();
            
            var scope = GetComponent<PlayerScope>();

            using (LifetimeScope.EnqueueParent(parent))
            {
                using (LifetimeScope.Enqueue(OnConfiguration))
                {
                    await UniTask.Create(async () => scope.Build());
                }
            }

            var flowHandler = new FlowHandler();

            foreach (var containerBuilder in _builders)
                containerBuilder.Resolve(scope.Container, flowHandler);

            var root = GetComponent<IPlayerRoot>();

            await root.OnBootstrapped(flowHandler, scope);
        }

        private void OnConfiguration(IContainerBuilder builder)
        {
            var root = GetComponent<IPlayerRoot>();

            builder.RegisterComponent(root);

            foreach (var containerBuilder in _builders)
                containerBuilder.OnBuild(builder);
        }
    }
}