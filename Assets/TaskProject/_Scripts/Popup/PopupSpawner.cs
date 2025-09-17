using UnityEngine;

namespace Game
{
    public class PopupSpawner : MonoBehaviour
    {
        [SerializeField] private Transform _canvasRoot;
        [SerializeField] private RectTransform _startAnchor;
        [SerializeField] private float _moveUpOffset = 80f;
        [SerializeField] private Popup _popupPrefab;

        public void ShowPopup(string message)
        {
            var popup = Instantiate(_popupPrefab, _startAnchor.position, _startAnchor.rotation, _canvasRoot);
            popup.Show(message, popup.AnchoredPosition.y + _moveUpOffset);
        }
    }
}
