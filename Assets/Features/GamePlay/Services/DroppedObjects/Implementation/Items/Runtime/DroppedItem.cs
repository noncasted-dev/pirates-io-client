using System;
using Common.ObjectsPools.Runtime.Abstract;
using Cysharp.Threading.Tasks;
using GamePlay.Items.Abstract;
using NaughtyAttributes;
using UnityEngine;

namespace GamePlay.Services.DroppedObjects.Implementation.Items.Runtime
{
    [DisallowMultipleComponent]
    public class DroppedItem :
        MonoBehaviour,
        IPoolObject<DroppedItem>,
        IDroppedItem
    {
        private static readonly int Play = Animator.StringToHash("Play");

        [SerializeField] [ReadOnly] private string _name;
        [SerializeField] [ReadOnly] private int _count;

        [SerializeField] private float _collectImmunityTime = 5f;

        [SerializeField] private Animator _animator;
        [SerializeField] private Collider2D _collider;
        private Action<IDroppedItem> _collectedCallback;

        private int _id;
        private IItem _item;

        private Action<DroppedItem> _returnToPool;
        public int Id => _id;
        public IItem Item => _item;

        public void Construct(int id, Action<IDroppedItem> collectedCallback, IItem item)
        {
            _item = item;
            _collectedCallback = collectedCallback;
            _id = id;

            _name = item.BaseData.Name;
            _count = item.Count;
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

            ActivateCollider().Forget();
        }

        public void OnReturned()
        {
            _collider.enabled = false;
        }

        private async UniTaskVoid ActivateCollider()
        {
            var delay = (int)(_collectImmunityTime * 1000f);
            await UniTask.Delay(delay, false, PlayerLoopTiming.Update, this.GetCancellationTokenOnDestroy());

            _collider.enabled = true;
        }
    }
}