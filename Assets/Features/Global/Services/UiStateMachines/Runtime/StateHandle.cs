using System;
using System.Collections.Generic;

namespace Global.Services.UiStateMachines.Runtime
{
    public class StateHandle
    {
        public StateHandle(
            IUiState state,
            Action<IUiState> exitedCallback,
            Action<StateHandle> recoveredCallback)
        {
            State = state;
            _exitedCallback = exitedCallback;
            _recoveredCallback = recoveredCallback;
        }
        
        public readonly IUiState State;
        
        private readonly Action<IUiState> _exitedCallback;
        private readonly Action<StateHandle> _recoveredCallback;

        private readonly Stack<StateHandle> _stack = new();

        public string Name => State.Name;

        public void AddToStack(StateHandle handle)
        {
            _stack.Push(handle);
        }

        public void Recover()
        {
            if (_stack.Count != 0)
            {
                foreach (var stateHandle in _stack)
                    stateHandle.Recover();
            }
            
            State.Recover();
            _recoveredCallback?.Invoke(this);
        }

        public void Exit()
        {
            if (_stack.Count != 0)
            {
                foreach (var stateHandle in _stack)
                    stateHandle.Exit();
            }
            
            State.Exit();
            _exitedCallback?.Invoke(State);
        }
    }
}