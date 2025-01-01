using System;
using System.Collections.Generic;
using SolarSystem;
using UnityEngine;

namespace Resources
{
    [Serializable]
    public struct ResourceRarityWrapper
    {
        [SerializeField] EResourceType m_ResourceType;
        [SerializeField] EResourceRarity m_ResourceRarity;
        
        public EResourceType GetResourceType() { return m_ResourceType; }
        public EResourceRarity GetResourceRarity() { return m_ResourceRarity; }
    }
    
    [Serializable]
    public struct PlanetTypeResourceRarityWrapper
    {
        [SerializeField] EPlanetType m_PlanetType;
        [SerializeField] List<ResourceRarityWrapper> m_ResourceRarities;
        
        public EPlanetType GetPlanetType() { return m_PlanetType; }
        public List<ResourceRarityWrapper> GetResourceRarities() { return m_ResourceRarities; }
    }

    [Serializable]
    public struct MoonTypeResourceRarityWrapper
    {
        [SerializeField] EMoonType m_MoonType;
        [SerializeField] List<ResourceRarityWrapper> m_ResourceRarities;
        public EMoonType GetMoonType() { return m_MoonType; }
        public List<ResourceRarityWrapper> GetResourceRarities() { return m_ResourceRarities; }
    }
}

