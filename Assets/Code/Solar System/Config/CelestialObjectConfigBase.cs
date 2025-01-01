using Resources;
using UnityEngine;
using UnityEngine.Serialization;

namespace SolarSystem.Configs
{
    public class CelestialObjectConfigBase : ScriptableObject
    {
        [Header("Orbit")]
        [SerializeField] Material m_OrbitMaterial;
        [SerializeField] Color m_OrbitColor;
        [SerializeField] bool m_ShouldDrawOrbit = true;
        [SerializeField] Core.Helpers.RangeValueFloat m_OrbitSpeed;
        [SerializeField] float m_OrbitDrawWidth = 0.02f;
        [SerializeField] int m_OrbitDrawPoints = 100;
        [SerializeField] bool m_IsMoveableOrbit = false;

        
        [Header("Radius")] 
        [FormerlySerializedAs("m_ObjectRadius"), SerializeField] Core.Helpers.RangeValueFloat m_ObjectDiameter;
        
        [Header("Resource Settings")]
        [SerializeField] ResourcesConfig m_ResourcesConfig;
        
        public virtual Color GetRandomColor(CelestialObjectBase celestialObject)
        {
            throw new System.NotImplementedException();
        }
        
        //Getters
        public Core.Helpers.RangeValueFloat GetObjectDiameterRange() { return m_ObjectDiameter; }
        public Material GetOrbitMaterial() { return m_OrbitMaterial; }
        public Color GetOrbitColor() { return m_OrbitColor; }
        public bool GetShouldDrawOrbit() { return m_ShouldDrawOrbit; }

        public Core.Helpers.RangeValueFloat GetOrbitSpeedRange() { return m_OrbitSpeed; }

        public float GetOrbitDrawWidth() { return m_OrbitDrawWidth; }
        public int GetOrbitDrawPoints() { return m_OrbitDrawPoints; }
        public bool GetIsMoveableOrbit() { return m_IsMoveableOrbit; }
        public ResourcesConfig GetResourcesConfig() => m_ResourcesConfig;
    } 
}

