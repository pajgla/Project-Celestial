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
            m_Star = GameObject.Instantiate(config.GetStarPrefab(), transform.position, Quaternion.identity, transform);
            //#TODO Pick random sprite
            m_Star.GetComponent<SpriteRenderer>().sprite = config.GetStarSprites()[0];
        
            int planetsToCreate = Random.Range(config.GetMinPlanetsCount(), config.GetMaxPlanetsCount() + 1);
            float distanceFromStar = 1;
            for (int i = 0; i < planetsToCreate; i++)
            {
                if (distanceFromStar > config.GetMaxDistanceFromStar())
                {
                    break;
                }

                Planet newPlanet = SolarSystemHelpers.GenerateNewPlanet(config.GetPlanetConfig());
                newPlanet.transform.parent = transform;
                newPlanet.Initialize(m_Star, config.GetPlanetConfig(), distanceFromStar);
                distanceFromStar += Random.Range(config.GetMinDistanceBetweenPlanets(), config.GetMaxDistanceBetweenPlanets());
                m_Planets.Add(newPlanet.gameObject);
            }
        }
    }
}