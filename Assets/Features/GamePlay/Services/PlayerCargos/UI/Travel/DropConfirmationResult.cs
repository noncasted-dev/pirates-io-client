namespace GamePlay.Services.PlayerCargos.UI.Travel
{
    public readonly struct DropConfirmationResult
    {
        private DropConfirmationResult(DropConfirmationResultType type, int count)
        {
            Type = type;
            Count = count;
        }
        
        public readonly DropConfirmationResultType Type;
        public readonly int Count;

        public static DropConfirmationResult Canceled()
        {
            return new DropConfirmationResult(DropConfirmationResultType.Canceled, 0);
        }
        
        public static DropConfirmationResult Applied(int count)
        {
            return new DropConfirmationResult(DropConfirmationResultType.Applied, count);
        }
    }
}