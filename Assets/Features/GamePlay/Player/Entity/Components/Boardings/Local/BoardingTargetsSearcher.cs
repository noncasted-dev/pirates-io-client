using GamePlay.Services.Network.RemoteEntities.Entity;
using GamePlay.Services.Network.RemoteEntities.Storage;
using UnityEngine;

namespace GamePlay.Player.Entity.Components.Boardings.Local
{
    public class BoardingTargetsSearcher : IBoardingTargetSearcher
    {
        public BoardingTargetsSearcher(
            IRemotePlayersRegistry registry,
            BoardingConfigAsset config)
        {
            _registry = registry;
            _config = config;
        }
        
        private readonly IRemotePlayersRegistry _registry;
        private readonly BoardingConfigAsset _config;

        public bool Search(Vector2 selfPosition, out IRemotePlayer target)
        {
            target = null;
            
            if (_registry.Any == false)
                return false;
            
            var remotes = _registry.Copy;

            Sort(remotes, 0, remotes.Length - 1, selfPosition);

            var first = remotes[0];
            var targetPosition = first.Position;
            var distance = Vector2.Distance(selfPosition, targetPosition);

            if (distance > _config.TriggerDistance)
                return false;

            target = first;
            return true;
        }
        
        private void Sort(IRemotePlayer[] array, int leftIndex, int rightIndex, Vector3 playerPosition)
        {
            float GetDistance(int index)
            {
                return Vector3.Distance(playerPosition, array[index].Position);
            }
            
            var i = leftIndex;
            var j = rightIndex;
            var pivot = GetDistance(leftIndex);

            while (i <= j)
            {
                while (GetDistance(i) < pivot)
                {
                    i++;
                }
        
                while (GetDistance(j) > pivot)
                {
                    j--;
                }
                if (i <= j)
                {
                    (array[i], array[j]) = (array[j], array[i]);
                    i++;
                    j--;
                }
            }
    
            if (leftIndex < j)
                Sort(array, leftIndex, j, playerPosition);
            if (i < rightIndex)
                Sort(array, i, rightIndex, playerPosition);
        }
    }
}