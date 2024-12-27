using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CelestialObjects
{
    public class CelestialObjectConfigBase : ScriptableObject
    {
        [Header("Orbit")]
        [SerializeField] Material m_OrbitMaterial;
        [SerializeField] Color m_OrbitColor;
        [SerializeField] bool m_ShouldDrawOrbit = true;
        [SerializeField] float m_MinOrbitSpeed = 1f;
        [SerializeField] float m_MaxOrbitSpeed = 5f;
        [SerializeField] float m_OrbitDrawWidth = 0.02f;
        [SerializeField] int m_OrbitDrawPoints = 100;
        [SerializeField] bool m_IsMoveableOrbit = false;
        
        public virtual Color GetRandomColor(CelestialObjectBase celestialObject)
        {
            throw new System.NotImplementedException();
        }
        
        //Getters
        public Material GetOrbitMaterial() { return m_OrbitMaterial; }
        public Color GetOrbitColor() { return m_OrbitColor; }
        public bool GetShouldDrawOrbit() { return m_ShouldDrawOrbit; }
        public float GetMinOrbitSpeed() { return m_MinOrbitSpeed; }
        public float GetMaxOrbitSpeed() { return m_MaxOrbitSpeed; }
        public float GetOrbitDrawWidth() { return m_OrbitDrawWidth; }
        public int GetOrbitDrawPoints() { return m_OrbitDrawPoints; }
        public bool GetIsMoveableOrbit() { return m_IsMoveableOrbit; }
    } 
}

