using System.Collections.Generic;
using Resources;
using Resources.Constants;
using UnityEngine;

namespace SolarSystem.Configs
{
    [CreateAssetMenu(fileName = "New Planet Type Resource Rarity Config", menuName = ResourcesConstants.S_CelestialResourcesConfigMenuName + "Planet Type Resource Rarity Config")]
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
        
        //Getters
        public List<PlanetTypeResourceRarityWrapper> GetResourceRarities() { return m_ResourceRarities; }
    } 
}