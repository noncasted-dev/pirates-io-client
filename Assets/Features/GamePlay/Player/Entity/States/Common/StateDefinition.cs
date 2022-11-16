#region

using UnityEngine;

#endregion

namespace GamePlay.Player.Entity.States.Common
{
    public abstract class StateDefinition : ScriptableObject
    {
        [SerializeField] private StateDefinition[] _transitions;

        public bool IsTransitable(StateDefinition definition)
        {
            foreach (var transition in _transitions)
                if (definition == transition)
                    return true;

            return false;
        }
    }
}