namespace Global.Services.Updaters.Runtime.Abstract
{
    public interface IUpdater
    {
        void Add(IUpdatable updatable);
        void Remove(IUpdatable updatable);

        void Add(IPreUpdatable updatable);
        void Remove(IPreUpdatable updatable);

        void Add(IPreFixedUpdatable updatable);
        void Remove(IPreFixedUpdatable updatable);

        void Add(IFixedUpdatable updatable);
        void Remove(IFixedUpdatable updatable);

        void Add(IPostFixedUpdatable updatable);
        void Remove(IPostFixedUpdatable updatable);
    }
}