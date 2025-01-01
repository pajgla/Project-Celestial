using UnityEngine;
using SolarSystem.Constants;
using UnityEngine.Serialization;

namespace SolarSystem.Configs
{
    [CreateAssetMenu(fileName = "New Celestial Region Config", menuName = SolarSystemConstants.S_CelestialConfigMenuName + "Celestial Region Config")]
    public class CelestialRegionConfig : ScriptableObject
    {
        [FormerlySerializedAs("m_InnerRegionEnd")] [SerializeField] float m_InnerRegionRadius;
        [FormerlySerializedAs("m_MiddleRegionEnd")] [SerializeField] float m_MiddleRegionRadius;
        //Outer is anything greater than the middle region
        
        public float GetInnerRegionRadius() { return m_InnerRegionRadius; }
        public float GetMiddleRegionRadius() { return m_MiddleRegionRadius; }
    }
}

