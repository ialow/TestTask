using UnityEngine.SceneManagement;
using Untils;

namespace Game
{
    public class RestartGame : IFSMState<StateGameplay>
    {
        public RestartGame()
        {
            
        }

        public StateGameplay State => StateGameplay.Restart;

        public void Enter()
        {
            var currentScene = SceneManager.GetActiveScene();
            SceneManager.LoadScene(currentScene.name);
        }

        public void Exit()
        {
            
        }
    }
}
