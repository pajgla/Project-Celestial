using System;
using System.Collections.Generic;
using SolarSystem.Constants;
using UnityEngine;

namespace SolarSystem.Configs
{
    [Serializable]
    struct PlanetTypeToColorWrapper
    {
        public EPlanetType m_EPlanetType;
        public Color m_Color;
    }
    
    [CreateAssetMenu(fileName = "New Celestial Object Color Config", menuName = SolarSystemConstants.S_CelestialConfigMenuName + "New Celestial Object Color Config")]
    public class CelestialObjectColorConfig : ScriptableObject
    {
        [SerializeField] List<PlanetTypeToColorWrapper> m_PlanetTypeToColorWrappers;

        public Color GetColorForType(EPlanetType planetType)
        {
            foreach (PlanetTypeToColorWrapper wrapper in m_PlanetTypeToColorWrappers)
            {
                if (wrapper.m_EPlanetType == planetType)
                {
                    return wrapper.m_Color;
                }
            }
            
            Debug.LogError("Wrapper for planet type " + planetType.ToString() + " is missing!");
            return Color.white;
        }
    }
}

