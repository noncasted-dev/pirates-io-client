using GamePlay.Services.Projectiles.Entity;
using Global.Services.InputViews.Runtime;
using Local.Services.Abstract.Callbacks;

namespace GamePlay.Services.Projectiles.Selector.Runtime
{
    public class ProjectilesSelectorInput : ILocalSwitchListener
    {
        public ProjectilesSelectorInput(IProjectileSelector selector, IInputView inputView)
        {
            _selector = selector;
            _inputView = inputView;
        }
        
        private readonly IProjectileSelector _selector;
        private readonly IInputView _inputView;
        
        public void OnEnabled()
        {
            _inputView.SelectFirstProjectilePerformed += OnFirstPressed;
            _inputView.SelectSecondProjectilePerformed += OnSecondPressed;
            _inputView.SelectThirdProjectilePerformed += OnThirdPressed;
            _inputView.SelectForthProjectilePerformed += OnForthPressed;
        }

        public void OnDisabled()
        {
            _inputView.SelectFirstProjectilePerformed -= OnFirstPressed;
            _inputView.SelectSecondProjectilePerformed -= OnSecondPressed;
            _inputView.SelectThirdProjectilePerformed -= OnThirdPressed;
            _inputView.SelectForthProjectilePerformed -= OnForthPressed;
        }

        private void OnFirstPressed()
        {
            _selector.Select(ProjectileType.Ball);
        }
        
        private void OnSecondPressed()
        {
            _selector.Select(ProjectileType.Knuppel);
        }
        
        private void OnThirdPressed()
        {
            _selector.Select(ProjectileType.Shrapnel);
        }
        
        private void OnForthPressed()
        {
            _selector.Select(ProjectileType.Fishnet);
        }
    }
}