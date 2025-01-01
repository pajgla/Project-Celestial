using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SolarSystem
{
    public class SolarSystemGenerator : MonoBehaviour
    {
        [SerializeField] Configs.SolarSystemConfig m_SolarSystemConfig;

        public void Start()
        {
            SolarSystem newSolarSystem = GameObject.Instantiate(m_SolarSystemConfig.GetSolarSystemPrefab(), transform.position, Quaternion.identity);
            newSolarSystem.Initialize(m_SolarSystemConfig);
        }

        void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                Time.timeScale = 3;
            }
        }
    }
}