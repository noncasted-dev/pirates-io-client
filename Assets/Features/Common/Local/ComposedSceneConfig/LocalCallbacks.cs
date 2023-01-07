using System.Collections.Generic;
using Common.DiContainer.Abstract;
using Common.Local.Services.Abstract;
using Common.Local.Services.Abstract.Callbacks;
using Cysharp.Threading.Tasks;
using VContainer;

namespace Common.Local.ComposedSceneConfig
{
    public class LocalCallbacks : ILocalCallbacks
    {
        private readonly List<ILocalAsyncAwakeListener> _asyncAwakes = new();
        private readonly List<ILocalAsyncBootstrappedListener> _asyncBootstraps = new();
        private readonly List<ILocalAwakeListener> _awakes = new();

        private readonly List<ILocalBootstrappedListener> _bootstraps = new();
        private readonly List<ILocalLoadListener> _loads = new();

        private readonly List<ILocalContainerBuildListener> _registers = new();
        private readonly List<IDependencyResolver> _resolvers = new();

        private readonly List<ILocalSwitchListener> _switches = new();

        public IReadOnlyList<ILocalSwitchListener> SwitchCallbacks => _switches;

        public void Listen(object service)
        {
            if (service is ILocalAwakeListener awake)
                _awakes.Add(awake);

            if (service is ILocalAsyncAwakeListener asyncAwake)
                _asyncAwakes.Add(asyncAwake);

            if (service is ILocalSwitchListener switchCallback)
                _switches.Add(switchCallback);

            if (service is ILocalLoadListener load)
                _loads.Add(load);

            if (service is ILocalContainerBuildListener register)
                _registers.Add(register);

            if (service is IDependencyResolver resolver)
                _resolvers.Add(resolver);

            if (service is ILocalBootstrappedListener bootstrap)
                _bootstraps.Add(bootstrap);

            if (service is ILocalAsyncBootstrappedListener asyncBootstrap)
                _asyncBootstraps.Add(asyncBootstrap);
        }

        public void InvokeAwakeCallbacks()
        {
            foreach (var awake in _awakes)
                awake.OnAwake();
        }

        public async UniTask InvokeAsyncAwakeCallbacks()
        {
            var tasks = new UniTask[_asyncAwakes.Count];

            for (var i = 0; i < tasks.Length; i++)
                tasks[i] = _asyncAwakes[i].OnAwakeAsync();

            await UniTask.WhenAll(tasks);
        }

        public void InvokeEnableCallback()
        {
            foreach (var switchListener in _switches)
                switchListener.OnEnabled();
        }

        public void InvokeLoadedCallbacks()
        {
            foreach (var load in _loads)
                load.OnLoaded();
        }

        public void InvokeBootstrapCallbacks()
        {
            foreach (var bootstrap in _bootstraps)
                bootstrap.OnBootstrapped();
        }

        public async UniTask InvokeAsyncBootstrapCallbacks()
        {
            var tasks = new UniTask[_asyncBootstraps.Count];

            for (var i = 0; i < tasks.Length; i++)
                tasks[i] = _asyncBootstraps[i].OnBootstrappedAsync();

            await UniTask.WhenAll(tasks);
        }

        public void Resolve(IObjectResolver objectResolver)
        {
            foreach (var resolver in _resolvers)
                resolver.Resolve(objectResolver);
        }

        public void InvokeRegisterCallbacks(IDependencyRegister builder)
        {
            foreach (var register in _registers)
                register.OnContainerBuild(builder);
        }
    }
}