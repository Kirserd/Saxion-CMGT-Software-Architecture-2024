using System;
using System.Collections.Generic;
using UnityEngine;

namespace MIRAI.Grid.Cell
{
    public static class CreaturePool
    {
        private static Dictionary<Type, Queue<Creature>> _pools = new();
        public static Creature Instantiate(Creature creatureArchetype, Vector3 position = new(), Quaternion rotation = new(), Transform parent = null)
        {
            Type creatureType = creatureArchetype.GetType();

            if (!_pools.ContainsKey(creatureType))
                AddPool(creatureType);

            var properPool = _pools[creatureType];

            if (properPool.Count <= 1)
                Scale(ref properPool, creatureArchetype);
            
            Creature creature = properPool.Dequeue();

            creature.gameObject.SetActive(true);

            if (parent is not null)
               creature.transform.SetParent(parent);

            creature.transform.position = position;
            creature.transform.rotation = rotation;

            return creature;
        }
        public static void AddPool(Type creatureType)
        {
            if (!creatureType.IsAssignableFrom(typeof(Creature)))
                return;

            _pools.Add(creatureType, new Queue<Creature>());        
        }
        public static void Scale(ref Queue<Creature> pool, Creature prefab)
        {
            pool.Enqueue(UnityEngine.Object.Instantiate(prefab, prefab.transform.position, prefab.transform.rotation));
            pool.Enqueue(UnityEngine.Object.Instantiate(prefab, prefab.transform.position, prefab.transform.rotation));
        }
        public static void Destroy(Creature creature, Type asType)
        {
            var properPool = _pools[asType];

            properPool.Enqueue(creature);
            creature.gameObject.SetActive(false);
        }
    }
}