using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

namespace SolarSystem
{
    [CreateAssetMenu(fileName = "New Planet Config", menuName = "Config/Planet Config")]
    public class PlanetConfig : CelestialObjectConfigBase
    {
        [Header("Celestial Region Settings")]
        [SerializeField] CelestialRegionConfig m_CelestialRegionConfig;
        
        [Header("Planet Color Settings")]
        [SerializeField] CelestialObjectColorConfig m_CelestialObjectColorConfig;
        
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
        
        [Header("Color Settings")]
        [SerializeField] Color[] m_CloseRangePlanetColors;
        [SerializeField] Color[] m_MidRangePlanetColors;
        [SerializeField] Color[] m_FarRangePlanetColors;


        public override Color GetRandomColor(CelestialObjectBase celestialObject)
        {
            if (celestialObject.GetType() != typeof(Planet))
            {
                Debug.LogError("PlanetConfig should only be used with Planet objects");
                return Color.white;
            }
            
            float orbitRadius = celestialObject.GetOrbitRadius();
            Color[] allowedColors;
            if (orbitRadius <= GetCloseRangeDistance())
            {
                allowedColors = GetCloseRangePlanetColors();
            }
            else if (orbitRadius <= GetMidRangeDistance())
            {
                allowedColors = GetMidRangePlanetColors();
            }
            else
            {
                allowedColors = GetFarRangePlanetColors();
            }
        
            return allowedColors[Random.Range(0, allowedColors.Length)];
        }

        //Getters
        public Sprite[] GetAllowedSprites() { return m_AllowedSprites; }
        public float GetCloseRangeDistance() { return m_CloseRangeDistance; }
        public float GetMidRangeDistance() { return m_MidRangeDistance; }
        public Color[] GetCloseRangePlanetColors() { return m_CloseRangePlanetColors; }
        public Color[] GetMidRangePlanetColors() { return m_MidRangePlanetColors; }
        public Color[] GetFarRangePlanetColors() { return m_FarRangePlanetColors; }
        public Planet GetPlanetPrefab() { return m_PlanetPrefab; }
        public int GetMoonChance() { return m_MoonChance; }
        public int GetMultipleMoonChance() { return m_MultipleMoonChance; }
        public int GetMaxMoons() { return m_MaxMoons; }
        public MoonConfig GetMoonConfig() { return m_MoonConfig; }

        public Core.Helpers.RangeValueFloat GetDistanceBetweenMoonsRange() { return m_DistanceBetweenMoons; }

        public Core.Helpers.RangeValueFloat GetDistanceFromPlanetToFirstMoonRange() { return m_DistanceFromPlanetToFirstMoon; }
        public int GetGiantPlanetChance() { return m_GiantPlanetChance; }
        public Core.Helpers.RangeValueFloat GetGiantPlanetDiameterRange() { return m_GiantPlanetDiameter; }
        public CelestialRegionConfig GetCelestialRegionConfig() { return m_CelestialRegionConfig; }
        public CelestialObjectColorConfig GetCelestialObjectColorConfig() { return m_CelestialObjectColorConfig; }
    }
}
