#region

using GamePlay.Player.Entity.Setup.Path;
using GamePlay.Player.Entity.States.Common;
using UnityEngine;

#endregion

namespace GamePlay.Player.Entity.States.Idles.Runtime
{
    [CreateAssetMenu(fileName = PlayerAssetsPaths.DefinitionPrefix + "Idle",
        menuName = PlayerAssetsPaths.Idle + "Definition")]
    public class IdleDefinition : StateDefinition
    {
    }
}