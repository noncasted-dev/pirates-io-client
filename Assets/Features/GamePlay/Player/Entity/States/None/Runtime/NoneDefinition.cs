using GamePlay.Player.Entity.Setup.Path;
using GamePlay.Player.Entity.States.Common;
using UnityEngine;

namespace GamePlay.Player.Entity.States.None.Runtime
{
    [CreateAssetMenu(fileName = PlayerAssetsPaths.DefinitionPrefix + "None",
        menuName = PlayerAssetsPaths.None + "Definition")]
    public class NoneDefinition : StateDefinition
    {
    }
}