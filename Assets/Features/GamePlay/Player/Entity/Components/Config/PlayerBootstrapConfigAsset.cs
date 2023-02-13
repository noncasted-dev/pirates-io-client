using GamePlay.Player.Entity.Components.Abstract;
using GamePlay.Player.Entity.Components.ActionsStates.Runtime;
using GamePlay.Player.Entity.Components.DamageProcessors.Runtime;
using GamePlay.Player.Entity.Components.Healths.Runtime;
using GamePlay.Player.Entity.Components.InertialMovements.Runtime;
using GamePlay.Player.Entity.Components.Rotations.Runtime;
using GamePlay.Player.Entity.Components.ShipResources.Runtime;
using GamePlay.Player.Entity.Components.StateMachines.Runtime;
using GamePlay.Player.Entity.Setup.Path;
using GamePlay.Player.Entity.States.Deaths.Runtime;
using GamePlay.Player.Entity.States.Floating.Runtime;
using GamePlay.Player.Entity.States.Idles.Runtime;
using GamePlay.Player.Entity.States.None.Runtime;
using GamePlay.Player.Entity.States.PathFollowing.Runtime;
using GamePlay.Player.Entity.States.RangeAttacks.Runtime;
using GamePlay.Player.Entity.States.Respawns.Runtime;
using GamePlay.Player.Entity.States.Runs.Runtime;
using GamePlay.Player.Entity.Weapons.Handler.Runtime;
using UnityEngine;

namespace GamePlay.Player.Entity.Components.Config
{
    [CreateAssetMenu(fileName = PlayerAssetsPaths.ConfigPrefix + "Bootstrap",
        menuName = PlayerAssetsPaths.Config + "Bootstrap")]
    public class PlayerBootstrapConfigAsset : PlayerBootstrapConfig
    {
        [Header("Components")] [SerializeField]
        private RotationAsset _rotation;
        [SerializeField] private StateMachineAsset _stateMachine;
        [SerializeField] private InertialMovementAsset _inertialMovement;

        [Space(50)] [Header("States")] [SerializeField]
        private FloatingStateAsset _floating;
        [SerializeField] private IdleAsset _idle;
        [SerializeField] private NoneAsset _none;
        [SerializeField] private RespawnAsset _respawn;
        [SerializeField] private RunAsset _run;
        [SerializeField] private RangeAttackAsset _range;
        [SerializeField] private WeaponsHandlerAsset _weaponsHandler;
        [SerializeField] private DeathAsset _death;
        [SerializeField] private HealthAsset _health;
        [SerializeField] private DamageProcessorAsset _damageProcessor;
        [SerializeField] private ActionsStateAsset _actionsState;
        [SerializeField] private ShipResourcesAsset _shipResources;
        [SerializeField] private PathFollowerAsset _pathFollower;

        public override PlayerComponentAsset[] GetAssets()
        {
            return new PlayerComponentAsset[]
            {
                _rotation,
                _stateMachine,
                _inertialMovement,
                _floating,
                _idle,
                _none,
                _respawn,
                _run,
                _range,
                _weaponsHandler,
                _death,
                _health,
                _damageProcessor,
                _actionsState,
                _shipResources,
                _pathFollower
            };
        }
    }
}