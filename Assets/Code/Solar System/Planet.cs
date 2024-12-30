using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace SolarSystem
{
    public class Planet : CelestialObjectBase
    {
        List<Moon> m_Moons = new List<Moon>();
        
        [SerializeField] EPlanetType m_PlanetType;
        bool m_IsGiantPlanet = false;
        
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
            
            base.Initialize(parentObject, config, orbitRadius);
        }

        public void GenerateMoons(PlanetConfig config)
        {
            int spawnChance = Random.Range(0, 101);
            int requiredChance = config.GetMoonChance();
            if (spawnChance > requiredChance)
            {
                return;
            }

            int moonsToSpawn = 1;
            int multipleMoonsChance = Random.Range(0, 101);
            if (multipleMoonsChance <= config.GetMultipleMoonChance())
            {
                moonsToSpawn = Random.Range(2, config.GetMaxMoons() + 1);
            }

            float radius = GetDiameter() / 2.0f + config.GetDistanceFromPlanetToFirstMoonRange().GetRandomValueFromRange();
            for (int i = 0; i < moonsToSpawn; i++)
            {
                Moon newMoon = SolarSystemHelpers.GenerateNewMoon(config.GetMoonConfig());

                float moonRadius = newMoon.GetDiameter() / 2.0f;
                radius += moonRadius + config.GetDistanceBetweenMoonsRange().GetRandomValueFromRange();
                newMoon.Initialize(this, config.GetMoonConfig(), radius);

                radius += moonRadius;
                
                m_Moons.Add(newMoon);
            }
        }

        public void CalculateRadiusWithMoons()
        {
            float radius = 0;
            if (HasMoons())
            {
                Moon lastMoon = m_Moons.Last();
                radius = lastMoon.GetOrbitRadius() + lastMoon.GetDiameter() / 2.0f;
            }
            else
            {
                radius = GetDiameter() / 2.0f;
            }
            
            GetRadiusWithMoons(radius);
        }

        public bool HasMoons()
        {
            return m_Moons.Count > 0;
        }
        
        //Getters
        public bool GetIsGiantPlanet() { return m_IsGiantPlanet; }
        public void SetIsGiantPlanet(bool value) { m_IsGiantPlanet = value; }
        public EPlanetType GetPlanetType() { return m_PlanetType; }
        public void SetPlanetType(EPlanetType value) { m_PlanetType = value; }
    }
}

