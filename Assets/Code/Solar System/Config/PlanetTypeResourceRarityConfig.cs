using System.Collections;
using System.Collections.Generic;
using Resources;
using UnityEngine;

namespace SolarSystem
{
    [CreateAssetMenu(fileName = "New Planet Type Resource Rarity Config", menuName = "Config/Planet Type Resource Rarity Config")]
    public class PlanetTypeResourceRarityConfig : ScriptableObject
    {
        [SerializeField] List<PlanetTypeResourceRarityWrapper> m_ResourceRarities = new List<PlanetTypeResourceRarityWrapper>();


        public List<ResourceRarityWrapper> GetResourceRaritiesForPlanetType(EPlanetType planetType)
        {
            foreach (PlanetTypeResourceRarityWrapper resourceRarity in m_ResourceRarities)
            {
                if (resourceRarity.GetPlanetType() == planetType)
                {
                    return resourceRarity.GetResourceRarities();
                }
            }
            
            Debug.LogError($"No resource rarities found for planet type {planetType.ToString()}");
            return new List<ResourceRarityWrapper>();
        }

        public EResourceRarity GetResourceRarityForPlanetType(EPlanetType planetType, EResourceType resourceType)
        {
            List<ResourceRarityWrapper> resourceRarities = GetResourceRaritiesForPlanetType(planetType);
            foreach (ResourceRarityWrapper resourceRarityWrapper in resourceRarities)
            {
                if (resourceRarityWrapper.GetResourceType() == resourceType)
                {
                    return resourceRarityWrapper.GetResourceRarity();
                }
            }

            return EResourceRarity.None;
        }
        
        //Getters
        public List<PlanetTypeResourceRarityWrapper> GetResourceRarities() { return m_ResourceRarities; }
    } 
}