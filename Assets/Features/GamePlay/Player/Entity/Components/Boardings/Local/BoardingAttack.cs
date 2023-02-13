using GamePlay.Player.Entity.Components.InertialMovements.Runtime;
using GamePlay.Player.Entity.Setup.Flow.Callbacks;
using GamePlay.Player.Entity.Views.RigidBodies.Runtime;
using GamePlay.Services.Network.RemoteEntities.Entity;
using Global.Services.Updaters.Runtime.Abstract;

namespace GamePlay.Player.Entity.Components.Boardings.Local
{
    public class BoardingAttack : IUpdatable, IPlayerSwitchListener
    {
        public BoardingAttack(
            IBoardingTargetSearcher targetSearcher,
            ISpeedCalculator speedCalculator,
            IUpdater updater,
            IRigidBody rigidBody)
        {
            _targetSearcher = targetSearcher;
            _speedCalculator = speedCalculator;
            _updater = updater;
            _rigidBody = rigidBody;
        }

        private const float _tickTime = 1f;

        private readonly IBoardingTargetSearcher _targetSearcher;
        private readonly ISpeedCalculator _speedCalculator;
        private readonly IUpdater _updater;
        private readonly IRigidBody _rigidBody;

        private float _tickTimer;
        private IRemotePlayer _target;
        
        public void OnEnabled()
        {
            _updater.Add(this);
        }

        public void OnDisabled()
        {
            _updater.Remove(this);
        }

        public void OnUpdate(float delta)
        {
            _tickTimer -= delta;

            if (_tickTimer > 0f)
                return;

            _tickTimer = _tickTime;

            if (_targetSearcher.Search(_rigidBody.Position, out _target) == false)
                return;
            
            
        }
    }
}