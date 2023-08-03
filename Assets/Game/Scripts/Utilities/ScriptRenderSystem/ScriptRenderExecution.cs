using System;
using UnityEditor;

namespace Game.Scripts.Utilities.ScriptRenderSystem
{
#if UNITY_EDITOR
    [InitializeOnLoad]
    public class ScriptRenderExecution
    {
        static ScriptRenderExecution()
        {
            foreach (MonoScript monoScript in MonoImporter.GetAllRuntimeMonoScripts())
            {
                if (monoScript.GetClass() != null)
                {
                    foreach (var a in Attribute.GetCustomAttributes(monoScript.GetClass(), typeof(ScriptOrder)))
                    {
                        var currentOrder = MonoImporter.GetExecutionOrder(monoScript);
                        var newOrder = ((ScriptOrder)a).Order;
                        if (currentOrder != newOrder)
                            MonoImporter.SetExecutionOrder(monoScript, newOrder);
                    }
                }
            }
        }
    }
#endif
}