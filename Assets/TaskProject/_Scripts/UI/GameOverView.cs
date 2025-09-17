using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Game
{
    public class GameOverView : MonoBehaviour
    {
        [Inject] private FSMGameplay _fsmGameplay;
        [Inject] private LocalizationService _localizationService;

        [SerializeField] private Button _restartButton;
        [SerializeField] private TextMeshProUGUI _reminderText;
        [SerializeField] private float _durationBlink = 0.8f;

        public void Init()
        {
            _reminderText.alpha = 0f;
            _reminderText.text = _localizationService.Get("game_over");
        }

        public void Active()
        {
            gameObject.SetActive(true);
            StartBlink();
        }

        public void Restart()
        {
            _fsmGameplay.EnterIn(StateGameplay.Restart);
        }

        private void StartBlink()
        {
            var sequence = DOTween.Sequence();
            sequence.Append(_reminderText.DOFade(1f, _durationBlink).SetEase(Ease.InOutSine));
            sequence.Append(_reminderText.DOFade(0f, _durationBlink).SetEase(Ease.InOutSine));
            sequence.SetLoops(-1);
        }
    }
}
