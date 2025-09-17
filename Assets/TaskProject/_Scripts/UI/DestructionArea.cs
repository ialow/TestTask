using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Zenject;

namespace Game
{
    public class DestructionArea : MonoBehaviour, IDropHandler
    {
        private const float ALPHA_THRESHOLD = 0.1f;

        [SerializeField] private Image _alphaRayCastImage;
        [SerializeField] private float _dropDuration = 0.4f;
        [Inject] private PopupSpawner _popupSpawner;
        [Inject] private LocalizationService _localizationService;

        public void Awake()
        {
            _alphaRayCastImage.alphaHitTestMinimumThreshold = ALPHA_THRESHOLD;
        }

        public void OnDrop(PointerEventData eventData)
        {
            if (eventData.pointerDrag.TryGetComponent<RectTransform>(out var element))
            {
                var cube = element.GetComponent<Cube>();
                cube.OnDragEnded = null;

                element.SetParent(_alphaRayCastImage.transform);
                PlayDestroyAnimation(element);
                _popupSpawner.ShowPopup(_localizationService.Get("cube_in_deletion_area"));
            }
        }

        private void PlayDestroyAnimation(RectTransform element)
        {
            var topBorder = _alphaRayCastImage.rectTransform.rect.height * 0.5f;
            var targetY = -(topBorder + element.rect.height);

            var sequenceAnimations = DOTween.Sequence();
            sequenceAnimations.Append(element.DOAnchorPosY(targetY, _dropDuration).SetEase(Ease.InQuad));
            sequenceAnimations.OnComplete(() => Destroy(element.gameObject));
        }
    }
}
