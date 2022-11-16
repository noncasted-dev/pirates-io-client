namespace GamePlay.Items.Abstract
{
    public interface IItem
    {
        BaseItemData BaseData { get; }
        int Count { get; }

        void Add(int amount);
        void Reduce(int amount);
    }
}