using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SolarSystem
{
    public static class PlanetFactory
    {
        public static Planet GenerateNewPlanet(PlanetConfig config, float orbitRadius)
        {
            Planet newPlanet = GameObject.Instantiate(config.GetPlanetPrefab());
            if (!ValidatePlanetPrefab(newPlanet))
            {
                GameObject.Destroy(newPlanet.gameObject);
                return null;
            }
            
            SetRandomAngleToStar(newPlanet);

            RandomizePlanetRadius(config, newPlanet, orbitRadius);
            
            newPlanet.GenerateMoons(config);
            newPlanet.CalculateRadiusWithMoons();
            
            EPlanetType planetType = DeterminePlanetType(newPlanet, config, orbitRadius);
            newPlanet.SetPlanetType(planetType);

            DeterminePlanetColor(newPlanet, config);
            
            
            return newPlanet;
        }

        private static void DeterminePlanetColor(Planet planet, PlanetConfig config)
        {
            Color color = config.GetCelestialObjectColorConfig().GetColorForType(planet.GetPlanetType());
            
            planet.GetComponent<SpriteRenderer>().color = color;
        }

        private static bool ValidatePlanetPrefab(Planet planet)
        {
            SpriteRenderer spriteRenderer = planet.GetComponent<SpriteRenderer>();
            if (spriteRenderer == null)
            {
                Debug.LogError("Planet Prefab doesn't have a SpriteRenderer!");
                return false;
            }

            return true;
        }

        private static void RandomizePlanetRadius(PlanetConfig config, Planet planet, float orbitRadius)
        {
            float planetDiameter = 0;
        
            ECelestialRegionType regionType = SolarSystemHelpers.GetCelestialRegionTypeForOrbitRadius(config.GetCelestialRegionConfig(), orbitRadius);
            float giantPlanetChance = Random.Range(0, 101);
            float requiredGiantPlanetChance = config.GetGiantPlanetChance();
            
            if (regionType != ECelestialRegionType.Inner && giantPlanetChance <= requiredGiantPlanetChance)
            {
                planetDiameter = config.GetGiantPlanetDiameterRange().GetRandomValueFromRange();
                planet.SetIsGiantPlanet(true);
                Debug.Log("Spawned a giant!");
            }
            else
            {
                planetDiameter = config.GetObjectDiameterRange().GetRandomValueFromRange();   
            }
        
            planet.SetDiameter(planetDiameter);
        }

        private static void SetRandomAngleToStar(Planet planet)
        {
            float randomAngle = Random.Range(0.0f, 360.0f);
            planet.SetCurrentAngleToParentObject(randomAngle);
        }

        private static EPlanetType DeterminePlanetType(Planet planet, PlanetConfig config, float distanceFromStar)
        {
            CelestialRegionConfig celestialRegionConfig = config.GetCelestialRegionConfig();
            if (celestialRegionConfig == null)
            {
                Debug.LogError("Planet config doesn't have a Celestial Region Config!");
                return EPlanetType.Rocky;
            }
            
            ECelestialRegionType regionType = SolarSystemHelpers.GetCelestialRegionTypeForOrbitRadius(celestialRegionConfig, distanceFromStar);
            if (planet.GetIsGiantPlanet())
            {
                if (regionType == ECelestialRegionType.Inner)
                {
                    Debug.LogError("Gas planet shouldn't spawn in inner region!");
                    return EPlanetType.Rocky;
                }
                else if (regionType == ECelestialRegionType.Middle)
                {
                    return EPlanetType.GasGiant;
                }
                else
                {
                    return EPlanetType.IceGiant;
                }
            }

            EPlanetType planetType;
            if (regionType == ECelestialRegionType.Inner)
            {
                planetType = RandomPlanetType(new EPlanetType[]
                {
                    EPlanetType.Desert,
                    EPlanetType.Volcanic,
                    EPlanetType.MetalRich,
                    EPlanetType.Desert
                });
            }
            else if (regionType == ECelestialRegionType.Middle)
            {
                planetType = RandomPlanetType(new EPlanetType[]
                {
                    EPlanetType.Terrestrial,
                    EPlanetType.Oceanic,
                    EPlanetType.Ice,
                    EPlanetType.Rocky,
                    EPlanetType.MetalRich,
                    EPlanetType.Desert
                });
            }
            else
            {
                planetType = RandomPlanetType(new EPlanetType[]
                {
                    EPlanetType.Ice,
                    EPlanetType.FrozenRock,
                    EPlanetType.Cryovolcanic
                });
            }

            return planetType;
        }

        private static EPlanetType RandomPlanetType(EPlanetType[] types)
        {
            int typesLength = types.Length;
            if (typesLength == 0)
            {
                Debug.LogError("Provided empty types array!");
                return EPlanetType.Rocky;
            }
            
            int index = Random.Range(0, typesLength);
            return types[index];
        }
    }
}

