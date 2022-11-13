using UnityEngine;

namespace GamePlay.Services.Projectiles.Mover
{
    public class ProjectileRaycastData
    {
        public ProjectileRaycastData(float colliderHeight)
        {
            ColliderHeight = colliderHeight;
        }
        
        public readonly float ColliderHeight;

        private LayerMask _layerMask;
        private float _angle;

        public LayerMask LayerMask => _layerMask;
        public float Angle => _angle;

        public void Setup(LayerMask layerMask, float angle)
        {
            _layerMask = layerMask;
            _angle = angle;
        }
    }
}