#region

using Common.EditableScriptableObjects.Attributes;
using GamePlay.Player.Entity.Setup.Path;
using GamePlay.Player.Entity.States.RangeAttacks.Runtime.Config;
using GamePlay.Player.Entity.States.Respawns.Runtime;
using GamePlay.Player.Entity.States.Runs.Runtime;
using UnityEngine;

#endregion

namespace GamePlay.Player.Entity.Setup.Editor
{
    [CreateAssetMenu(fileName = PlayerAssetsPaths.ConfigPrefix + "StatesConfigs",
        menuName = PlayerAssetsPaths.Config + "StatesConfigs")]
    public class PlayerStatesConfigs : ScriptableObject
    {
        [SerializeField] [EditableObject] private RespawnConfigAsset _respawn;
        [SerializeField] [EditableObject] private RunConfigAsset _run;
        [SerializeField] [EditableObject] private RangeAttackConfigAsset _range;
    }
}