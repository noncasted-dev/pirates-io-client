using System;
using Common.ObjectsPools.Runtime.Abstract;
using Cysharp.Threading.Tasks;
using GamePlay.Items.Abstract;
using Global.Services.Updaters.Runtime.Abstract;
using NaughtyAttributes;
using UnityEngine;

namespace GamePlay.Services.DroppedObjects.Implementation.Items.Runtime
{
    [DisallowMultipleComponent]
    public class DroppedItem :
        MonoBehaviour,
        IPoolObject<DroppedItem>,
        IDroppedItem,
        IUpdatable
    {
        public void Construct(IUpdater updater)
        {
            _updater = updater;
        }

        private static readonly int Play = Animator.StringToHash("Play");

        [SerializeField] [ReadOnly] private string _name;
        [SerializeField] [ReadOnly] private int _count;

        [SerializeField] [CurveRange(0f, 0f, 1f, 1f)]
        private AnimationCurve _curve;

        [SerializeField] private float _collectImmunityTime = 5f;
        [SerializeField] private float _flyHeight = 1f;
        [SerializeField] private float _flyTime = 1f;

        [SerializeField] private Animator _animator;
        [SerializeField] private Collider2D _collider;

        [SerializeField] private GameObject _flyState;
        [SerializeField] private GameObject _landedState;
        private Action<IDroppedItem> _collectedCallback;

        private float _flyProgress;

        private int _id;
        private IItem _item;
        private Vector2 _origin;

        private Action<DroppedItem> _returnToPool;
        private Vector2 _target;
        private IUpdater _updater;

        public int Id => _id;
        public IItem Item => _item;

        public void Drop(
            int id,
            Action<IDroppedItem> collectedCallback,
            IItem item,
            Vector2 origin,
            Vector2 target)
        {
            _target = target;
            _origin = origin;
            _item = item;
            _collectedCallback = collectedCallback;
            _id = id;

            _name = item.BaseData.Name;
            _count = item.Count;

            _flyProgress = 0f;
        }

        public void Collect()
        {
            _collectedCallback?.Invoke(this);
        }

        public void Destroy()
        {
            _returnToPool?.Invoke(this);
        }

        public GameObject GameObject => gameObject;

        public void SetPosition(Vector2 position)
        {
            transform.position = position;
        }

        public void SetupPoolObject(Action<DroppedItem> returnToPool)
        {
            _returnToPool = returnToPool;

            _collider.enabled = false;
        }

        public void OnTaken()
        {
            _animator.SetTrigger(Play);

            _collider.enabled = false;
            _landedState.SetActive(false);
            _flyState.SetActive(true);

            _updater.Add(this);

            ActivateCollider().Forget();
        }

        public void OnReturned()
        {
            _updater.Remove(this);

            _collider.enabled = false;
        }

        public void OnUpdate(float delta)
        {
            if (_flyProgress > _flyTime)
            {
                _flyState.SetActive(false);
                _landedState.SetActive(true);
                return;
            }

            _flyProgress += delta;

            var progress = _flyProgress / _flyTime;
            var evaluation = _curve.Evaluate(progress);
            var height = evaluation * _flyHeight;

            var position = Vector2.Lerp(_origin, _target, progress);
            position.y += height;

            transform.position = position;
        }

        private async UniTaskVoid ActivateCollider()
        {
            var delay = (int)(_collectImmunityTime * 1000f);
            await UniTask.Delay(delay, false, PlayerLoopTiming.Update, this.GetCancellationTokenOnDestroy());

            _collider.enabled = true;
        }
    }
}