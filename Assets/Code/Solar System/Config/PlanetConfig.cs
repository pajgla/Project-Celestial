using SolarSystem.Constants;
using UnityEngine;
using UnityEngine.Serialization;

namespace SolarSystem.Configs
{
    [CreateAssetMenu(fileName = "New Planet Config", menuName = SolarSystemConstants.S_CelestialConfigMenuName + "Planet Config")]
    public class PlanetConfig : CelestialObjectConfigBase
    {
        [Header("Celestial Region Settings")]
        [SerializeField] CelestialRegionConfig m_CelestialRegionConfig;
        
        [Header("Planet Color Settings")]
        [SerializeField] Configs.CelestialObjectColorConfig m_CelestialObjectColorConfig;
        
        [SerializeField] Sprite[] m_AllowedSprites;
        [SerializeField] Planet m_PlanetPrefab;

        [Header("Distance Settings")]
        [SerializeField] float m_CloseRangeDistance = 5f;
        [SerializeField] float m_MidRangeDistance = 10f;

        [Header("Giant Planet Settings")]
        [SerializeField, Range(0, 100)] int m_GiantPlanetChance;
        [FormerlySerializedAs("m_GiantPlanetRadius")] [SerializeField] Core.Helpers.RangeValueFloat m_GiantPlanetDiameter;

        [Header("Moon Settings")]
        [SerializeField] MoonConfig m_MoonConfig;
        [SerializeField, Range(0, 100)] int m_MoonChance = 5;
        [SerializeField, Range(0, 100)] int m_MultipleMoonChance = 1;
        [SerializeField] int m_MaxMoons = 5;
        [SerializeField] Core.Helpers.RangeValueFloat m_DistanceBetweenMoons;
        [SerializeField] Core.Helpers.RangeValueFloat m_DistanceFromPlanetToFirstMoon;

        //Getters
        public Sprite[] GetAllowedSprites() { return m_AllowedSprites; }
        public float GetCloseRangeDistance() { return m_CloseRangeDistance; }
        public float GetMidRangeDistance() { return m_MidRangeDistance; }
        public Planet GetPlanetPrefab() { return m_PlanetPrefab; }
        public int GetMoonChance() { return m_MoonChance; }
        public int GetMultipleMoonChance() { return m_MultipleMoonChance; }
        public int GetMaxMoons() { return m_MaxMoons; }
        public MoonConfig GetMoonConfig() { return m_MoonConfig; }

        public Core.Helpers.RangeValueFloat GetDistanceBetweenMoonsRange() => m_DistanceBetweenMoons;

        public Core.Helpers.RangeValueFloat GetDistanceFromPlanetToFirstMoonRange() => m_DistanceFromPlanetToFirstMoon;
        public int GetGiantPlanetChance() => m_GiantPlanetChance;
        public Core.Helpers.RangeValueFloat GetGiantPlanetDiameterRange() => m_GiantPlanetDiameter;
        public CelestialRegionConfig GetCelestialRegionConfig() => m_CelestialRegionConfig;
        public Configs.CelestialObjectColorConfig GetCelestialObjectColorConfig() => m_CelestialObjectColorConfig;
    }
}
