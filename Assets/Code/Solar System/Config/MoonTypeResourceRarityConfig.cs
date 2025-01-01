using System.Collections.Generic;
using Resources;
using UnityEngine;

namespace SolarSystem.Configs
{
    [CreateAssetMenu(fileName = "New Moon Type Resource Rarity Config", menuName = "Config/Resources/Moon Type Resource Rarity Config")]
    public class MoonTypeResourceRarityConfig : ScriptableObject
    {
        [SerializeField] List<MoonTypeResourceRarityWrapper> m_ResourceRarities = new List<MoonTypeResourceRarityWrapper>();
        
        public List<ResourceRarityWrapper> GetResourceRaritiesForPlanetType(EMoonType moonType)
        {
            foreach (MoonTypeResourceRarityWrapper resourceRarity in m_ResourceRarities)
            {
                if (resourceRarity.GetMoonType() == moonType)
                {
                    return resourceRarity.GetResourceRarities();
                }
            }
            
            Debug.LogError($"No resource rarities found for moon type {moonType.ToString()}");
            return new List<ResourceRarityWrapper>();
        }
    }
}

