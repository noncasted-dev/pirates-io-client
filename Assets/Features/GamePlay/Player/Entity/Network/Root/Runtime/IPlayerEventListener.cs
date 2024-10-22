﻿using System;
using Ragon.Client;

namespace GamePlay.Player.Entity.Network.Root.Runtime
{
    public interface IPlayerEventListener
    {
        void AddListener<TEvent>(Action<RagonPlayer, TEvent> callback)
            where TEvent : IRagonEvent, new();
    }
}