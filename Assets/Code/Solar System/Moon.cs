using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace SolarSystem
{
    public class Moon : CelestialObjectBase
    {
        public override void Initialize(CelestialObjectBase parentObject, CelestialObjectConfigBase config, float orbitRadius)
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
    }
}