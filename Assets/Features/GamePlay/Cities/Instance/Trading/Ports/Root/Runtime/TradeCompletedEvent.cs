using System;
using GamePlay.Cities.Instance.Storage.Runtime;
using GamePlay.Items.Abstract;

namespace GamePlay.Cities.Instance.Trading.Ports.Root.Runtime
{
    public readonly struct TradeCompletedEvent
    {
        public TradeCompletedEvent(Action<IItem[], IItem[], IPriceProvider> redrawCallback)
        {
            RedrawCallback = redrawCallback;
        }
        
        public readonly Action<IItem[], IItem[], IPriceProvider> RedrawCallback;
    }
}