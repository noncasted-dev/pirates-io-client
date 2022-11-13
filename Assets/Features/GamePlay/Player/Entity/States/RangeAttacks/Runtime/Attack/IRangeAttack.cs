namespace GamePlay.Player.Entity.States.RangeAttacks.Runtime.Attack
{
    public interface IRangeAttack
    {
        bool HasInput { get; }
        void OnActionInput();
        void OnAttackInputCanceled();
        void Enter();
    }
}