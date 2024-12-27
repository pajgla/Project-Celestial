using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CelestialObjects
{
    [CreateAssetMenu(fileName = "New Solar System Config", menuName = "Config/Solar System Config")]
    public class SolarSystemConfig : ScriptableObject
    {
        [SerializeField] PlanetConfig m_PlanetConfig;

        [SerializeField] SolarSystem m_SolarSystemPrefab;
        [SerializeField] Star m_StarPrefab;

        [SerializeField] string[] m_SolarSystemNames;
        [SerializeField] Sprite[] m_StarSprites;
        [SerializeField] int m_MinPlanetsCount = 1;
        [SerializeField] int m_MaxPlanetsCount = 10;
        [SerializeField] float m_MinDistanceBetweenPlanets = 0.2f;
        [SerializeField] float m_MaxDistanceBetweenPlanets = 4f;
        [SerializeField] float m_MaxDistanceFromStar = 15f;

        public PlanetConfig GetPlanetConfig()
        {
            return m_PlanetConfig;
        }

        public SolarSystem GetSolarSystemPrefab()
        {
            return m_SolarSystemPrefab;
        }

        public Star GetStarPrefab()
        {
            return m_StarPrefab;
        }

        public string[] GetSolarSystemNames()
        {
            return m_SolarSystemNames;
        }

        public Sprite[] GetStarSprites()
        {
            return m_StarSprites;
        }

        public int GetMinPlanetsCount()
        {
            return m_MinPlanetsCount;
        }

        public int GetMaxPlanetsCount()
        {
            return m_MaxPlanetsCount;
        }

        public float GetMinDistanceBetweenPlanets()
        {
            return m_MinDistanceBetweenPlanets;
        }

        public float GetMaxDistanceBetweenPlanets()
        {
            return m_MaxDistanceBetweenPlanets;
        }

        public float GetMaxDistanceFromStar()
        {
            return m_MaxDistanceFromStar;
        }
    }
}
