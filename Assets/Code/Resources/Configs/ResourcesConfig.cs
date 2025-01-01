using System.Collections;
using System.Collections.Generic;
using SolarSystem;
using UnityEngine;

namespace Resources
{
    [CreateAssetMenu(fileName = "New Resources Config", menuName = "Config/Resources/Resources Config")]
    public class ResourcesConfig : ScriptableObject
    {
        [SerializeField] PlanetTypeResourceRarityConfig m_PlanetTypeResourceRarityConfig;
        [SerializeField] RarityChancesConfig m_RarityChancesConfig;
        [SerializeField] ResourceSpawningConfig m_ResourceSpawningConfig;
        
        //Getters
        public PlanetTypeResourceRarityConfig GetPlanetTypeResourceRarityConfig() => m_PlanetTypeResourceRarityConfig;
        public RarityChancesConfig GetRarityChancesConfig() => m_RarityChancesConfig;
        public ResourceSpawningConfig GetResourceSpawningConfig() => m_ResourceSpawningConfig;
    }
}

