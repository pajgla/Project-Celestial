using System.Collections;
using System.Collections.Generic;
using SolarSystem;
using SolarSystem.Configs;
using UnityEngine;

namespace Resources
{
    [CreateAssetMenu(fileName = "New Resources Config", menuName = "Config/Resources/Resources Config")]
    public class ResourcesConfig : ScriptableObject
    {
        [SerializeField] RarityChancesConfig m_RarityChancesConfig;
        [SerializeField] ResourceSpawningConfig m_ResourceSpawningConfig;
        
        [Header("Planet specific")]
        [SerializeField] PlanetTypeResourceRarityConfig m_PlanetTypeResourceRarityConfig;
        
        [Header("Moon specific")]
        [SerializeField] MoonTypeResourceRarityConfig m_MoonTypeResourceRarityConfig;
        
        //Getters
        public PlanetTypeResourceRarityConfig GetPlanetTypeResourceRarityConfig() => m_PlanetTypeResourceRarityConfig;
        public RarityChancesConfig GetRarityChancesConfig() => m_RarityChancesConfig;
        public ResourceSpawningConfig GetResourceSpawningConfig() => m_ResourceSpawningConfig;
        public MoonTypeResourceRarityConfig GetMoonTypeResourceRarityConfig() => m_MoonTypeResourceRarityConfig;
    }
}

