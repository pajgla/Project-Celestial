using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SolarSystem.Debugging
{
    public class RegionDebug : MonoBehaviour
    {
        [SerializeField] Color m_InnerRegionColor = Color.white;
        [SerializeField] Color m_MiddleRegionColor = Color.white;
        [SerializeField] Color m_OuterRegionColor = Color.white;

        [SerializeField] CelestialRegionConfig m_RegionConfig;
        [SerializeField] SolarSystemConfig m_SolarSystemConfig;
        [SerializeField] GameObject m_RegionPrefab;

        void Start()
        {
            CreateRegionDebug(m_InnerRegionColor, m_RegionConfig.GetInnerRegionRadius() * 2f, -1);
            CreateRegionDebug(m_MiddleRegionColor, m_RegionConfig.GetMiddleRegionRadius() * 2f, -2);
            CreateRegionDebug(m_OuterRegionColor, m_SolarSystemConfig.GetMaxSolarSystemRadius() * 2f, -3);
        }

        private void CreateRegionDebug(Color color, float diameter, int sortingLayer)
        {
            GameObject regionGO = Instantiate(m_RegionPrefab, transform);
            regionGO.transform.position = Vector3.zero;

            SpriteRenderer spriteRenderer = regionGO.GetComponent<SpriteRenderer>();
            spriteRenderer.color = color;
            spriteRenderer.sortingOrder = sortingLayer;
            regionGO.transform.localScale = new Vector3(diameter, diameter, 1f);
        }
    }
}

