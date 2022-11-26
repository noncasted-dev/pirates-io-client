using Common.EditableScriptableObjects.Attributes;
using GamePlay.Player.Entity.Components.InertialMovements.Runtime;
using GamePlay.Player.Entity.Components.Rotations.Runtime;
using GamePlay.Player.Entity.Components.StateMachines.Runtime;
using GamePlay.Player.Entity.Setup.Path;
using GamePlay.Player.Entity.Weapons.Handler.Runtime;
using UnityEngine;

namespace GamePlay.Player.Entity.Setup.Editor
{
    [CreateAssetMenu(fileName = PlayerAssetsPaths.ConfigPrefix + "Components",
        menuName = PlayerAssetsPaths.Config + "Components")]
    public class PlayerComponentsConfig : ScriptableObject
    {
        [SerializeField]  private InertialMovementAsset _inertialMovement;
        [SerializeField]  private RotationAsset _rotation;
        [SerializeField]  private StateMachineAsset _stateMachine;
        [SerializeField]  private WeaponsHandlerAsset _weaponsHandler;
    }
}