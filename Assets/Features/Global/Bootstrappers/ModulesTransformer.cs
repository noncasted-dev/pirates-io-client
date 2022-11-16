using UnityEngine;
using UnityEngine.SceneManagement;

namespace Global.Bootstrappers
{
    public class ModulesTransformer
    {
        public ModulesTransformer(Scene scene)
        {
            _scene = scene;
        }

        private readonly Scene _scene;

        public void AddModule(Component module)
        {
            var serviceTransform = module.transform;

            SceneManager.MoveGameObjectToScene(module.gameObject, _scene);

            serviceTransform.position = Vector3.zero;
            serviceTransform.rotation = Quaternion.Euler(0f, 0f, 0f);
        }
    }
}