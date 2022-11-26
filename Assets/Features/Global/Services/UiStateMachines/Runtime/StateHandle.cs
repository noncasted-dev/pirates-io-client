using System;
using System.Collections.Generic;
using UnityEngine;

namespace Global.Services.UiStateMachines.Runtime
{
    public class StateHandle
    {
        public StateHandle(
            IUiState state,
            StateHandle head,
            Action<IUiState> exitedCallback,
            Action<StateHandle> recoveredCallback)
        {
            State = state;
            _head = head;
            _exitedCallback = exitedCallback;
            _recoveredCallback = recoveredCallback;
        }

        public readonly IUiState State;
        private readonly StateHandle _head;

        private readonly Action<IUiState> _exitedCallback;
        private readonly Action<StateHandle> _recoveredCallback;

        private readonly List<StateHandle> _stack = new();
        private readonly List<StateHandle> _removeQueue = new();

        public string Name => State.Name;

        public void AddToStack(StateHandle handle)
        {
            _stack.Add(handle);
        }

        public void Recover()
        {
            if (_stack.Count != 0)
                foreach (var stateHandle in _stack)
                    stateHandle.Recover();

            State.Recover();
            _recoveredCallback?.Invoke(this);
        }

        public void Exit(bool withDispose)
        {
            if (_stack.Count != 0)
            {
                foreach (var stateHandle in _stack)
                {
                    stateHandle.Exit(withDispose);
                }
            }

            if (withDispose == true)
                _head?.RemoveFromStack(this);

            State.Exit();
            _exitedCallback?.Invoke(State);
        }

        private void RemoveFromStack(StateHandle handle)
        {
            _stack.Remove(handle);
        }
    }
}