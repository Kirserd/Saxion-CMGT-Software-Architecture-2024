using UnityEngine;

namespace MIRAI.Grid.Cell
{
    public class Tower : GridCellShell, IDamageable
    {
        private ITowerActor _actor;
        private TowerStats _stats;
        private TowerSpriteSet _spriteSet;

        public ITowerActor Actor => _actor;
        public TowerStats Stats => _stats;
        public TowerSpriteSet SpriteSet => _spriteSet;

        float IDamageable.MaxHP { get => _stats.MaxHP; }
        float IDamageable.MinHP { get => _stats.MinHP; }
        float IDamageable.HP
        {
            get => _stats.HP;
            set => _stats.HP = Mathf.RoundToInt(value);
        }

        public void SetParts(ITowerActor actor, TowerStats stats, TowerSpriteSet spriteSet)
        {
            _actor = actor;
            _stats = stats;
            _spriteSet = spriteSet;
        }
        public void InitSprite()
        {
            _renderer.sortingLayerName = "Tiles";
            _renderer.sortingOrder = -1;
            _renderer.color = Color.cyan;

            _spriteSet.SetSpriteOfLevel(_renderer, 0);
        }
        public bool LevelUp()
        {
            if (_stats.Level >= _stats.MaxLevel)
                return false;

            _stats.LevelUp();
            _spriteSet.SetSpriteOfLevel(_renderer, _stats.Level);

            return true;
        }
        private void TryAct()
        {
            return; // stop act

            GridCellShell[] selection = _actor.Selector.GetSeletion();

            if (selection is null)
                return;

            _actor.Act(_actor.Selector.GetSeletion());
        }

        protected virtual void FixedUpdate()
        {
            if (!_stats.IsReadyToAct())
                return;

            TryAct();
        }

        void IDamageable.OnCriticalHealth() => Death();
        void IDamageable.OnDamaged(float damageAmount) { }
        void IDamageable.OnHealed(float healAmount) { }

        protected virtual void Death() => GridRegistrar.OverwriteAt(X, Y, null);
   
    }
}