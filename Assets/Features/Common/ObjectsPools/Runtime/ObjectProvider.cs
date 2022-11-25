using Common.ObjectsPools.Logs;
using Common.ObjectsPools.Runtime.Abstract;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Common.ObjectsPools.Runtime
{
    public class ObjectProvider<T> : IObjectsPool, IObjectReturner<T>, IObjectProvider<T> where T : IPoolObject<T>
    {
        public ObjectProvider(
            ObjectsPoolLogger logger,
            IObjectFactory<T> factory,
            int startupInstances,
            Transform parent)
        {
            _logger = logger;
            _factory = factory;
            _startupInstances = startupInstances;
            _parent = parent;
        }

        private readonly IObjectFactory<T> _factory;

        private readonly ObjectsPoolLogger _logger;
        private readonly Transform _parent;

        private readonly ObjectsRegistry<T> _registry = new();
        private readonly int _startupInstances;

        public T Get(Vector2 position)
        {
            T poolObject;

            if (_registry.ContainsInactive() == true)
            {
                poolObject = _registry.GetInactive();
            }
            else
            {
                poolObject = _factory.Create(Vector2.zero);
                poolObject.SetupPoolObject(Return);
                MoveToParent(poolObject);
                _registry.OnActiveCreated(poolObject);
            }

            poolObject.SetPosition(position);
            poolObject.OnTaken();

            return poolObject;
        }

        public void Return(T poolObject)
        {
            poolObject.GameObject.SetActive(false);
            MoveToParent(poolObject);
            _registry.OnReturned(poolObject);
        }

        public IObjectProvider<T1> GetProvider<T1>()
        {
            return this as IObjectProvider<T1>;
        }

        public async UniTask PreloadAsync()
        {
            await _factory.PreloadAsync();
        }

        public void InstantiateStartupInstances()
        {
            for (var i = 0; i < _startupInstances; i++)
            {
                var poolObject = _factory.Create(Vector2.zero);
                poolObject.SetupPoolObject(Return);
                MoveToParent(poolObject);
                poolObject.GameObject.SetActive(false);
                _registry.OnInactiveCreated(poolObject);
            }
        }

        public void Unload()
        {
            _registry.DestroyAll();

            _factory.Unload();
        }

        private void MoveToParent(T poolObject)
        {
            poolObject.GameObject.transform.parent = _parent;
        }
    }
}