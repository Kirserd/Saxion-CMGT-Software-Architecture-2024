using System.Collections.Generic;
using System;
using System.Linq;
using UnityEngine;

namespace MIRAI.Grid.Cell
{
    [Serializable]
    public class Stat
    {
        [SerializeField]
        public float _onLevelUpGrowth;

        [SerializeField]
        private float _base;
        private float _final;
        private bool _isDirty;

        private List<Modifier> _modifiers = new();

        public Stat(Stat other)
        {
            _onLevelUpGrowth = other._onLevelUpGrowth;
            _final = other._final;

            Base = other._base;
        }

        public float Base
        {
            get => _base;
            set
            {
                _base = value;
                _isDirty = true;
            }
        }
        public float Final
        {
            get
            {
                if (_isDirty)
                    CalculateFinalValue();

                return _final;
            }
        }
        public bool IsDirty => _isDirty;

        public void LevelUp()
            => Base += _onLevelUpGrowth;

        public void AddModifier(ModifierType type, float amount)
        {
            _modifiers.Add(new Modifier(type, amount));
            _isDirty = true;
        }
        public void RemoveModifier(ModifierType type, float amount)
        {
            _modifiers.RemoveAll(modifier => modifier.Type == type && Math.Abs(modifier.Amount - amount) < float.Epsilon);
            _isDirty = true;
        }

        private void CalculateFinalValue()
        {
            var sortedModifiers = _modifiers.OrderBy(modifier => modifier.Type);

            _final = _base;

            foreach (var modifier in sortedModifiers)
            {
                switch (modifier.Type)
                {
                    case ModifierType.Multiplication:
                        _final *= modifier.Amount;
                        break;
                    case ModifierType.Addition:
                        _final += modifier.Amount;
                        break;
                }
            }

            _isDirty = false;
        }
    }
}