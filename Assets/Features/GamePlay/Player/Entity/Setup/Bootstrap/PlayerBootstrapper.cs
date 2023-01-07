using System.Linq;
using Cysharp.Threading.Tasks;
using GamePlay.Player.Entity.Setup.Abstract;
using GamePlay.Player.Entity.Setup.Flow;
using UnityEngine;
using VContainer;
using VContainer.Unity;
using ContainerBuilder = Common.DiContainer.Runtime.ContainerBuilder;

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
            var attachedBuilders = GetComponents<IPlayerContainerBuilder>().ToList();

            foreach (var attachedBuilder in attachedBuilders)
                rootBuilders.Remove(attachedBuilder);

            _builders = rootBuilders.Concat(attachedBuilders).ToArray();

            var scope = GetComponent<PlayerScope>();

            var builder = new ContainerBuilder();

            using (LifetimeScope.EnqueueParent(parent))
            {
                using (LifetimeScope.Enqueue(OnConfiguration))
                {
                    await UniTask.Create(async () => scope.Build());
                }
            }

            void OnConfiguration(IContainerBuilder container)
            {
                var root = GetComponent<IPlayerRoot>();

                container.RegisterComponent(root);

                foreach (var target in _builders)
                    target.OnBuild(builder);

                builder.RegisterAll(container);
            }

            var flowHandler = new FlowHandler();

            builder.ResolveAllWithCallbacks(scope.Container, flowHandler);

            var root = scope.Container.Resolve<IPlayerRoot>();

            await root.OnBootstrapped(flowHandler, scope);
        }
    }
}