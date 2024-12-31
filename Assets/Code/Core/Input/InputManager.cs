using System;
using System.Collections;
using System.Collections.Generic;
using Core.Singleton;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Core.Input
{
    public class InputManager : Singleton<InputManager>
    {
        GameplayActionMaps m_DefaultInputActions;

        protected override void Awake()
        {
            base.Awake();
            
            m_DefaultInputActions = new GameplayActionMaps();
            m_DefaultInputActions.Enable();
        }

        void OnDestroy()
        {
            m_DefaultInputActions.Disable();
        }
        
        //Getters
        public GameplayActionMaps GetDefaultInputActions() { return m_DefaultInputActions; }
    }
}

