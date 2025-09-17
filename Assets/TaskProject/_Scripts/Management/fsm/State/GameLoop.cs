using DG.Tweening;
using Untils;

namespace Game
{
    public class GameLoop : ISuspendFSMState<StateGameplay>
    {
        public GameLoop()
        {

        }

        public StateGameplay State => StateGameplay.GameLoop;

        public void Enter()
        {

        }

        public void Exit()
        {

        }

        public void Resume()
        {
            DOTween.PlayAll();
        }

        public void Suspend()
        {
            DOTween.PauseAll();
        }
    }
}
