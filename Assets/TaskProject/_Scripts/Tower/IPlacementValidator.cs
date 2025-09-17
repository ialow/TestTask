using UnityEngine;

namespace Game
{
    public interface IPlacementValidator
    {
        bool CanPlace(RectTransform newElement, Tower tower);
    }
}
