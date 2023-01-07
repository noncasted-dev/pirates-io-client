namespace GamePlay.Player.Entity.Weapons.Cannon.Components.Shooter
{
    public interface IShooter
    {
        void Cancel();
        void Shoot(float angle, float spread);
        void Shoot(float angle, float spread, int count);
    }
}