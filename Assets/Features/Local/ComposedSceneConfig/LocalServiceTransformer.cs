#region

using UnityEngine;
using UnityEngine.SceneManagement;

#endregion

namespace Local.ComposedSceneConfig
{
    public class LocalServiceTransformer
    {
        public LocalServiceTransformer(Scene scene)
        {
            _scene = scene;
        }

        private readonly Scene _scene;

        public void AddService(MonoBehaviour service)
        {
            SceneManager.MoveGameObjectToScene(service.gameObject, _scene);
        }
    }
}