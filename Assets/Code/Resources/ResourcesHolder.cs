using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Resources
{
    [System.Serializable]
    public class ResourcesHolder
    {
        [SerializeField] Dictionary<EResourceType, float> m_Resources = new Dictionary<EResourceType, float>();
        
        //Returns new amount of resource type
        public float AddResource(EResourceType resourceType, float amount)
        {
            if (m_Resources.TryAdd(resourceType, amount))
            {
                return amount;
            }
            
            m_Resources[resourceType] += amount;
            return m_Resources[resourceType];
        }

        //Returns new amount of resource type
        public float ReduceResource(EResourceType resourceType, float amount)
        {
            if (!m_Resources.ContainsKey(resourceType))
            {
                Debug.LogError("Trying to reduce resource that doesn't exist in this holder");
                return amount;
            }
            
            m_Resources[resourceType] -= amount;
            return m_Resources[resourceType];
        }
    }
}

