using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Game
{
    public class PauserView : MonoBehaviour
    {
        [Inject] private FSMGameplay _fsmGameplay;
        [Inject] private LocalizationService _localizationService;

        [SerializeField] private Button _resumeButton;
        [SerializeField] private TextMeshProUGUI _reminderText;
        [SerializeField] private float _durationBlink = 0.8f;

        private Tween _blinkTween;

        public void Init()
        {
            _reminderText.alpha = 0f;
            _reminderText.text = _localizationService.Get("press_to_continue");
        }

        public void SetActive(bool isActive)
        {
            gameObject.SetActive(isActive);

            if (isActive)
                StartBlink();
            else
                StopBlink();
        }

        private void StartBlink()
        {
            var sequence = DOTween.Sequence();
            sequence.Append(_reminderText.DOFade(1f, _durationBlink).SetEase(Ease.InOutSine));
            sequence.Append(_reminderText.DOFade(0f, _durationBlink).SetEase(Ease.InOutSine));
            sequence.SetLoops(-1);
            _blinkTween = sequence;
        }

        private void StopBlink()
        {
            _blinkTween?.Kill();
            _reminderText.alpha = 0f;
        }

        public void Recume()
        {
            _fsmGameplay.ExitAndResume();
        }
    }
}
