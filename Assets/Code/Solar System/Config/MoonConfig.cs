using SolarSystem.Constants;
using UnityEngine;

namespace SolarSystem.Configs
{
    [CreateAssetMenu(fileName = "New Moon Config", menuName = SolarSystemConstants.S_CelestialConfigMenuName  + "Moon Config")]
    public class MoonConfig : CelestialObjectConfigBase
    {
        [Header("Display settings")]
        [SerializeField] Sprite[] m_AllowedSprites;
        [SerializeField] MoonColorConfig m_MoonColorConfig;
        
        [Header("Prefabs")]
        [SerializeField] Moon m_MoonPrefab;
        
        public override Color GetRandomColor(CelestialObjectBase celestialObject)
        {
            return Color.cyan;
        }
        
        //Getters
        public Sprite[] GetAllowedSprites() => m_AllowedSprites;
        public Moon GetMoonPrefab() => m_MoonPrefab;
        public MoonColorConfig GetMoonColorConfig() => m_MoonColorConfig;
    }
}

