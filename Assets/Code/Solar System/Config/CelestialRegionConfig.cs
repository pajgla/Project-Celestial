using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Core;
using UnityEngine.Serialization;

namespace SolarSystem
{
    [CreateAssetMenu(fileName = "New Celestial Region Config", menuName = "Config/Celestial Region Config")]
    public class CelestialRegionConfig : ScriptableObject
    {
        [FormerlySerializedAs("m_InnerRegionEnd")] [SerializeField] float m_InnerRegionRadius;
        [FormerlySerializedAs("m_MiddleRegionEnd")] [SerializeField] float m_MiddleRegionRadius;
        //Outer is anything greater than the middle region
        
        public float GetInnerRegionRadius() { return m_InnerRegionRadius; }
        public float GetMiddleRegionRadius() { return m_MiddleRegionRadius; }
    }
}

