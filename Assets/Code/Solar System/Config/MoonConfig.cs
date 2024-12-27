using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CelestialObjects
{
    [CreateAssetMenu(fileName = "New Moon Config", menuName = "Config/Moon Config")]
    public class MoonConfig : CelestialObjectConfigBase
    {
        [SerializeField] Sprite[] m_AllowedSprites;
        
        public override Color GetRandomColor(CelestialObjectBase celestialObject)
        {
            return Color.cyan;
        }
        
        //Getters
        public Sprite[] GetAllowedSprites() { return m_AllowedSprites; }
    }
}

