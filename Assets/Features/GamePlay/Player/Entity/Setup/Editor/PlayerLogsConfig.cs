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
        [Space(100)] [Header("Views")] [SerializeField] [EditableObject]
        private AnimatorLogSettings _animator;
        [SerializeField] [EditableObject] private FloatingStateLogSettings _floatingState;
        [SerializeField] [EditableObject] private IdleLogSettings _idle;
        [SerializeField] [EditableObject] private InertialMovementLogSettings _inertialMovement;
        [SerializeField] [EditableObject] private NoneLogSettings _none;
        [SerializeField] [EditableObject] private RangeAttackLogSettings _rangeAttack;

        [SerializeField] [EditableObject] private RespawnLogSettings _respawn;
        [SerializeField] [EditableObject] private RigidBodyLogSettings _rigidBody;

        [Space(100)] [Header("Components")] [SerializeField] [EditableObject]
        private RotationLogSettings _rotation;
        [SerializeField] [EditableObject] private RunLogSettings _run;

        [SerializeField] [EditableObject] private SpriteViewLogSettings _spriteView;
        [Header("States")] [SerializeField] [EditableObject]
        private StateMachineLogSettings _stateMachine;
        [SerializeField] [EditableObject] private TransformLogSettings _transform;

        [SerializeField] [EditableObject] private WeaponsHandlerLogSettings _weaponsHandler;
    }
}