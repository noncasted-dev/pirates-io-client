namespace GamePlay.Cities.Instance.Storage.Runtime
{
    public struct SellPrice
    {
        public SellPrice(int median, int total)
        {
            Median = median;
            Total = total;
        }
        
        public readonly int Median;
        public readonly int Total;
    }
}