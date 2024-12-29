using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

namespace CelestialObjects
{
    [CreateAssetMenu(fileName = "New Planet Config", menuName = "Config/Planet Config")]
    public class PlanetConfig : CelestialObjectConfigBase
    {
        [SerializeField] Sprite[] m_AllowedSprites;
        [SerializeField] Planet m_PlanetPrefab;

        [Header("Distance Settings")]
        [SerializeField] float m_CloseRangeDistance = 5f;
        [SerializeField] float m_MidRangeDistance = 10f;

        [Header("Moon Settings")]
        [SerializeField] Moon m_MoonPrefab;
        [SerializeField] MoonConfig m_MoonConfig;
        [SerializeField, Range(0.0f, 100.0f)] float m_MoonChance = 5f;
        [SerializeField, Range(0.0f, 100.0f)] float m_MultipleMoonChance = 1f;
        [SerializeField] int m_MaxMoons = 5;
        [SerializeField] Core.RangeValueFloat m_DistanceBetweenMoons;
        [SerializeField] Core.RangeValueFloat m_DistanceFromPlanetToFirstMoon;
        
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
        public float GetMoonChance() { return m_MoonChance; }
        public Moon GetMoonPrefab() { return m_MoonPrefab; }
        public float GetMultipleMoonChance() { return m_MultipleMoonChance; }
        public int GetMaxMoons() { return m_MaxMoons; }
        public MoonConfig GetMoonConfig() { return m_MoonConfig; }

        public Core.RangeValueFloat GetDistanceBetweenMoonsRange() { return m_DistanceBetweenMoons; }

        public Core.RangeValueFloat GetDistanceFromPlanetToFirstMoonRange() { return m_DistanceFromPlanetToFirstMoon; }
    }
}
