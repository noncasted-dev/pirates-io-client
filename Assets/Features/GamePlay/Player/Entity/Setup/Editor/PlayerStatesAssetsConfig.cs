using GamePlay.Player.Entity.Setup.Path;
using GamePlay.Player.Entity.States.Floating.Runtime;
using GamePlay.Player.Entity.States.Idles.Runtime;
using GamePlay.Player.Entity.States.None.Runtime;
using GamePlay.Player.Entity.States.RangeAttacks.Runtime;
using GamePlay.Player.Entity.States.Respawns.Runtime;
using GamePlay.Player.Entity.States.Runs.Runtime;
using UnityEngine;

namespace GamePlay.Player.Entity.Setup.Editor
{
    [CreateAssetMenu(fileName = PlayerAssetsPaths.ConfigPrefix + "StatesAssets",
        menuName = PlayerAssetsPaths.Config + "StatesAssets")]
    public class PlayerStatesAssetsConfig : ScriptableObject
    {
        [SerializeField] private FloatingStateAsset _floating;
        [SerializeField] private IdleAsset _idle;
        [SerializeField] private NoneAsset _none;
        [SerializeField] private RangeAttackAsset _range;
        [SerializeField] private RespawnAsset _respawn;
        [SerializeField] private RunAsset _run;
    }
}