using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Untils;
using Zenject;

namespace Game
{
    public class FSMGameplay : FSMDictionary<StateGameplay>
    {
        [Inject]
        public void Init(IEnumerable<IFSMState<StateGameplay>> states)
        {
            States = states.ToDictionary(x => x.State, x => x);
        }

        public override void EnterIn(StateGameplay state)
        {
            var nameState = StackStates.TryPeek(out var stateObj) ? stateObj.GetType().Name : "";
            Debug.Log($"[FSMGameplay] '{nameState}' -> '{state}'");

            base.EnterIn(state);
        }

        public override void SuspendAndEnterIn(StateGameplay state)
        {
            var nameState = StackStates.TryPeek(out var stateObj) ? stateObj.GetType().Name : "";
            Debug.Log($"[FSMGameplay] Приастоновка '{nameState}' ⏸ переход в '{state}'");

            base.SuspendAndEnterIn(state);
        }

        public override void ExitAndResume()
        {
            if (StackStates.Count > 1)
            {
                var currentState = StackStates.Peek();
                var previousState = StackStates.ToArray()[1];

                Debug.Log($"[FSMGameplay] Выход из '{currentState.GetType().Name}' ⏯ возврат в '{previousState.GetType().Name}'");
            }
            else
            {
                var nameState = StackStates.TryPeek(out var stateObj) ? stateObj.GetType().Name : "";
                Debug.LogWarning($"[FSMGameplay] Попытка выхода из состояния '{nameState}'");
            }

            base.ExitAndResume();
        }
    }
}
