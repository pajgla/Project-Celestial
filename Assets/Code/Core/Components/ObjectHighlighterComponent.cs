using System;
using System.Collections;
using System.Collections.Generic;
using Resources;
using SolarSystem;
using UnityEngine;

namespace Core.Components
{
    [RequireComponent(typeof(SpriteRenderer))]
    public class ObjectHighlighterComponent : MonoBehaviour
    {
        [SerializeField] Material m_DefaultMaterial;
        [SerializeField] Material m_HighlightedMaterial;

        void Start()
        {
            if (m_DefaultMaterial == null)
            {
                SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
                m_DefaultMaterial = spriteRenderer.material;
            }

            if (m_HighlightedMaterial == null)
            {
                Debug.LogError("Highlighted material is null!");
                this.enabled = false;
            }
        }

        public void OnHighlight()
        {
            SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
            spriteRenderer.material = m_HighlightedMaterial;
        }

        public void OnUnhighlight()
        {
            SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
            spriteRenderer.material = m_DefaultMaterial;
        }

        public void OnSelect()
        {
            //#TODO This is just for debugging purposes
            
            Planet planet = GetComponent<Planet>();
            ResourcesHolder resourcesHolder = planet.GetResourcesHolder();

            foreach (var resourceTuple in resourcesHolder.GetHoldingResources())
            {
                Debug.Log(resourceTuple.Key.ToString() + ": " + resourceTuple.Value);
            }
        }
    }
}

