using System.Collections.Generic;
using GamePlay.Cities.Instance.Root.Runtime;
using Global.Services.ItemFactories.Runtime;
using UnityEngine;
using UnityEngine.UI;

namespace Features.GamePlay.Services.Maps.Runtime
{
    [DisallowMultipleComponent]
    public class MapEntry : MonoBehaviour
    {
        [SerializeField] private List<Image> _most;
        [SerializeField] private List<Image> _less;

        [SerializeField] private CityDefinition _definition;

        public void Construct(IItemFactory factory)
        {
            for (var i = 0; i < _definition.MostProduced.Count; i++)
                _most[i].sprite = factory.Create(_definition.MostProduced[i], 1).BaseData.Icon;
            
            for (var i = 0; i < _definition.LeastProduced.Count; i++)
                _less[i].sprite = factory.Create(_definition.LeastProduced[i], 1).BaseData.Icon;
        }
    }
}