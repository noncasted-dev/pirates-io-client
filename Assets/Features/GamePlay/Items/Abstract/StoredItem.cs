namespace GamePlay.Items.Abstract
{
    public readonly struct StoredItem
    {
        public StoredItem(ItemType type, int amount)
        {
            Type = type;
            Amount = amount;
        }

        public readonly ItemType Type;
        public readonly int Amount;
    }
}