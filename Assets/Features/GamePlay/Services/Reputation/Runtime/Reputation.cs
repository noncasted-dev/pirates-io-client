using System;
using GamePlay.Factions.Common;
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

        [SerializeField] private Sprite _englandFlag;
        [SerializeField] private Sprite _franceFlag;
        [SerializeField] private Sprite _hollandFlag;
        [SerializeField] private Sprite _spainFlag;
        [SerializeField] private Sprite _pirateFlag;
        
        private FactionType _faction;

        public int Value => _value;
        public Sprite Flag => GetFlag();
        public FactionType Faction => _faction;

        public int ConvertFromMoney(int spend)
        {
            var add = Mathf.CeilToInt(spend * _percentFromMoney);

            return add;
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

        public void OnFactionSelected(FactionType faction)
        {
            _faction = faction;
        }

        private Sprite GetFlag()
        {
            return _faction switch
            {
                FactionType.England => _englandFlag,
                FactionType.France => _franceFlag,
                FactionType.Pirates => _pirateFlag,
                FactionType.Holland => _hollandFlag,
                FactionType.Spain => _spainFlag,
                _ => throw new ArgumentOutOfRangeException()
            };
        }
    }
}