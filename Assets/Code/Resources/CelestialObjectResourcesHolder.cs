using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Resources
{
    public class CelestialObjectResourcesHolder
    {
        ResourcesHolder m_ToBeMinedResourcesHolder = new ResourcesHolder();
        ResourcesHolder m_AvailableResourcesHolder = new ResourcesHolder();
        
        //Getters
        public ResourcesHolder GetToBeMinedResourcesHolder() => m_ToBeMinedResourcesHolder;
        public ResourcesHolder GetAvailableResourcesHolder() => m_AvailableResourcesHolder;
    }
}

