#region

using Common.EditableScriptableObjects.Attributes;
using GamePlay.Player.Entity.Setup.Path;
using GamePlay.Player.Entity.States.Floating.Runtime;
using GamePlay.Player.Entity.States.Idles.Runtime;
using GamePlay.Player.Entity.States.None.Runtime;
using GamePlay.Player.Entity.States.RangeAttacks.Runtime;
using GamePlay.Player.Entity.States.Respawns.Runtime;
using GamePlay.Player.Entity.States.Runs.Runtime;
using UnityEngine;

#endregion

namespace GamePlay.Player.Entity.Setup.Editor
{
    [CreateAssetMenu(fileName = PlayerAssetsPaths.ConfigPrefix + "StatesAssets",
        menuName = PlayerAssetsPaths.Config + "StatesAssets")]
    public class PlayerStatesAssetsConfig : ScriptableObject
    {
        [SerializeField] [EditableObject] private FloatingStateAsset _floating;
        [SerializeField] [EditableObject] private IdleAsset _idle;
        [SerializeField] [EditableObject] private NoneAsset _none;
        [SerializeField] [EditableObject] private RangeAttackAsset _range;
        [SerializeField] [EditableObject] private RespawnAsset _respawn;
        [SerializeField] [EditableObject] private RunAsset _run;
    }
}