using System;
using NaughtyAttributes;
using UniRx;
using UnityEngine;

namespace GamePlay.Services.Reputation.Runtime
{
    [DisallowMultipleComponent]
    public class Reputation : MonoBehaviour, IReputation, IReputationPresenter
    {
        [SerializeField] [ReadOnly] private int _value;

        public int Value => _value;

        public void Add(int add)
        {
            _value += add;
            
            MessageBroker.Default.Publish(new ReputationChangedEvent(_value));
        }

        public void Reduce(int reduce)
        {
            _value -= reduce;

            MessageBroker.Default.Publish(new ReputationChangedEvent(_value));
        }
    }
}