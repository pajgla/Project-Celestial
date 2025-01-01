using UnityEngine;
using UnityEngine.Serialization;

namespace SolarSystem.Configs
{
    [CreateAssetMenu(fileName = "New Solar System Config", menuName = "Config/Solar System Config")]
    public class SolarSystemConfig : ScriptableObject
    {
        [SerializeField] PlanetConfig m_PlanetConfig;

        [Header("Prefab references")]
        [SerializeField] SolarSystem m_SolarSystemPrefab;
        [SerializeField] Star m_StarPrefab;

        [Header("Planet configs")]
        [SerializeField] Core.Helpers.RangeValueInt m_PlanetCount;
        [SerializeField] Core.Helpers.RangeValueFloat m_DistanceBetweenPlanets;
        [SerializeField] Core.Helpers.RangeValueFloat m_FirstPlanetDistanceFromStar;
        [FormerlySerializedAs("m_MaxSolarSystemHalfOrbitDistance")] [SerializeField] float m_MaxSolarSystemRadius = 15f;
        
        [Header("Star configs")]
        [SerializeField] Sprite[] m_StarSprites;
        [SerializeField] Core.Helpers.RangeValueFloat m_StarRadius;
        
        [SerializeField] string[] m_SolarSystemNames;

        
        //Getters
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

        public Core.Helpers.RangeValueInt GetPlanetCount() { return m_PlanetCount; }

        public Core.Helpers.RangeValueFloat GetDistanceBetweenPlanetsRange() { return m_DistanceBetweenPlanets; }
        public float GetMaxSolarSystemRadius() { return m_MaxSolarSystemRadius; }

        public Core.Helpers.RangeValueFloat GetFirstPlanetDistanceFromStarRange() { return m_FirstPlanetDistanceFromStar; }
        public Core.Helpers.RangeValueFloat GetStarRadiusRange() { return m_StarRadius; }
    }
}
