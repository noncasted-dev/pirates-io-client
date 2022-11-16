#region

using Common.EditableScriptableObjects.Attributes;
using Local.ComposedSceneConfig;
using Local.Services.Abstract;
using Menu.Common;
using Menu.Services.Common.Scope;
using Menu.Services.MenuLoop.Runtime;
using Menu.Services.UI.Runtime;
using UnityEngine;
using VContainer.Unity;

#endregion

namespace Menu.Bootstrap
{
    [CreateAssetMenu(fileName = "Menu", menuName = MenuAssetsPaths.Root + "Scene")]
    public class MenuAsset : ComposedSceneAsset
    {
        [SerializeField] [EditableObject] private MenuLoopAsset _loop;
        [SerializeField] [EditableObject] private MenuUIAsset _ui;

        [SerializeField] private MenuScope _scopePrefab;

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