using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    public class Tower
    {
        private List<RectTransform> _elements = new List<RectTransform>();
        private IPlacementValidator _placementValidator;

        public IReadOnlyList<RectTransform> Elements => _elements;
        public int CountElements => _elements.Count;
        public bool IsEmpty => _elements.Count == 0;

        public Tower(IPlacementValidator placementValidator)
        {
            _placementValidator = placementValidator;
        }

        public int IndexOfElement(RectTransform rect)
        {
            return _elements.IndexOf(rect);
        }

        public void Add(RectTransform element)
        {
            _elements.Add(element);
        }

        public void RemoveAt(int index)
        {
            _elements.RemoveAt(index);
        }

        public float GetTopAnchorPositionY(int indexPoligon)
        {
            var positionY = _elements[0].anchoredPosition.y + _elements[0].rect.height * 0.5f;

            for (int i = 1; i <= indexPoligon; i++)
            {
                var poligon = _elements[i].GetComponent<Cube>();
                positionY += poligon.Data.Height;
            }

            return positionY;
        }

        public RectTransform GetLast()
        {
            if (_elements.Count == 0) return default;
            return _elements[_elements.Count - 1];
        }

        public Vector2 GetTargetPosition(RectTransform newElement)
        {
            var previousElement = _elements[_elements.Count - 2];
            return new Vector2(newElement.anchoredPosition.x, previousElement.anchoredPosition.y + previousElement.rect.height);
        }

        public bool CanPlace(RectTransform newElement)
        {
            return _placementValidator.CanPlace(newElement, this);
        }
    }
}
