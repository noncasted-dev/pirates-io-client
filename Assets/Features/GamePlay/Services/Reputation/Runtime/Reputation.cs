using System;
using GamePlay.Cities.Instance.Root.Runtime;
using GamePlay.Common.Areas.Implementation.Cities;
using GamePlay.Factions.Common;
using GamePlay.Player.Entity.Network.Remote.Receivers.Damages.Runtime;
using GamePlay.Services.Saves.Definitions;
using Global.Services.FilesFlow.Runtime.Abstract;
using Global.Services.MessageBrokers.Runtime;
using NaughtyAttributes;
using UnityEngine;
using VContainer;

namespace GamePlay.Services.Reputation.Runtime
{
    [DisallowMultipleComponent]
    public class Reputation : MonoBehaviour, IReputation, IReputationPresenter
    {
        [Inject]
        private void Construct(IFileLoader fileLoader, IFileSaver fileSaver)
        {
            _fileSaver = fileSaver;
            _fileLoader = fileLoader;
        }

        [SerializeField] [ReadOnly] private int _value;
        [SerializeField] private float _percentFromMoney = 0.01f;

        [SerializeField] private Sprite _englandFlag;
        [SerializeField] private Sprite _franceFlag;
        [SerializeField] private Sprite _hollandFlag;
        [SerializeField] private Sprite _spainFlag;
        [SerializeField] private Sprite _pirateFlag;
        private IDisposable _cityEnterListener;

        private IDisposable _damageListener;

        private FactionType _faction;
        private IFileLoader _fileLoader;
        private IFileSaver _fileSaver;
        private CityDefinition _lastCity;

        private void OnEnable()
        {
            _damageListener = Msg.Listen<RemoteDamagedEvent>(OnRemoteDamaged);
            _cityEnterListener = Msg.Listen<CityEnteredEvent>(OnCityEntered);
        }

        private void OnDisable()
        {
            _damageListener?.Dispose();
            _cityEnterListener?.Dispose();
        }

        public int Value => _value;
        public Sprite Flag => GetFlag();
        public FactionType Faction => _faction;
        public CityDefinition LastCity => _lastCity;

        public int ConvertFromMoney(int spend)
        {
            var add = Mathf.CeilToInt(spend * _percentFromMoney);

            return add;
        }

        public void Add(int add)
        {
            _value += add;

            Msg.Publish(new ReputationChangedEvent(_value));
        }

        public void Reduce(int reduce)
        {
            _value -= reduce;

            Msg.Publish(new ReputationChangedEvent(_value));
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

            var save = _fileLoader.LoadOrCreate<ShipSave>();
            save.LastCity = data.City.Name;
            _fileSaver.Save(save);
        }
    }
}