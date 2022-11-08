namespace GamePlay.Player.Entity.Views.Sprites.Runtime
{
    public interface ISpriteSwitcher
    {
        void Enable(bool enableSubSprites = false);
        void Disable(bool disableSubSprites = false);
    }
}