using Common.EditableScriptableObjects.Attributes;
using GamePlay.Player.Entity.Components.InertialMovements.Logs;
using GamePlay.Player.Entity.Components.Rotations.Logs;
using GamePlay.Player.Entity.Components.StateMachines.Logs;
using GamePlay.Player.Entity.Setup.Path;
using GamePlay.Player.Entity.States.Floating.Logs;
using GamePlay.Player.Entity.States.Idles.Logs;
using GamePlay.Player.Entity.States.None.Logs;
using GamePlay.Player.Entity.States.RangeAttacks.Logs;
using GamePlay.Player.Entity.States.Respawns.Logs;
using GamePlay.Player.Entity.States.Runs.Logs;
using GamePlay.Player.Entity.Views.Animators.Logs;
using GamePlay.Player.Entity.Views.RigidBodies.Logs;
using GamePlay.Player.Entity.Views.Sprites.Logs;
using GamePlay.Player.Entity.Views.Transforms.Logs;
using GamePlay.Player.Entity.Weapons.Handler.Logs;
using UnityEngine;

namespace GamePlay.Player.Entity.Setup.Editor
{
    [CreateAssetMenu(fileName = PlayerAssetsPaths.ConfigPrefix + "Logs",
        menuName = PlayerAssetsPaths.Config + "Logs")]
    public class PlayerLogsConfig : ScriptableObject
    {
        [Space(100)] [Header("Views")] [SerializeField] 
        private AnimatorLogSettings _animator;
        [SerializeField]  private FloatingStateLogSettings _floatingState;
        [SerializeField]  private IdleLogSettings _idle;
        [SerializeField]  private InertialMovementLogSettings _inertialMovement;
        [SerializeField]  private NoneLogSettings _none;
        [SerializeField]  private RangeAttackLogSettings _rangeAttack;

        [SerializeField]  private RespawnLogSettings _respawn;
        [SerializeField]  private RigidBodyLogSettings _rigidBody;

        [Space(100)] [Header("Components")] [SerializeField] 
        private RotationLogSettings _rotation;
        [SerializeField]  private RunLogSettings _run;

        [SerializeField]  private SpriteViewLogSettings _spriteView;
        [Header("States")] [SerializeField] 
        private StateMachineLogSettings _stateMachine;
        [SerializeField]  private TransformLogSettings _transform;

        [SerializeField]  private WeaponsHandlerLogSettings _weaponsHandler;
    }
}