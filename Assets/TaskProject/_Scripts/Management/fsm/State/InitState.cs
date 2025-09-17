using Untils;

namespace Game
{
    public class InitState : IFSMState<StateGameplay>
    {
        private readonly FSMGameplay _fSM;
        private readonly PauserView _pauserView;
        private readonly Toolbar _toolbar;
        private readonly TowerArea _towerArea;

        public InitState(FSMGameplay fSMGameplay, PauserView pauserView, Toolbar toolbar, TowerArea towerArea)
        {
            _fSM = fSMGameplay;
            _pauserView = pauserView;
            _toolbar = toolbar;
            _towerArea = towerArea;
        }

        public StateGameplay State => StateGameplay.Init;

        public void Enter()
        {
            _pauserView.Init();
            _toolbar.Init();

            _towerArea.Init(new Tower(new DefaultPlacementValidator())); //to do: Создавать класс Tower с учетем конфига
            _fSM.EnterIn(StateGameplay.GameLoop);
        }

        public void Exit()
        {
            
        }
    }
}
