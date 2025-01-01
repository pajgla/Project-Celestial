using System.Collections.Generic;
using SolarSystem.Constants;
using UnityEngine;

namespace SolarSystem.Configs
{
    [CreateAssetMenu(fileName = "New Moon Color Config", menuName = SolarSystemConstants.S_CelestialConfigMenuName + "Moon Color Config")]
    public class MoonColorConfig : ScriptableObject
    {
        [System.Serializable]
        class MoonTypeColorWrapper
        {
            public EMoonType m_MoonType;
            public Color m_MoonColor;
        }
        
        [SerializeField] List<MoonTypeColorWrapper> m_MoonTypeColors;

        public Color GetMoonColor(EMoonType moonType)
        {
            foreach (MoonTypeColorWrapper wrapper in m_MoonTypeColors)
            {
                if (wrapper.m_MoonType == moonType)
                {
                    return wrapper.m_MoonColor;
                }
            }
            
            Debug.LogError($"Moon type {moonType.ToString()} is missing from Moon Color Config");
            return Color.white;
        }
    }
}