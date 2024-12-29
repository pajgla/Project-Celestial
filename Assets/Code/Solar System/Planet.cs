using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace CelestialObjects
{
    public class Planet : CelestialObjectBase
    {
        List<Moon> m_Moons = new List<Moon>();
        
        public override void Initialize(CelestialObjectBase parentObject, CelestialObjectConfigBase config, float orbitRadius)
        {
            if (config.GetType() != typeof(PlanetConfig))
            {
                Debug.LogError("Config of type 'PlanetConfig' is required for planet initialization!");
                return;
            }
            
            PlanetConfig planetConfig = (PlanetConfig)config;

            //#TODO
            GetComponent<SpriteRenderer>().sprite = planetConfig.GetAllowedSprites()[0];
            
            GenerateMoons(planetConfig);
            base.Initialize(parentObject, config, orbitRadius + (GetTotalRadius() / 2.0f));
        }

        private void GenerateMoons(PlanetConfig config)
        {
            float spawnChance = Random.Range(0.0f, 100.0f);
            float requiredChance = config.GetMoonChance();
            if (spawnChance > requiredChance)
            {
                return;
            }

            int moonsToSpawn = 1;
            float multipleMoonsChance = Random.Range(0.0f, 100.0f);
            if (multipleMoonsChance <= config.GetMultipleMoonChance())
            {
                moonsToSpawn = Random.Range(2, config.GetMaxMoons() + 1);
            }

            float radius = (GetRadius() / 2.0f) + config.GetDistanceFromPlanetToFirstMoonRange().GetRandomValueFromRange();
            for (int i = 0; i < moonsToSpawn; i++)
            {
                Moon newMoon = Instantiate(config.GetMoonPrefab(), transform);
                newMoon.Initialize(this, config.GetMoonConfig(), radius);
                radius += (newMoon.GetRadius() / 2.0f) + config.GetDistanceBetweenMoonsRange().GetRandomValueFromRange();
                
                m_Moons.Add(newMoon);
            }
            
            Moon lastSpawnedMoon = m_Moons.Last();
            float totalRadius = (lastSpawnedMoon.GetOrbitRadius() * 2) + lastSpawnedMoon.GetRadius();
            SetTotalRadius(totalRadius);
        }
    }
}

