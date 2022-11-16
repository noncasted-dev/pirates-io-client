using System.Collections;
using Cysharp.Threading.Tasks;
using Global.Services.Loggers.Runtime;
using UnityEditor;
using UnityEngine;
using UnityEngine.TestTools;
using ILogger = Global.Services.Loggers.Runtime.ILogger;

namespace Global.Services.AssetsFlow.Tests
{
    public class AssetsFlowTest
    {
        [UnityTest]
        public IEnumerator TestFlow()
        {
            var handlerPrefab = AssetDatabase.LoadAssetAtPath<GameObject>(
                "Assets/Features/Global/Services/AssetsFlow/Tests/AssetsFlow_TestHandler.prefab");

            var handler = Object.Instantiate(handlerPrefab).GetComponent<AssetsFlowTestHandler>();

            handler.Construct();

            var instantiator = handler.Factory.Create<TestObject>(handler.Reference);

            var result = instantiator.PreloadAsync();

            while (result.Status != UniTaskStatus.Succeeded)
                yield return null;

            var instantiated = instantiator.Instantiate(Vector2.zero);
            instantiated.Run();
            instantiated = instantiator.Instantiate(Vector2.zero);
            instantiated.Run();
            instantiated = instantiator.Instantiate(Vector2.zero);
            instantiated.Run();

            instantiator.Release();

            result = instantiator.PreloadAsync();

            while (result.Status != UniTaskStatus.Succeeded)
                yield return null;

            instantiated = instantiator.Instantiate(Vector2.zero);
            instantiated.Run();
            instantiated = instantiator.Instantiate(Vector2.zero);
            instantiated.Run();
            instantiated = instantiator.Instantiate(Vector2.zero);
            instantiated.Run();

            instantiator.Release();
        }

        public class LoggerMock : ILogger
        {
            public void Log(string message)
            {
                Debug.Log(message);
            }

            public void Log(string header, string message)
            {
                Debug.Log(message);
            }

            public void Log(string message, LogParameters parameters)
            {
                Debug.Log(message);
            }

            public void Warning(string warning)
            {
                Debug.Log(warning);
            }

            public void Error(string error)
            {
                Debug.Log(error);
            }

            public void Error(string error, LogParameters parameters)
            {
                Debug.Log(error);
            }
        }
    }
}