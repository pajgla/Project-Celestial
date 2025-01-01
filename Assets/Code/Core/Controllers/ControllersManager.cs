using System;
using System.Collections.Generic;
using Core.Singleton;
using UnityEngine;

namespace Core.Controllers
{
    public class ControllersManager : Singleton<ControllersManager>
    {
        [SerializeField] List<ControllerBase> m_ControllersToInstantiate = new List<ControllerBase>();
        
        List<ControllerBase> m_ActiveControllers = new List<ControllerBase>();

        protected override void Awake()
        {
            base.Awake();

            foreach (ControllerBase controller in m_ControllersToInstantiate)
            {
                ControllerBase newController = Instantiate(controller, transform);
                m_ActiveControllers.Add(newController);
            }
        }

        void Start()
        {
            foreach (ControllerBase controller in m_ActiveControllers)
            {
                controller.Initialize();
            }
        }
    }
}

