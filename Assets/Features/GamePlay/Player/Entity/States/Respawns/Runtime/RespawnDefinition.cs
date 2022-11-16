using GamePlay.Player.Entity.Setup.Path;
using GamePlay.Player.Entity.States.Common;
using UnityEngine;

namespace GamePlay.Player.Entity.States.Respawns.Runtime
{
    [CreateAssetMenu(fileName = PlayerAssetsPaths.DefinitionPrefix + "Respawn",
        menuName = PlayerAssetsPaths.Respawn + "Definition")]
    public class RespawnDefinition : StateDefinition
    {
    }
}