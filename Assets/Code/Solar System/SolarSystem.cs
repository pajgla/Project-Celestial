using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace CelestialObjects
{
    public class SolarSystem : MonoBehaviour
    {
        Star m_Star;
        List<GameObject> m_Planets = new List<GameObject>();
    
        public void Initialize(SolarSystemConfig config)
        {
            m_Star = SolarSystemHelpers.GenerateNewStar(config, transform);

            int planetsToCreate = config.GetPlanetCount().GetRandomValueFromRange();
            float distanceFromStar = (m_Star.GetDiameter() / 2.0f) + config.GetFirstPlanetDistanceFromStarRange().GetRandomValueFromRange();
            
            for (int i = 0; i < planetsToCreate; i++)
            {
                if (distanceFromStar > config.GetMaxSolarSystemHalfOrbitDistance())
                {
                    //We have broken the max distance from the star, so return
                    break;
                }

                Planet newPlanet = SolarSystemHelpers.GenerateNewPlanet(config.GetPlanetConfig());
                newPlanet.transform.parent = transform;
                float orbitRadius = distanceFromStar + newPlanet.GetRadiusWithMoons();
                newPlanet.Initialize(m_Star, config.GetPlanetConfig(), orbitRadius);
                distanceFromStar += newPlanet.GetRadiusWithMoons() * 2f + config.GetDistanceBetweenPlanetsRange().GetRandomValueFromRange();
                m_Planets.Add(newPlanet.gameObject);
            }
        }
    }
}