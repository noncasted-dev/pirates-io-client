using System;
using GamePlay.Cities.Instance.Root.Runtime;
using GamePlay.Common.Areas.Implementation.Cities;
using GamePlay.Factions.Common;
using GamePlay.Player.Entity.Network.Remote.Receivers.Damages.Runtime;
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
        private CityDefinition _lastCity;

        private IDisposable _damageListener;
        private IDisposable _cityEnterListener;

        public int Value => _value;
        public Sprite Flag => GetFlag();
        public FactionType Faction => _faction;
        public CityDefinition LastCity => _lastCity;

        private void OnEnable()
        {
            _damageListener = MessageBroker.Default.Receive<RemoteDamagedEvent>().Subscribe(OnRemoteDamaged);
            _cityEnterListener = MessageBroker.Default.Receive<CityEnteredEvent>().Subscribe(OnCityEntered);
        }

        private void OnDisable()
        {
            _damageListener?.Dispose();
            _cityEnterListener?.Dispose();
        }

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

        private void OnRemoteDamaged(RemoteDamagedEvent data)
        {
        }

        private void OnCityEntered(CityEnteredEvent data)
        {
            _lastCity = data.City;
        }
    }
}