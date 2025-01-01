using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Resources
{
    [CreateAssetMenu(fileName = "New Resource Spawning Config", menuName = Constants.ResourcesConstants.S_CelestialResourcesConfigMenuName + "Resource Spawning Config")]
    public class ResourceSpawningConfig : ScriptableObject
    {
        [System.Serializable]
        public class ResourceAmountWrapper
        {
            public EResourceType m_ResourceType;
            public List<ResourceAmountEntry> m_ResourceAmountEntries = new List<ResourceAmountEntry>(); 
        }

        [System.Serializable]
        public class ResourceAmountEntry
        {
            public EResourceRarity m_ResourceRarity;
            public Core.Helpers.RangeValueFloat m_AmountRange;
            public float m_CelestialObjectRadiusMultiplier;
        }
        
        [SerializeField] List<ResourceAmountWrapper> m_ResourceAmountWrappers = new List<ResourceAmountWrapper>();

        public ResourceAmountEntry GetResourceAmountFor(EResourceType resourceType, EResourceRarity resourceRarity)
        {
            foreach (ResourceAmountWrapper amountWrapper in m_ResourceAmountWrappers)
            {
                if (amountWrapper.m_ResourceType == resourceType)
                {
                    foreach (ResourceAmountEntry amountEntry in amountWrapper.m_ResourceAmountEntries)
                    {
                        if (amountEntry.m_ResourceRarity == resourceRarity)
                        {
                            return amountEntry;
                        }
                    }
                }
            }
            
            Debug.LogError($"No entry found for Resource {resourceType} with Rarity {resourceRarity}");
            return null;
        }
    }

}
