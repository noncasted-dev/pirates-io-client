#region

using UnityEngine;

#endregion

namespace GamePlay.Player.Entity.Views.WeaponsRoots.Runtime
{
    public class WeaponsRoot : MonoBehaviour, IWeaponsRoot
    {
        public Transform Transform => transform;
        public Vector2 Position => transform.position;
    }
}