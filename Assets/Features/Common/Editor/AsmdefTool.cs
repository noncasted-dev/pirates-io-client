using UnityEditor;
using UnityEditor.Compilation;
using UnityEngine;

namespace Common.Editor
{
    public class AsmdefTool
    {
        [MenuItem("Tools/DebugAssemblies")]
        public static void ProcessAsmdef()
        {
            var playerAssemblies =
                CompilationPipeline.GetAssemblies(AssembliesType.Player);

            foreach (var assembly in playerAssemblies)
            {
                Debug.Log($"NAME: {assembly.name}");
                
                foreach (var reference in assembly.allReferences)
                {
                    Debug.Log($"REFERENCE: {reference}");
                }
            }
        }
    }
}