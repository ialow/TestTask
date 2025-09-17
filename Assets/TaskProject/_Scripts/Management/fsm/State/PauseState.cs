using Untils;

namespace Game
{
    public class PauseState : IFSMState<StateGameplay>
    {
        private readonly PauserView _puaserView;

        public PauseState(PauserView puaserView)
        {
            _puaserView = puaserView;
        }

        public StateGameplay State => StateGameplay.Pause;

        public void Enter()
        {
            _puaserView.SetActive(true);
        }

        public void Exit()
        {
            _puaserView.SetActive(false);
        }
    }
}
