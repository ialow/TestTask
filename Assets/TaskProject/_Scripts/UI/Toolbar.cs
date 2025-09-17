using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Game
{
    public class Toolbar : MonoBehaviour
    {
        [Inject] private PopupSpawner _popupSpawner;
        [Inject] private LocalizationService _localizationService;
        [Inject] private CubeConfig _cubeCongif;

        [SerializeField] private Canvas _canvas;
        [SerializeField] private ScrollRect _scrollRect;
        [SerializeField] private Transform _containerForCubes;
        [SerializeField] private Cube _cubePrefab;

        public Canvas Canvas => _canvas;

        public void Init()
        {
            for (var i = 0; i < _cubeCongif.Size; i++)
            {
                Add(_cubeCongif.CubeSprite(i), i);
            }
        }

        public void Add(Sprite sprite, int indexInToolbar)
        {
            var cube = Instantiate(_cubePrefab, _containerForCubes);
            var rect = cube.GetComponent<RectTransform>();

            var data = new CubeData(rect.rect.height, rect.rect.width, sprite);
            cube.Init(this, data, _popupSpawner, _localizationService);
            cube.OnDragStarted = () =>
            {
                _popupSpawner.ShowPopup(_localizationService.Get("create_cube"));
                Add(sprite, indexInToolbar);
            };
            cube.transform.SetSiblingIndex(indexInToolbar);
        }

        public void ScrollSetActive(bool isActive)
        {
            _scrollRect.enabled = isActive;
        }
    }
}
