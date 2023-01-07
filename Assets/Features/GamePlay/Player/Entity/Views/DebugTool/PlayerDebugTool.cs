using GamePlay.Player.Entity.States.Deaths.Runtime;
using NaughtyAttributes;
using UnityEngine;
using VContainer;

namespace GamePlay.Player.Entity.Views.DebugTool
{
    public class PlayerDebugTool : MonoBehaviour
    {
        [Inject]
        private void Construct(IDeath death)
        {
            _death = death;
        }

        private IDeath _death;

        [Button("Kill")]
        private void Kill()
        {
            _death.Enter();
        }
    }
}