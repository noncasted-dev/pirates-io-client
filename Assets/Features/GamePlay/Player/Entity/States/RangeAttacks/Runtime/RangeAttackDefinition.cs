#region

using GamePlay.Player.Entity.Setup.Path;
using GamePlay.Player.Entity.States.Common;
using UnityEngine;

#endregion

namespace GamePlay.Player.Entity.States.RangeAttacks.Runtime
{
    [CreateAssetMenu(fileName = PlayerAssetsPaths.DefinitionPrefix + "RangeAttack",
        menuName = PlayerAssetsPaths.RangeAttack + "Definition")]
    public class RangeAttackDefinition : StateDefinition
    {
    }
}