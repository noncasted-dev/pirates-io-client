using Common.Local.ComposedSceneConfig;
using Common.Local.Services.Abstract;
using Menu.Common;
using Menu.Services.Common.Scope;
using Menu.Services.MenuLoop.Runtime;
using Menu.Services.UI.Runtime;
using UnityEngine;
using VContainer.Unity;

namespace Menu.Bootstrap
{
    [CreateAssetMenu(fileName = "Menu", menuName = MenuAssetsPaths.Root + "Scene")]
    public class MenuAsset : ComposedSceneAsset
    {
        [SerializeField]  private MenuLoopAsset _loop;

        [SerializeField] private MenuScope _scopePrefab;
        [SerializeField]  private MenuUIAsset _ui;

        protected override LocalServiceAsset[] AssignServices()
        {
            return new LocalServiceAsset[]
            {
                _loop,
                _ui
            };
        }

        protected override LifetimeScope AssignScope()
        {
            return _scopePrefab;
        }
    }
}