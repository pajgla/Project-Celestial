using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CelestialObjects
{
    public class SolarSystemGenerator : MonoBehaviour
    {
        [SerializeField] SolarSystemConfig m_SolarSystemConfig;

        public void Start()
        {
            SolarSystem newSolarSystem = GameObject.Instantiate(m_SolarSystemConfig.GetSolarSystemPrefab());
            newSolarSystem.Initialize(m_SolarSystemConfig);
        }
    }
}