using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    [CreateAssetMenu(fileName = "CubeConfig", menuName = "Configs/CubeConfig")]
    public class CubeConfig : ScriptableObject
    {
        [SerializeField] private List<Sprite> _cubeSprites;

        public int Size => _cubeSprites.Count;

        public Sprite CubeSprite(int index)
        {
            return _cubeSprites[index];
        }
    }
}
