using System.Collections.Generic;
using UnityEngine;

namespace Global.Services.Updaters.Runtime
{
    public class UpdatablesHandler<T>
    {
        private readonly List<T> _addQueue = new();
        private readonly List<T> _list = new();

        private readonly HashSet<T> _queue = new();
        private readonly List<T> _removeQueue = new();

        public IReadOnlyList<T> List => _list;
        public int Count => _list.Count;

        public void Add(T updatable)
        {
            _addQueue.Add(updatable);
        }

        public void Remove(T updatable)
        {
            _removeQueue.Add(updatable);
        }

        public void Fetch()
        {
            FetchAdd();
            FetchRemove();
        }

        private void FetchAdd()
        {
            _list.AddRange(_addQueue);
            _addQueue.Clear();
        }

        private void FetchRemove()
        {
            foreach (var removed in _removeQueue)
                _list.Remove(removed);

            _removeQueue.Clear();
        }

        private void ResolveOverlap(T updatable)
        {
            Debug.Log("[Update] Resolve overlap");

            if (_addQueue.Contains(updatable) == true)
                _addQueue.Remove(updatable);

            if (_removeQueue.Contains(updatable) == true)
                _removeQueue.Remove(updatable);

            _queue.Remove(updatable);
        }
    }
}