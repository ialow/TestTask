using UnityEngine;

namespace Game
{
    [System.Serializable]
    public struct LocalizedText
    {
        [field: SerializeField] public string Key { get; private set; }
        [field: SerializeField] public string Russian { get; private set; }
        [field: SerializeField] public string English { get; private set; }
    }
}
