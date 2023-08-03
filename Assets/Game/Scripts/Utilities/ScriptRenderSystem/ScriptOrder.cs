using System;

namespace Game.Scripts.Utilities.ScriptRenderSystem
{
    public class ScriptOrder : Attribute
    {
        public readonly int Order;

        public ScriptOrder(int order)
        {
            Order = order;
        }
    }
}