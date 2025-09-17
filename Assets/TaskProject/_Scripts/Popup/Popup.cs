using DG.Tweening;
using TMPro;
using UnityEngine;

namespace Game
{
    public class Popup : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _message;
        [SerializeField] private RectTransform _rect;
        [SerializeField] private float _duration = 0.1f;
        [SerializeField] private float _fadeOutDuration = 0.06f;

        public Vector2 AnchoredPosition => _rect.anchoredPosition;

        public void Show(string message, float targetAnchorY)
        {
            _message.alpha = 0f;
            _message.text = message;

            var sequence = DOTween.Sequence();
            sequence.Append(_message.DOFade(1f, _duration));
            sequence.Join(_rect.DOAnchorPosY(targetAnchorY, _duration).SetEase(Ease.OutCubic));
            sequence.Append(_message.DOFade(0f, _fadeOutDuration));
            sequence.OnComplete(() => Destroy(gameObject));
        }
    }
}
