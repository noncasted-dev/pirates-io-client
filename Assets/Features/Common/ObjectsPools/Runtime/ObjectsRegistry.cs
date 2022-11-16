using System.Collections.Generic;
using Common.ObjectsPools.Runtime.Abstract;
using UnityEngine;

namespace Common.ObjectsPools.Runtime
{
    public class ObjectsRegistry<T> where T : IPoolObject<T>
    {
        private readonly List<T> _active = new();
        private readonly List<T> _inactive = new();

        public bool ContainsInactive()
        {
            return _inactive.Count != 0;
        }

        public void OnActiveCreated(T poolObject)
        {
            _active.Add(poolObject);
        }

        public void OnInactiveCreated(T poolObject)
        {
            _inactive.Add(poolObject);
        }

        public T GetInactive()
        {
            var poolObject = _inactive[0];

            _inactive.RemoveAt(0);
            _active.Add(poolObject);

            poolObject.GameObject.SetActive(true);

            return poolObject;
        }

        public void OnReturned(T poolObject)
        {
            _inactive.Add(poolObject);
            _active.Remove(poolObject);
        }

        public void DestroyAll()
        {
            foreach (var poolObject in _active)
            {
                poolObject.OnReturned();
                Object.Destroy(poolObject.GameObject);
            }

            foreach (var poolObject in _inactive)
            {
                poolObject.OnReturned();
                Object.Destroy(poolObject.GameObject);
            }

            _active.Clear();
            _inactive.Clear();
        }
    }
}