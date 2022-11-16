#region

using System;
using Common.ObjectsPools.Runtime.Abstract;
using GamePlay.Items.Abstract;
using NaughtyAttributes;
using UnityEngine;

#endregion

namespace GamePlay.Services.ObjectDroppers.Implementation.Items.Runtime
{
    [DisallowMultipleComponent]
    [RequireComponent(typeof(Animator))]
    public class DroppedItem :
        MonoBehaviour,
        IPoolObject<DroppedItem>,
        IDroppedItem
    {
        private static readonly int Play = Animator.StringToHash("Play");

        [SerializeField] [ReadOnly] private string _name;
        [SerializeField] [ReadOnly] private int _count;

        private Action<DroppedItem> _returnToPool;
        private Action<IDroppedItem> _collectedCallback;

        private int _id;
        private IItem _item;

        private Animator _animator;

        public GameObject GameObject => gameObject;
        public int Id => _id;
        public IItem Item => _item;

        public void SetPosition(Vector2 position)
        {
            transform.position = position;
        }

        public void Construct(int id, Action<IDroppedItem> collectedCallback, IItem item)
        {
            _item = item;
            _collectedCallback = collectedCallback;
            _id = id;

            _name = item.BaseData.Name;
            _count = item.Count;
        }

        public void SetupPoolObject(Action<DroppedItem> returnToPool)
        {
            _returnToPool = returnToPool;
            _animator = GetComponent<Animator>();
        }

        public void OnTaken()
        {
            _animator.SetTrigger(Play);
        }

        public void OnReturned()
        {
        }

        public void Collect()
        {
            _collectedCallback?.Invoke(this);
        }

        public void Destroy()
        {
            _returnToPool?.Invoke(this);
        }
    }
}