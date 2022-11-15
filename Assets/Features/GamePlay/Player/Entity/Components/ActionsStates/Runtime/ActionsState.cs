namespace GamePlay.Player.Entity.Components.ActionsStates.Runtime
{
    public class ActionsState : IActionsStatePresenter, IActionsStateProvider
    {
        private bool _canShoot = true;

        public bool CanShoot => _canShoot;

        public void EnableShooting()
        {
            _canShoot = true;
        }

        public void DisableShooting()
        {
            _canShoot = false;
        }
    }
}