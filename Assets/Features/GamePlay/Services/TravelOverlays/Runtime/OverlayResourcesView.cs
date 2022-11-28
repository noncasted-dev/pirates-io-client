using TMPro;
using UnityEngine;

namespace GamePlay.Services.TravelOverlays.Runtime
{
    [DisallowMultipleComponent]
    public class OverlayResourcesView : MonoBehaviour
    {
        [SerializeField] private TMP_Text _guns;
        [SerializeField] private TMP_Text _cannons;
        [SerializeField] private TMP_Text _food;
        [SerializeField] private TMP_Text _repairMaterials;
    }
}