using Resources;
using UnityEngine;

namespace Core.Controllers
{
    public class PlayerInventoryController : ControllerBase
    {
        [SerializeField] PlayerStartingResourcesConfig m_StartingResourcesConfig;
        
        ResourcesHolder m_ResourcesHolder = new Resources.ResourcesHolder();
        
        public override void Initialize()
        {
            OnGameStart();
        }

        public void OnGameStart()
        {
            //Should be called once the game starts, but for now, we are calling it whenever the controller is created

            foreach (PlayerStartingResourcesConfig.ResourceAmountWrapper wrapper in m_StartingResourcesConfig.GetResourceAmounts())
            {
                m_ResourcesHolder.AddResource(wrapper.m_ResourceType, wrapper.m_StartingAmount);
            }
        }

        public ResourcesHolder GetPlayerResourcesHolder() => m_ResourcesHolder;
    }
}

