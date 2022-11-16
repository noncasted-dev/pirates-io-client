#region

using GamePlay.Player.Entity.Setup.Path;
using GamePlay.Player.Entity.States.Common;
using UnityEngine;

#endregion

namespace GamePlay.Player.Entity.States.Runs.Runtime
{
    [CreateAssetMenu(fileName = PlayerAssetsPaths.DefinitionPrefix + "Run",
        menuName = PlayerAssetsPaths.Run + "Definition")]
    public class RunDefinition : StateDefinition
    {
    }
}