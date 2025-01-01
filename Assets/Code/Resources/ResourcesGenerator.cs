using System.Collections.Generic;
using SolarSystem;
using UnityEngine;

namespace Resources
{
    public static class ResourcesGenerator
    {
        public static void GenerateResources(CelestialObjectConfigBase config, Planet planet)
        {
            if (config == null)
            {
                Debug.LogError("Null config provided");
                return;
            }

            ResourcesConfig resourcesConfig = config.GetResourcesConfig();
            if (resourcesConfig == null)
            {
                //Some celestial objects (like a star) do not need to have resources config, so we can safely return
                return;
            }
            
            PlanetTypeResourceRarityConfig planetTypeResourceRarityConfig = resourcesConfig.GetPlanetTypeResourceRarityConfig();
            if (planetTypeResourceRarityConfig == null)
            {
                Debug.LogError("PlanetTypeResourceRarityConfig is null");
                return;
            }
            
            RarityChancesConfig rarityChancesConfig = resourcesConfig.GetRarityChancesConfig();
            if (rarityChancesConfig == null)
            {
                Debug.LogError("RarityChancesConfig is null");
                return;
            }
            
            ResourceSpawningConfig resourceSpawningConfig = resourcesConfig.GetResourceSpawningConfig();
            if (resourceSpawningConfig == null)
            {
                Debug.LogError("ResourceSpawningConfig is null");
                return;
            }
            
            EPlanetType planetType = planet.GetPlanetType();
            List<ResourceRarityWrapper> resourcesRarityWrappers = planetTypeResourceRarityConfig.GetResourceRaritiesForPlanetType(planetType);
            if (resourcesRarityWrappers == null || resourcesRarityWrappers.Count == 0)
            {
                Debug.LogWarning($"No resource rarity wrappers found for planet type {planetType.ToString()}. Is this okay?");
                return;
            }

            ResourcesHolder resourcesHolder = planet.GetResourcesHolder();
            
            foreach (ResourceRarityWrapper resourceRarityWrapper in resourcesRarityWrappers)
            {
                int chance = Random.Range(0, 101);
                if (rarityChancesConfig.IsChanceCheckPassed(resourceRarityWrapper.GetResourceRarity(), chance))
                {
                    EResourceType resourceType = resourceRarityWrapper.GetResourceType();
                    EResourceRarity resourceRarity = resourceRarityWrapper.GetResourceRarity();
                    ResourceSpawningConfig.ResourceAmountEntry resourceAmountEntry = resourceSpawningConfig.GetResourceAmountFor(resourceType, resourceRarity);

                    if (resourceAmountEntry == null)
                    {
                        continue;
                    }
                    
                    //#TODO Implement celestial object radius multiplier
                    float spawnAmount = resourceAmountEntry.m_AmountRange.GetRandomValueFromRange();
                    resourcesHolder.AddResource(resourceType, spawnAmount);
                }
            }
        }
    }
}