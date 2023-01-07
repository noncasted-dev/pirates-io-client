namespace GamePlay.Player.Entity.States.RangeAttacks.Runtime.Aim
{
    public class AimHandle : IAimHandle
    {
        public AimHandle()
        {
            _progress = 0f;
            _isCanceled = false;
        }

        private bool _isCanceled;

        private float _progress;

        public float Progress => _progress;
        public bool IsCanceled => _isCanceled;

        public void OnProgress(float progress)
        {
            _progress = progress;
        }

        public void OnCanceled()
        {
            _isCanceled = true;
        }
    }
}