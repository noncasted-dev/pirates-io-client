using Common.Structs;
using GamePlay.Services.PlayerSpawn.RemoteBuilders.Runtime;
using Ragon.Client;
using UnityEngine;

namespace GamePlay.Player.Entity.Weapons.Cannon.Root
{
    public class BotShooter : RagonBehaviour, IBotCannon
    {
        private const float _shootDelay = 8f;
        
        private float _timer;
        private bool _isDisabled = false;   
        
        private ICanon _cannon;
        private bool _isBot;

        public override void OnCreatedEntity()
        {
            if (Entity.IsMine == true)
                _isBot = true;
        }

        public void Inject(ICanon cannon)
        {
            _cannon = cannon;
        }

        public void Disable()
        {
            _isDisabled = true;
        }

        public void Enable()
        {
            _isDisabled = false;
        }
        
        private void Update()
        {
            if (_isBot == false)
                return;
            
            if (_isDisabled == true)
                return;
            
            if (_cannon == null)
                return;

            _timer += Time.deltaTime;
            
            if (_timer < _shootDelay)
                return;

            _timer = 0f;

            foreach (var remote in RemotePlayerBuilder.Instance.Remotes)
            {
                if (remote == null)
                    continue;
                
                var distance = Vector2.Distance(remote.position, transform.position);
                
                if (distance > 20f)
                    continue;

                Vector2 direction = (remote.position - transform.position).normalized;
                var angle = direction.ToAngle();
                _cannon.Shoot(angle, 45f, 5);
                return;
            }
        }
    }
}