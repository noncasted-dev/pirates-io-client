namespace GamePlay.Cities.Instance.Storage.Runtime
{
    public struct SellPrice
    {
        public SellPrice(int single, int total)
        {
            Single = single;
            Total = total;
        }
        
        public readonly int Single;
        public readonly int Total;
    }
}