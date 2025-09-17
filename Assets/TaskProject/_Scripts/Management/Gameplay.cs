using UnityEngine;
using Zenject;

namespace Game
{
    public class Gameplay : MonoBehaviour
    {
        [Inject] private FSMGameplay _fSMGameplay;

        private void Start()
        {
            _fSMGameplay.EnterIn(StateGameplay.Init);
        }

        public void Puase()
        {
            _fSMGameplay.SuspendAndEnterIn(StateGameplay.Pause);
        }
    }
}
