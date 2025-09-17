using UnityEngine;
using Zenject;

namespace Game
{
    public class GameplayInstaller : MonoInstaller
    {
        [SerializeField] private Toolbar _toolbar;
        [SerializeField] private TowerArea _towerAreaView;
        [SerializeField] private PopupSpawner _popupSpawner;
        [SerializeField] private PauserView _puaserView;
        [SerializeField] private GameOverView _gameOverView;

        [Header("Конфиги")]
        [SerializeField] private CubeConfig _cubeCongif;
        [SerializeField] private LocalizationConfig _localizationConfig;
        
        public override void InstallBindings()
        {
            Container.Bind<Toolbar>().FromInstance(_toolbar).AsSingle();
            Container.Bind<TowerArea>().FromInstance(_towerAreaView).AsSingle();
            Container.Bind<PopupSpawner>().FromInstance(_popupSpawner).AsSingle();
            Container.Bind<PauserView>().FromInstance(_puaserView).AsSingle();
            Container.Bind<GameOverView>().FromInstance(_gameOverView).AsSingle();

            Container.Bind<CubeConfig>().FromInstance(_cubeCongif).AsSingle();
            Container.Bind<LocalizationConfig>().FromInstance(_localizationConfig).AsSingle();
            Container.Bind<LocalizationService>().AsSingle();

            Container.BindInterfacesAndSelfTo<InitState>().AsSingle();
            Container.BindInterfacesAndSelfTo<GameLoop>().AsSingle();
            Container.BindInterfacesAndSelfTo<PauseState>().AsSingle();
            Container.BindInterfacesAndSelfTo<GameOver>().AsSingle();
            Container.BindInterfacesAndSelfTo<RestartGame>().AsSingle();
            Container.Bind<FSMGameplay>().AsSingle();
        }
    }
}
