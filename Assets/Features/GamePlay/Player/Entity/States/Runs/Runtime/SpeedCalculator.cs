using GamePlay.Player.Entity.Components.Healths.Runtime;
using GamePlay.Player.Entity.Components.InertialMovements.Runtime;
using GamePlay.Player.Entity.Components.ShipResources.Runtime;
using UnityEngine;

namespace GamePlay.Player.Entity.States.Runs.Runtime
{
    public class SpeedCalculator : ISpeedCalculator
    {
        public SpeedCalculator(IShipResources resources, IShipResourcesPresenter resourcesPresenter, ISail sail)
        {
            _resources = resources;
            _resourcesPresenter = resourcesPresenter;
            _sail = sail;
        }

        private const int _speedModifier = 7;

        private readonly IShipResources _resources;
        private readonly IShipResourcesPresenter _resourcesPresenter;
        private readonly ISail _sail;

        private bool _isInShallow;
        private bool _isBoarding;

        public float GetSpeed()
        {
            var sail = _sail.Strength / 100f;

            var loadDelta = _resources.Weight / (float)_resources.MaxWeight;
            var load = 1f - (loadDelta - 1f);

            load = Mathf.Clamp(load, 0.2f, 1f);

            var speed = _resources.MaxSpeed * sail * load;

            if (_isInShallow == true)
                speed *= 0.8f;

            if (_isBoarding == true)
                speed *= 0.5f;

            _resourcesPresenter.SetSpeed(Mathf.CeilToInt(speed));

            var result = speed / _speedModifier;

            return result;
        }

        public void OnShallowEntered()
        {
            _isInShallow = true;
        }

        public void OnShallowExited()
        {
            _isInShallow = false;
        }

        public void OnBoardingEntered()
        {
            _isBoarding = true;
        }

        public void OnBoardingExited()
        {
            _isBoarding = false;
        }
    }
}