using UnityEngine;

namespace MIRAI.Grid.Cell
{
    public static class TowerBuilder
    {
        public static Tower BuildTower(TowerBlueprint blueprint)
        {
            var gameObject = new GameObject(blueprint.Tooltip.Name);
            var tower = gameObject.AddComponent<Tower>();

            ITargetSelector selector = blueprint.Rules.Mode switch
            {
                TowerBlueprint.TargetingRules.TargetingMode.Closest     => null, //TODO: new ClosestSelector(blueprint.Rules.Tags); 
                TowerBlueprint.TargetingRules.TargetingMode.LowestHP    => null, //TODO: new LowestHPSelector(blueprint.Rules.Tags);
                TowerBlueprint.TargetingRules.TargetingMode.AOE         => null, //TODO: new AOESelector(blueprint.Rules.Tags);
                _ => null,
            };

            ITowerActor actor = null;
            TowerStats stats = null;

            if (blueprint is OffensiveTowerBlueprint offensiveTowerBlueprint)
            {
                actor = null;   //TODO: new OffensiveTowerActor(selector); 
                stats = new OffensiveTowerStats(offensiveTowerBlueprint.Stats);
            }
            else if (blueprint is GatheringTowerBlueprint gatheringTowerBlueprint) 
            { 
                actor = null;   //TODO: new GatheringTowerActor(selector);
                stats = new GatheringTowerStats(gatheringTowerBlueprint.Stats);
            }

            tower.SetParts(actor, stats, blueprint.SpriteSet);
            tower.SetTooltip(blueprint.Tooltip);
            tower.InitSprite();

            return tower;
        }
    }
}