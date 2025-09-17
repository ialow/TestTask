using UnityEngine;

namespace Game
{
    public class DefaultPlacementValidator : IPlacementValidator
    {
        public bool CanPlace(RectTransform newElement, Tower tower)
        {
            return tower.IsEmpty || (IsAboveLast(tower, newElement) && HasEnoughOverlap(tower, newElement));
        }

        private bool IsAboveLast(Tower tower, RectTransform newElement)
        {
            var last = tower.GetLast();
            var lastTop = last.anchoredPosition.y + last.rect.height * 0.5f;
            var newBottom = newElement.anchoredPosition.y - newElement.rect.height * 0.5f;
            return newBottom > lastTop;
        }

        private bool HasEnoughOverlap(Tower tower, RectTransform newElement)
        {
            var firstElement = tower.Elements[0];

            var lastLeft = firstElement.anchoredPosition.x - firstElement.rect.width * 0.5f;
            var lastRight = firstElement.anchoredPosition.x + firstElement.rect.width * 0.5f;
            var newLeft = newElement.anchoredPosition.x - newElement.rect.width * 0.5f;
            var newRight = newElement.anchoredPosition.x + newElement.rect.width * 0.5f;

            var overlap = Mathf.Min(lastRight, newRight) - Mathf.Max(lastLeft, newLeft);
            var minRequired = Mathf.Min(firstElement.rect.width, newElement.rect.width) * 0.5f;

            return overlap >= minRequired;
        }
    }
}
