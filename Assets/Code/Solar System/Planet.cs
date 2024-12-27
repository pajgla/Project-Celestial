using System.Collections.Generic;
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
            base.Initialize(parentObject, config, orbitRadius);

            //#TODO
            GetComponent<SpriteRenderer>().sprite = planetConfig.GetAllowedSprites()[0];
            
            GenerateMoons(planetConfig);
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

            float radius = config.GetMinDistanceFromMoonToPlanet();
            for (int i = 0; i < moonsToSpawn; i++)
            {
                Moon newMoon = Instantiate(config.GetMoonPrefab());
                newMoon.Initialize(this, config.GetMoonConfig(), radius);

                radius += config.GetDistanceBetweenMoons();
                
                m_Moons.Add(newMoon);
                print("Moon spawned");
            }
        }
    }
}

