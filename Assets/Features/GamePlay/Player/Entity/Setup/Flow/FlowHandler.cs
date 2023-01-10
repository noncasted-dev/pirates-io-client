using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using GamePlay.Player.Entity.Setup.Flow.Callbacks;
using ICallbackRegister = Common.DiContainer.Abstract.ICallbackRegister;

namespace GamePlay.Player.Entity.Setup.Flow
{
    public class FlowHandler : IFlowHandler, ICallbackRegister
    {
        private readonly List<IPlayerAsyncAwakeListener> _asyncAwakes = new();
        private readonly List<IPlayerAwakeListener> _awakes = new();
        private readonly List<IPlayerDestroyListener> _destroys = new();
        private readonly List<IPlayerStartListener> _starts = new();
        private readonly List<IPlayerSwitchListener> _switches = new();

        public void Listen(object listener)
        {
            TryAddToList(_awakes, listener);
            TryAddToList(_asyncAwakes, listener);
            TryAddToList(_starts, listener);
            TryAddToList(_switches, listener);
            TryAddToList(_destroys, listener);
        }

        public void InvokeAwake()
        {
            foreach (var callback in _awakes)
                callback.OnAwake();
        }

        public async UniTask InvokeAsyncAwake()
        {
            var awakes = new List<UniTask>();

            foreach (var callback in _asyncAwakes)
                awakes.Add(callback.OnAsyncAwake());

            await UniTask.WhenAll(awakes);
        }

        public void InvokeStart()
        {
            foreach (var callback in _starts)
                callback.OnStart();
        }

        public void InvokeEnable()
        {
            foreach (var callback in _switches)
                callback.OnEnabled();
        }

        public void InvokeDisable()
        {
            foreach (var callback in _switches)
                callback.OnDisabled();
        }

        public void InvokeDestroy()
        {
            foreach (var callback in _destroys)
                callback.OnDestroyed();
        }

        private void TryAddToList<T1, T2>(ICollection<T1> list, T2 component) where T1 : class
        {
            if (component is T1 callback)
                list.Add(callback);
        }
    }
}