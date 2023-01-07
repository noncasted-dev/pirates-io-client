using UnityEngine;
using UnityEngine.Tilemaps;

namespace Common.VFX
{
    public class TileColorizer : MonoBehaviour
    {
        public float range;
        public Tilemap map;

        public bool doColorizeWater;

        private void LateUpdate()
        {
            if (doColorizeWater) doColorizeWater = false;
        }

        private void OnDrawGizmos()
        {
            Gizmos.DrawWireSphere(transform.position, range);
        }
    }
}