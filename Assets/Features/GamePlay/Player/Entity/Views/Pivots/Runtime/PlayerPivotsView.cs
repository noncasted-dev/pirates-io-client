#region

using UnityEngine;

#endregion

namespace GamePlay.Player.Entity.Views.Pivots.Runtime
{
    public class PlayerPivotsView : MonoBehaviour, IPivots
    {
        [SerializeField] private PivotsDictionary _pivots;

        public Vector2 GetPosition(PivotType type)
        {
            return _pivots[type].position;
        }
    }
}