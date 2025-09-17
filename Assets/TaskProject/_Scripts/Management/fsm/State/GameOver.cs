using Untils;

namespace Game
{
    public class GameOver : IFSMState<StateGameplay>
    {
        private readonly GameOverView _gameOverView;

        public GameOver(GameOverView gameOverView)
        {
            _gameOverView = gameOverView;
        }

        public StateGameplay State => StateGameplay.GameOver;

        public void Enter()
        {
            //to do: сбрасывать сохраненый конфиг с башней
            _gameOverView.Init();
            _gameOverView.Active();
        }

        public void Exit()
        {
            
        }
    }
}
