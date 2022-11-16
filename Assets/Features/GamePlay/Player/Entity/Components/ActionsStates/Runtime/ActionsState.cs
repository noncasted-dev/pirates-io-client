namespace GamePlay.Player.Entity.Components.ActionsStates.Runtime
{
    public class ActionsState : IActionsStatePresenter, IActionsStateProvider
    {
        private bool _canShoot = true;

        public void EnableShooting()
        {
            _canShoot = true;
        }

        public void DisableShooting()
        {
            _canShoot = false;
        }

        public bool CanShoot => _canShoot;
    }
}