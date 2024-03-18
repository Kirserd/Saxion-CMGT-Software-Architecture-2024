using UnityEngine;

namespace MIRAI.Grid.Cell.TowerActors
{
    public class BasicGatheringActor : ITowerActor
    {
        public ITargetSelector Selector { get; set; }
        public TowerStats Stats 
        { 
            get => _stats; 
            set { if (value is GatheringTowerStats stats) _stats = stats; } 
        }
        private GatheringTowerStats _stats;

        private float _actCooldown;

        public virtual void Act(GridCellShell[] selection)
        {
            if(_actCooldown > 0)
                _actCooldown -= Time.deltaTime;
            if (_actCooldown > 0)
                return;
            
            Level.Instance.Resources.AddMoney(Mathf.RoundToInt(_stats.GAIN.Final * 5f));
            _actCooldown = 5f / _stats.SPD.Final;
        }
    }
}