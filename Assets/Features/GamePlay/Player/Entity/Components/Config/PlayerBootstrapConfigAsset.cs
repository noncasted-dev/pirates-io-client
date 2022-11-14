using Common.EditableScriptableObjects.Attributes;
using Features.GamePlay.Player.Entity.Components.DamageProcessors.Runtime;
using GamePlay.Player.Entity.Components.Abstract;
using GamePlay.Player.Entity.Components.Healths.Runtime;
using GamePlay.Player.Entity.Components.InertialMovements.Runtime;
using GamePlay.Player.Entity.Components.Rotations.Runtime;
using GamePlay.Player.Entity.Components.StateMachines.Runtime;
using GamePlay.Player.Entity.Setup.Path;
using GamePlay.Player.Entity.States.Deaths.Runtime;
using GamePlay.Player.Entity.States.Floating.Runtime;
using GamePlay.Player.Entity.States.Idles.Runtime;
using GamePlay.Player.Entity.States.None.Runtime;
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
        [Header("Components")] [SerializeField] [EditableObject]
        private RotationAsset _rotation;
        [SerializeField] [EditableObject] private StateMachineAsset _stateMachine;
        [SerializeField] [EditableObject] private InertialMovementAsset _inertialMovement;

        [Space(50)] [Header("States")] [SerializeField] [EditableObject]
        private FloatingStateAsset _floating;
        [SerializeField] [EditableObject] private IdleAsset _idle;
        [SerializeField] [EditableObject] private NoneAsset _none;
        [SerializeField] [EditableObject] private RespawnAsset _respawn;
        [SerializeField] [EditableObject] private RunAsset _run;
        [SerializeField] [EditableObject] private RangeAttackAsset _range;
        [SerializeField] [EditableObject] private WeaponsHandlerAsset _weaponsHandler;
        [SerializeField] [EditableObject] private DeathAsset _death;
        [SerializeField] [EditableObject] private HealthAsset _health;
        [SerializeField] [EditableObject] private DamageProcessorAsset _damageProcessor;

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
                _damageProcessor
            };
        }
    }
}