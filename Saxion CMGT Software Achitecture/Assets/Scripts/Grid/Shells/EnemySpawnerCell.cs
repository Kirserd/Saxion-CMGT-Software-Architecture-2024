using UnityEngine;

namespace MIRAI.Grid.Cell
{
    public class EnemySpawnerCell : OpenCell
    {
        public Transform EnemiesParent 
        {
            get
            {
                if (_enemiesParent is null)
                    _enemiesParent = GameObject.FindGameObjectWithTag("Enemies").transform;

                return _enemiesParent;
            }
        }
        private Transform _enemiesParent;

        private void Start() 
            => Level.Instance.WaveManager.OnWaveEvent += OnWaveEventHandle;

        public override void Unsubscribe()
        {
            base.Unsubscribe();
            Level.Instance.WaveManager.OnWaveEvent -= OnWaveEventHandle;
        }

        private void OnWaveEventHandle(LevelWaveData.WaveEvent.Wave wave)
        {
            CreaturePool.Instantiate(wave.Creature, transform.position, Quaternion.identity, EnemiesParent);
        }
    }
}