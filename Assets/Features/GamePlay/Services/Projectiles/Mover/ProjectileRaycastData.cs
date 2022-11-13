namespace GamePlay.Services.Projectiles.Mover
{
    public class ProjectileRaycastData
    {
        public ProjectileRaycastData(float colliderHeight)
        {
            ColliderHeight = colliderHeight;
        }
        
        public readonly float ColliderHeight;

        private float _angle;

        public float Angle => _angle;

        public void Setup(float angle)
        {
            _angle = angle;
        }
    }
}