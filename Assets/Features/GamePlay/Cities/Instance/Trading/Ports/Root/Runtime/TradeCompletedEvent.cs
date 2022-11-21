using System;
using System.Collections.Generic;
using GamePlay.Cities.Instance.Storage.Runtime;
using GamePlay.Items.Abstract;

namespace GamePlay.Cities.Instance.Trading.Ports.Root.Runtime
{
    public readonly struct TradeCompletedEvent
    {
        public TradeCompletedEvent(
            Action<
                IReadOnlyList<IItem>,
                IReadOnlyList<IItem>,
                IReadOnlyList<IItem>,
                IPriceProvider> redrawCallback)
        {
            RedrawCallback = redrawCallback;
        }
        
        public readonly Action<
            IReadOnlyList<IItem>,
            IReadOnlyList<IItem>,
            IReadOnlyList<IItem>,
            IPriceProvider> RedrawCallback;
    }
}