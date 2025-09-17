using UnityEngine;

namespace Game
{
    public struct CubeData
    {
        public readonly float Height;
        public readonly float Width;
        public readonly Sprite Sprite;

        public CubeData(float height, float width, Sprite sprite)
        {
            Height = height;
            Width = width;
            Sprite = sprite;
        }
    }
}
