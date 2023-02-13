namespace Features.GamePlay.Player.Entity.Components.Teams.Runtime
{
    public struct TeamBoardingStats
    {
        public TeamBoardingStats(int[] levelOne, int[] levelTwo, int[] levelThree, int sabersCount, int gunsCount)
        {
            LevelOne = levelOne;
            LevelTwo = levelTwo;
            LevelThree = levelThree;

            SabersCount = sabersCount;
            GunsCount = gunsCount;
        }
        
        public readonly int[] LevelOne;
        public readonly int[] LevelTwo;
        public readonly int[] LevelThree;

        public readonly int SabersCount;
        public readonly int GunsCount;
    }
}