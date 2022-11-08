namespace Global.Services.Updaters.Runtime.Abstract
{
    public interface IPostFixedUpdatable
    {
        void OnPostFixedUpdate(float delta);
    }
}