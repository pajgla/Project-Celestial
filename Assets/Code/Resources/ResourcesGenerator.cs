using System.Collections.Generic;
using SolarSystem;
using SolarSystem.Configs;
using UnityEngine;

namespace Resources
{
    public static class ResourcesGenerator
    {
        public static void GeneratePlanetResources(PlanetConfig config, Planet planet)
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

            PlanetTypeResourceRarityConfig rarityConfig = resourcesConfig.GetPlanetTypeResourceRarityConfig();
            if (rarityConfig == null)
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
            List<ResourceRarityWrapper> resourcesRarityWrappers =
                rarityConfig.GetResourceRaritiesForPlanetType(planetType);
            if (resourcesRarityWrappers == null || resourcesRarityWrappers.Count == 0)
            {
                Debug.LogWarning(
                    $"No resource rarity wrappers found for planet type {planetType.ToString()}. Is this okay?");
                return;
            }

            ResourcesHolder resourcesHolder = planet.GetResourcesHolder();

            foreach (ResourceRarityWrapper resourceRarityWrapper in resourcesRarityWrappers)
            {
                EResourceType resourceType = resourceRarityWrapper.GetResourceType();
                EResourceRarity resourceRarity = resourceRarityWrapper.GetResourceRarity();
                ResourceSpawningConfig.ResourceAmountEntry resourceAmountEntry = resourceSpawningConfig.GetResourceAmountFor(resourceType, resourceRarity);
                if (resourceAmountEntry == null)
                {
                    continue;
                }
                
                float spawnAmount = resourceAmountEntry.m_AmountRange.GetRandomValueFromRange();
                float scaledAmount = ScaleResourceBasedOnDiameter(config, planet, spawnAmount);
                resourcesHolder.AddResource(resourceType, scaledAmount);
            }
        }

        public static void GenerateMoonResources(MoonConfig config, Moon moon)
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

            MoonTypeResourceRarityConfig rarityConfig = resourcesConfig.GetMoonTypeResourceRarityConfig();
            if (rarityConfig == null)
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

            EMoonType moonType = moon.GetMoonType();
            List<ResourceRarityWrapper> resourcesRarityWrappers =
                rarityConfig.GetResourceRaritiesForPlanetType(moonType);
            if (resourcesRarityWrappers == null || resourcesRarityWrappers.Count == 0)
            {
                Debug.LogWarning(
                    $"No resource rarity wrappers found for planet type {moonType.ToString()}. Is this okay?");
                return;
            }

            ResourcesHolder resourcesHolder = moon.GetResourcesHolder();

            foreach (ResourceRarityWrapper resourceRarityWrapper in resourcesRarityWrappers)
            {
                int chance = Random.Range(0, 101);
                if (rarityChancesConfig.IsChanceCheckPassed(resourceRarityWrapper.GetResourceRarity(), chance))
                {
                    EResourceType resourceType = resourceRarityWrapper.GetResourceType();
                    EResourceRarity resourceRarity = resourceRarityWrapper.GetResourceRarity();
                    ResourceSpawningConfig.ResourceAmountEntry resourceAmountEntry =
                        resourceSpawningConfig.GetResourceAmountFor(resourceType, resourceRarity);

                    if (resourceAmountEntry == null)
                    {
                        continue;
                    }
                    
                    float spawnAmount = resourceAmountEntry.m_AmountRange.GetRandomValueFromRange();
                    float scaledAmount = ScaleResourceBasedOnDiameter(config, moon, spawnAmount);
                    resourcesHolder.AddResource(resourceType, scaledAmount);
                }
            }
        }

        static float ScaleResourceBasedOnDiameter(CelestialObjectConfigBase config, CelestialObjectBase celestialObject, float originalSpawnAmount)
        {
            Core.Helpers.RangeValueFloat planetDiameterRange = config.GetObjectDiameterRange();
            float averageSize = (planetDiameterRange.GetMaxValue() + planetDiameterRange.GetMinValue()) / 2f;
            float scale = celestialObject.GetDiameter() / averageSize;
            return originalSpawnAmount * scale;
        }
    }
}