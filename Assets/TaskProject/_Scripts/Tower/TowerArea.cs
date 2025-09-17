using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Zenject;

namespace Game
{
    public class TowerArea : MonoBehaviour, IDropHandler
    {
        [Inject] private FSMGameplay _fsmGameplay;
        [Inject] private PopupSpawner _popupSpawner;
        [Inject] private LocalizationService _localizationService;

        [SerializeField] private Canvas _canvas;
        [SerializeField] private RectTransform _areaForTower;
        [SerializeField] private float _dropDuration = 0.5f;

        private Tower _tower;

        public bool CanPlaceNextCube
        {
            get
            {
                var last = _tower.GetLast();
                var topY = _tower.GetTopAnchorPositionY(_tower.Elements.Count - 1);
                var areaTopY = _areaForTower.rect.y + _areaForTower.rect.height * 0.5f;
                return topY + last.rect.height >= areaTopY;
            }
        }

        public void Init(Tower tower)
        {
            _tower = tower;
        }

        public void OnDrop(PointerEventData eventData)
        {
            if (eventData.pointerDrag.TryGetComponent<RectTransform>(out var rectElement))
            {
                var cube = rectElement.GetComponent<Cube>();
                cube.OnDragEnded = () => { cube.SetActiveRaycast(true); };
                rectElement.SetParent(transform);

                if (_tower.CanPlace(rectElement))
                {
                    cube.OnDragStarted = () =>
                    {
                        cube.transform.SetParent(_canvas.transform);
                        _popupSpawner.ShowPopup(_localizationService.Get("cube_moving"));
                        RemoveCube(cube);
                    };

                    _tower.Add(rectElement);
                    PlayAddedAnimation(rectElement);
                    _popupSpawner.ShowPopup(_localizationService.Get("tower_building"));

                    if (CanPlaceNextCube) _fsmGameplay.EnterIn(StateGameplay.GameOver);
                }
                else
                {
                    PlayFailedAnimate(rectElement);
                    _popupSpawner.ShowPopup(_localizationService.Get("missed"));
                }
            }
        }

        private void RemoveCube(Cube cube)
        {
            var indexInTower = _tower.IndexOfElement(cube.GetComponent<RectTransform>());
            var positionY = _tower.Elements[indexInTower].anchoredPosition.y;

            _tower.RemoveAt(indexInTower);
            cube.transform.SetParent(_canvas.transform);

            if (indexInTower <= _tower.CountElements - 1)
            {
                for (var i = indexInTower; i < _tower.CountElements; i++)
                {
                    var element = _tower.Elements[i];
                    var targetY = i - indexInTower == 0 ? positionY : positionY + element.rect.height * (i - indexInTower);

                    PlayOffsetAnimation(element, targetY, (i - indexInTower) * 0.05f);
                }
            }
        }

        private void PlayOffsetAnimation(RectTransform element, float targetY, float delay)
        {
            element.DOAnchorPosY(targetY, _dropDuration).SetEase(Ease.OutBounce).SetDelay(delay);
        }

        private void PlayAddedAnimation(RectTransform element)
        {
            if (_tower.CountElements > 1)
            {
                element.SetParent(_areaForTower);
                element.DOAnchorPos(_tower.GetTargetPosition(element), 0.6f).SetEase(Ease.InBack);
            }
        }

        private void PlayFailedAnimate(RectTransform element)
        {
            var position = element.anchoredPosition + new Vector2(0, _tower.Elements[0].anchoredPosition.y);

            var sequence = DOTween.Sequence();
            sequence.Append(element.DOAnchorPos(position, _dropDuration).SetEase(Ease.InQuad));
            sequence.Join(element.gameObject.GetComponent<Image>().DOFade(0f, _dropDuration).SetEase(Ease.Linear));

            sequence.OnComplete(() => Destroy(element.gameObject));
        }
    }
}
