using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Zenject;

namespace Game
{
    public class Cube : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
    {
        private Toolbar _toolbar;

        private PopupSpawner _popupSpawner;
        private LocalizationService _localizationService;

        [SerializeField] private Image _raycastImage;
        [SerializeField] private RectTransform _rectTransform;

        public CubeData Data { get; private set; }

        public void Init(Toolbar toolbar, CubeData data, PopupSpawner popupSpawner, LocalizationService localizationService)
        {
            Data = data;
            
            _raycastImage.sprite = Data.Sprite;
            _toolbar = toolbar;

            _popupSpawner = popupSpawner;
            _localizationService = localizationService;
        }

        public Action OnDragStarted { get; set; }
        public Action OnDragEnded { get; set; }

        public void SetActiveRaycast(bool isActive)
        {
            _raycastImage.raycastTarget = isActive;
        }

        public void PlayDestroyAnimation()
        {
            _popupSpawner.ShowPopup(_localizationService.Get("cube_removed"));
            _raycastImage.DOFade(0f, 0.5f).OnComplete(() => Destroy(gameObject));
        }

        public void OnBeginDrag(PointerEventData eventData)
        {
            transform.SetParent(_toolbar.Canvas.transform);
            SetActiveRaycast(false);

            _toolbar.ScrollSetActive(false);

            OnDragEnded = PlayDestroyAnimation;
            OnDragStarted?.Invoke();
            OnDragStarted = null;
        }

        public void OnDrag(PointerEventData eventData)
        {
            _rectTransform.anchoredPosition += eventData.delta / _toolbar.Canvas.scaleFactor;
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            _toolbar.ScrollSetActive(true);

            OnDragEnded?.Invoke();
            OnDragEnded = null;
        }
    }
}
