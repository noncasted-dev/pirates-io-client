using System.Collections.Generic;
using Global.Services.InputViews.ConstraintsStorage;
using Global.Services.UiStateMachines.Logs;
using UnityEngine;
using VContainer;

namespace Global.Services.UiStateMachines.Runtime
{
    public class UiStateMachine : MonoBehaviour, IUiStateMachine
    {
        [Inject]
        private void Construct(
            IInputConstraintsStorage constraintsStorage,
            UiStateMachineLogger logger)
        {
            _logger = logger;
            _constraintsStorage = constraintsStorage;
        }

        private readonly Stack<StateHandle> _stack = new();
        private readonly Dictionary<IUiState, StateHandle> _handles = new();

        private IUiState _head;
        private IInputConstraintsStorage _constraintsStorage;
        private UiStateMachineLogger _logger;

        public void EnterAsSingle(IUiState state)
        {
            if (_stack.Count != 0)
            {
                var current = _stack.Peek();

                _logger.OnExitCurrent(current.Name);
                current.Exit(false);
            }

            var handle = new StateHandle(state, null, OnStackExited, OnStackRecovered);
            _stack.Push(handle);
            _handles.Add(state, handle);

            _head = state;

            _constraintsStorage.Add(state.Constraints.Input);

            _logger.OnEnteredSingle(state.Name);
        }

        public void EnterAsStack(IUiState head, IUiState state)
        {
            var handle = new StateHandle(state, _handles[head], OnStackExited, OnStackRecovered);

            _handles[head].AddToStack(handle);

            _constraintsStorage.Add(state.Constraints.Input);

            _logger.OnEnteredStack(head.Name, state.Name);
        }

        public void EnterAsStack(IUiState state)
        {
            var handle = new StateHandle(state, _handles[_head], OnStackExited, OnStackRecovered);

            _handles[_head].AddToStack(handle);
            _handles[state] = handle;

            _constraintsStorage.Add(state.Constraints.Input);

            _logger.OnEnteredStack(_head.Name, state.Name);
        }

        public void Exit(IUiState state, bool withDispose = true)
        {
            if (_handles.ContainsKey(state) == false)
            {
                _logger.OnNoStateInStackToExit(state.Name);
                return;
            }

            _handles[state].Exit(withDispose);
            _handles.Remove(state);

            _logger.OnExited(state.Name);

            if (state != _head)
                return;

            var head = _stack.Pop();
            _logger.OnExitHead(head.Name);

            _head = null;

            if (_stack.Count == 0)
            {
                _logger.OnNoPreviousStates();
                return;
            }

            var previous = _stack.Peek();
            previous.Recover();
            _head = previous.State;

            _logger.OnReturnedToPrevious(state.Name, previous.Name);
        }

        private void OnStackExited(IUiState state)
        {
            _constraintsStorage.Remove(state.Constraints.Input);
            _logger.OnExitedStack(state.Name);
        }

        private void OnStackRecovered(StateHandle handle)
        {
            _constraintsStorage.Add(handle.State.Constraints.Input);
            _logger.OnRecovered(handle.Name);
        }
    }
}