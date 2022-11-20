using NaughtyAttributes;
using UniRx;
using UnityEngine;

namespace GamePlay.Services.Reputation.Runtime
{
    [DisallowMultipleComponent]
    public class Reputation : MonoBehaviour, IReputation, IReputationPresenter
    {
        [SerializeField] [ReadOnly] private int _value;
        [SerializeField] private float _percentFromMoney = 0.01f;

        public int Value => _value;

        public void ConvertFromMoney(int spend)
        {
            var add = Mathf.CeilToInt(spend * _percentFromMoney);
            
            Add(add);
        }

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