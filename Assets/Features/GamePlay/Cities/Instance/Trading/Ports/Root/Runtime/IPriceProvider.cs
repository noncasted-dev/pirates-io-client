using GamePlay.Items.Abstract;

namespace GamePlay.Cities.Instance.Trading.Ports.Root.Runtime
{
    public interface IPriceProvider
    {
        int GetPrice(ItemType type, int count);
    }
}