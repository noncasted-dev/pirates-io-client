namespace GamePlay.Player.Entity.States.RangeAttacks.Runtime.Attack
{
    public interface IRangeAttack
    {
        bool HasInput { get; }
        void OnInput();
        void OnInputCanceled();
        void OnInputBroke();
        void Enter();
    }
}