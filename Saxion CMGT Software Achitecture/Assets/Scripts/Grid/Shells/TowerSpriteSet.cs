using System;
using UnityEngine;

namespace MIRAI.Grid.Cell
{
    [Serializable]
    public class TowerSpriteSet
    {
        [Header("Sprite order should correspond to tower levels")]
        [SerializeField]
        private Sprite[] _sprites;

        public void SetSpriteOfLevel(SpriteRenderer renderer, int level) 
            => renderer.sprite = _sprites[level];
    }
}