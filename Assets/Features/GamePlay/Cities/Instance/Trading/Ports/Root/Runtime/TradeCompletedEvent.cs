using System;
using System.Collections.Generic;
using GamePlay.Cities.Instance.Storage.Runtime;
using GamePlay.Items.Abstract;
using GamePlay.Items.Implementation;

namespace GamePlay.Cities.Instance.Trading.Ports.Root.Runtime
{
    public readonly struct TradeCompletedEvent
    {
        public TradeCompletedEvent(
            Action<
                IReadOnlyList<IItem>,
                IReadOnlyList<IItem>,
                IReadOnlyList<IItem>,
                IPriceProvider> redrawCallback,
            TradeResult result)
        {
            RedrawCallback = redrawCallback;
            Result = result;
        }
        
        public readonly Action<
            IReadOnlyList<IItem>,
            IReadOnlyList<IItem>,
            IReadOnlyList<IItem>,
            IPriceProvider> RedrawCallback;

        public readonly TradeResult Result;
    }
}