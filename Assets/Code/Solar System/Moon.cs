using SolarSystem.Configs;
using UnityEngine;

namespace SolarSystem
{
    public class Moon : ResourceHoldingCelestialObject
    {
        EMoonType m_MoonType;
        
        public override void Initialize(CelestialObjectBase parentObject, Configs.CelestialObjectConfigBase config, float orbitRadius)
        {
            if (config.GetType() != typeof(MoonConfig))
            {
                Debug.LogError("Config of type 'MoonConfig' is required for moon initialization!");
                return;
            }
            
            base.Initialize(parentObject, config, orbitRadius);
            
            MoonConfig moonConfig = (MoonConfig)config;
            GetComponent<SpriteRenderer>().sprite = moonConfig.GetAllowedSprites()[0];
        }
        
        //Getters & Setters
        public EMoonType GetMoonType() => m_MoonType;
        public void SetMoonType(EMoonType moonType) => m_MoonType = moonType;
    }
}