using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Resources
{
    [CreateAssetMenu(fileName = "Player Starting Resources Config", menuName = Constants.ResourcesConstants.S_PlayerResourcesConfigAssetMenu + "Player Starting Resources Config")]
    public class PlayerStartingResourcesConfig : ScriptableObject
    {
        [System.Serializable]
        public struct ResourceAmountWrapper
        {
            public EResourceType m_ResourceType;
            public float m_StartingAmount;
        }
        
        [SerializeField] List<ResourceAmountWrapper> m_ResourceAmounts = new List<ResourceAmountWrapper>();
        
        //Getters
        public List<ResourceAmountWrapper> GetResourceAmounts() => m_ResourceAmounts;
    }
}