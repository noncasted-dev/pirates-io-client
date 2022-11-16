#region

using GamePlay.Common.SceneObjects.Logs;
using UnityEngine;
using VContainer;

#endregion

namespace GamePlay.Common.SceneObjects.Runtime
{
    public class SceneObjectsHandler : MonoBehaviour, ISceneObjectsHandler, ISceneObjectsStorage
    {
        [Inject]
        private void Construct(SceneObjectLogger logger)
        {
            _logger = logger;
        }

        [SerializeField] private SceneObject[] _objects;

        private SceneObjectLogger _logger;

        public void InvokeAwake()
        {
            foreach (var sceneObject in _objects)
                sceneObject.InvokeAwake();

            _logger.OnAwake(_objects.Length);
        }

        public void InvokeEnable()
        {
            foreach (var sceneObject in _objects)
                sceneObject.InvokeEnabled();

            _logger.OnEnable(_objects.Length);
        }

        public void InvokeStart()
        {
            foreach (var sceneObject in _objects)
                sceneObject.InvokeStart();

            _logger.OnStart(_objects.Length);
        }

        public void InvokeDisable()
        {
            foreach (var sceneObject in _objects)
                sceneObject.InvokeDisabled();

            _logger.OnDisable(_objects.Length);
        }

        public void InvokeDestroy()
        {
            foreach (var sceneObject in _objects)
                sceneObject.InvokeDestroyed();

            _logger.OnDestroy(_objects.Length);
        }

        public void InvokeFullStartup()
        {
            InvokeAwake();
            InvokeEnable();
            InvokeStart();
        }

        public void InvokeFullUnloading()
        {
            InvokeDisable();
            InvokeDestroy();
        }

        public void SetObjects(SceneObject[] objects)
        {
            _objects = objects;
        }
    }
}