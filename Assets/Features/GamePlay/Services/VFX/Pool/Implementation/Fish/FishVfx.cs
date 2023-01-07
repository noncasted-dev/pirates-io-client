using System;
using Common.ObjectsPools.Runtime.Abstract;
using Cysharp.Threading.Tasks;
using GamePlay.Items.Abstract;
using GamePlay.Services.DroppedObjects.Implementation.Items.Runtime;
using GamePlay.Services.Projectiles.Implementation.Linear.Runtime;
using UnityEngine;

namespace GamePlay.Services.VFX.Pool.Implementation.Fish
{
    [DisallowMultipleComponent]
    public class FishVfx : MonoBehaviour, IPoolObject<FishVfx>, IDroppedItem
    {
        [SerializeField] private float _destroyDelay;
        [SerializeField] private ParticleSystem _fishSystem;
        [SerializeField] private ParticleSystem _barrelSystem;
        [SerializeField] private Collider2D _projectileCollider;
        [SerializeField] private Collider2D _collectCollider;

        [SerializeField] private GameObject _fish;
        [SerializeField] private GameObject _barrel;

        private IItem _item;

        private Action<FishVfx> _returnToPool;

        private void OnTriggerEnter2D(Collider2D col)
        {
            if (col.TryGetComponent(out LinearProjectile projectile) == false)
                return;

            projectile.OnFishCollected();

            _projectileCollider.enabled = false;
            _collectCollider.enabled = true;
            _fish.SetActive(false);
            _barrel.SetActive(true);

            _barrelSystem.Play();
        }

        public void Drop(
            int id,
            Action<IDroppedItem> collectedCallback,
            IItem item,
            Vector2 origin,
            Vector2 target)
        {
            _item = item;
        }

        public int Id { get; }
        public IItem Item => _item;

        public void Collect()
        {
            _returnToPool?.Invoke(this);
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

        public void SetupPoolObject(Action<FishVfx> returnToPool)
        {
            _returnToPool = returnToPool;
        }

        public void OnTaken()
        {
            _projectileCollider.enabled = true;
            _collectCollider.enabled = false;
            _fish.SetActive(true);
            _barrel.SetActive(false);

            _fishSystem.Play();
            _barrelSystem.Stop();

            ReturnOnDelay().Forget();
        }

        public void OnReturned()
        {
            _fishSystem.Stop();
        }

        private async UniTaskVoid ReturnOnDelay()
        {
            var destroyDelay = (int)(_destroyDelay * 1000);
            var token = this.GetCancellationTokenOnDestroy();

            await UniTask.Delay(destroyDelay, false, PlayerLoopTiming.Update, token);
            _returnToPool?.Invoke(this);
        }
    }
}