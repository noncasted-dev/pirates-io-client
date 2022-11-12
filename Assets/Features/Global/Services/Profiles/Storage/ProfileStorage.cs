using Features.Global.Services.Profiles.Logs;
using NaughtyAttributes;
using UnityEngine;
using VContainer;

namespace Features.Global.Services.Profiles.Storage
{
    [DisallowMultipleComponent]
    public class ProfileStorage : MonoBehaviour, IProfileStoragePresenter, IProfileStorageProvider
    {
        [Inject]
        private void Construct(ProfileLogger logger)
        {
            _logger = logger;
        }
        
        [SerializeField] [ReadOnly] private string _userName = string.Empty;
        private ProfileLogger _logger;

        public string UserName => _userName;
        
        public void SetUserName(string userName)
        {
            _userName = userName;

            _logger.OnUserNameSet(userName);
        }
    }
}