using Global.Services.Updaters.Logs;
using Global.Services.Updaters.Runtime.Abstract;
using UnityEngine;
using VContainer;

namespace Global.Services.Updaters.Runtime
{
    public class Updater : MonoBehaviour, IUpdater, IUpdateSpeedModifier, IUpdateSpeedSetter
    {
        [Inject]
        private void Construct(UpdaterLogger logger)
        {
            _logger = logger;
        }

        private readonly UpdatablesHandler<IFixedUpdatable> _fixedUpdatables = new();
        private readonly UpdatablesHandler<IPostFixedUpdatable> _postFixedUpdatables = new();
        private readonly UpdatablesHandler<IPreFixedUpdatable> _preFixedUpdatables = new();

        private readonly UpdatablesHandler<IPreUpdatable> _preUpdatables = new();
        private readonly UpdatablesHandler<IUpdateSpeedModifiable> _speedModifiables = new();
        private readonly UpdatablesHandler<IUpdatable> _updatables = new();

        private UpdaterLogger _logger;

        private float _speed = 1f;

        private void Update()
        {
            var delta = Time.deltaTime * _speed;

            _preUpdatables.Fetch();
            _updatables.Fetch();

            foreach (var updatable in _preUpdatables.List)
                updatable.OnPreUpdate(delta);

            _logger.OnPreUpdateCalled(_updatables.Count);

            foreach (var updatable in _updatables.List)
                updatable.OnUpdate(delta);

            _logger.OnUpdateCalled(_updatables.Count);
        }

        private void FixedUpdate()
        {
            var delta = Time.fixedDeltaTime * _speed;

            _preFixedUpdatables.Fetch();
            _fixedUpdatables.Fetch();
            _postFixedUpdatables.Fetch();

            foreach (var updatable in _preFixedUpdatables.List)
                updatable.OnPreFixedUpdate(delta);

            _logger.OnPreFixedUpdateCalled(_preFixedUpdatables.Count);

            foreach (var updatable in _fixedUpdatables.List)
                updatable.OnFixedUpdate(delta);

            _logger.OnFixedUpdateCalled(_updatables.Count);

            foreach (var updatable in _postFixedUpdatables.List)
                updatable.OnPostFixedUpdate(delta);

            _logger.OnPostFixedUpdateCalled(_updatables.Count);
        }

        public void Add(IPreUpdatable updatable)
        {
            _preUpdatables.Add(updatable);

            _logger.OnPreUpdatableAdded(_preUpdatables.Count);
        }

        public void Remove(IPreUpdatable updatable)
        {
            _preUpdatables.Remove(updatable);

            _logger.OnPreUpdatableRemoved(_preUpdatables.Count);
        }

        public void Add(IUpdatable updatable)
        {
            _updatables.Add(updatable);

            _logger.OnUpdatableAdded(_updatables.Count);
        }

        public void Remove(IUpdatable updatable)
        {
            _updatables.Remove(updatable);

            _logger.OnUpdatableRemoved(_updatables.Count);
        }

        public void Add(IFixedUpdatable updatable)
        {
            _fixedUpdatables.Add(updatable);

            _logger.OnFixedUpdatableAdded(_fixedUpdatables.Count);
        }

        public void Remove(IFixedUpdatable updatable)
        {
            _fixedUpdatables.Remove(updatable);

            _logger.OnFixedUpdatableRemoved(_fixedUpdatables.Count);
        }

        public void Add(IPostFixedUpdatable updatable)
        {
            _postFixedUpdatables.Add(updatable);

            _logger.OnPostFixedUpdatableAdded(_postFixedUpdatables.Count);
        }

        public void Remove(IPostFixedUpdatable updatable)
        {
            _postFixedUpdatables.Remove(updatable);

            _logger.OnPostFixedUpdatableRemoved(_postFixedUpdatables.Count);
        }

        public void Add(IPreFixedUpdatable updatable)
        {
            _preFixedUpdatables.Add(updatable);

            _logger.OnPreFixedUpdatableAdded(_preFixedUpdatables.Count);
        }

        public void Remove(IPreFixedUpdatable updatable)
        {
            _preFixedUpdatables.Remove(updatable);

            _logger.OnPreFixedUpdatableRemoved(_preFixedUpdatables.Count);
        }

        public float Speed => _speed;

        public void Add(IUpdateSpeedModifiable modifiable)
        {
            _speedModifiables.Add(modifiable);

            _logger.OnSpeedModifiableAdded();
        }

        public void Remove(IUpdateSpeedModifiable modifiable)
        {
            _speedModifiables.Remove(modifiable);

            _logger.OnSpeedModifiableRemoved();
        }

        public void Set(float speed)
        {
            if (speed < 0)
            {
                _logger.OnSpeedModifyError(speed);
                return;
            }

            _speed = speed;

            foreach (var speedModifiable in _speedModifiables.List)
                speedModifiable.OnSpeedModified(speed);

            _logger.OnSpeedModified(speed);
        }
    }
}