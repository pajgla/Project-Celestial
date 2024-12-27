using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;

namespace CelestialObjects
{
    [RequireComponent(typeof(SpriteRenderer))]
    public class CelestialObjectBase : MonoBehaviour
    {
        float m_CurrentAngleToParentObject = 0.0f;
        float m_OrbitSpeed = 1.0f;
        float m_OrbitRadius = 1.0f;

        bool m_IsMoveableOrbit = false;
        
        //Sun for the planet, and planet for the moons - around what we rotate
        CelestialObjectBase m_ParentCelestialObject;

        public virtual void Initialize(CelestialObjectBase parentObject, CelestialObjectConfigBase config, float orbitRadius)
        {
            SetParentCelestialObject(parentObject);
            m_OrbitRadius = orbitRadius;
            m_OrbitSpeed = Random.Range(config.GetMinOrbitSpeed(), config.GetMaxOrbitSpeed());
            m_CurrentAngleToParentObject = Random.Range(0.0f, 360.0f);
            m_IsMoveableOrbit = config.GetIsMoveableOrbit();

            GetComponent<SpriteRenderer>().color = config.GetRandomColor(this);

            if (config.GetShouldDrawOrbit())
            {
                DrawOrbit(config);
            }
        }

        void Update()
        {
            if (GetParentCelestialObject() != null)
            {
                SolarSystemHelpers.RotateCelestialObject(this);
            }

            if (m_IsMoveableOrbit)
            {
                UpdateOrbit();
            }
        }

        private void DrawOrbit(CelestialObjectConfigBase config)
        {
            LineRenderer lineRenderer = gameObject.AddComponent<LineRenderer>();
            lineRenderer.loop = true;
            lineRenderer.useWorldSpace = true;
            lineRenderer.startWidth = config.GetOrbitDrawWidth();
            lineRenderer.endWidth = config.GetOrbitDrawWidth();
            lineRenderer.positionCount = config.GetOrbitDrawPoints();
            lineRenderer.material = config.GetOrbitMaterial();

            lineRenderer.startColor = config.GetOrbitColor();
            lineRenderer.endColor = config.GetOrbitColor();

            Vector3 parentObjectPosition = GetParentCelestialObject().transform.position;
            Vector3[] localPoints = new Vector3[lineRenderer.positionCount];
            for (int i = 0; i < localPoints.Length; i++)
            {
                float angle = i * 360f / (localPoints.Length - 1);
                float angleInRadians = angle * Mathf.Deg2Rad;
                float x = parentObjectPosition.x + Mathf.Cos(angleInRadians) * m_OrbitRadius;
                float y = parentObjectPosition.y + Mathf.Sin(angleInRadians) * m_OrbitRadius;
                
                localPoints[i] = new Vector3(x, y, 0.0f);
            }
        
            lineRenderer.SetPositions(localPoints);
        }

        private void UpdateOrbit()
        {
            LineRenderer lineRenderer = GetComponent<LineRenderer>();
            if (lineRenderer == null)
            {
                Debug.LogError("LineRenderer is missing from a celestial object.");
                return;
            }
            Vector3 parentObjectPosition = GetParentCelestialObject().transform.position;
            int vertices = lineRenderer.positionCount;
            Vector3[] localPoints = new Vector3[vertices];
            lineRenderer.GetPositions(localPoints);
            
            for (int i = 0; i < vertices; i++)
            {
                //#TODO Optimize if needed
                float angle = i * 360f / (vertices - 1);
                float angleInRadians = angle * Mathf.Deg2Rad;
                float x = parentObjectPosition.x + Mathf.Cos(angleInRadians) * m_OrbitRadius;
                float y = parentObjectPosition.y + Mathf.Sin(angleInRadians) * m_OrbitRadius;
                
                localPoints[i] = new Vector3(x, y, 0.0f);
            }
            
            lineRenderer.SetPositions(localPoints);
        }

        //Getters
        public float GetCurrentAngleToParentObject() { return m_CurrentAngleToParentObject; }
        public void SetCurrentAngleToParentObject(float value) { m_CurrentAngleToParentObject = value; }
        public float GetOrbitSpeed() { return m_OrbitSpeed; }
        public void SetOrbitSpeed(float value) { m_OrbitSpeed = value; }
        public CelestialObjectBase GetParentCelestialObject() { return m_ParentCelestialObject; }
        public void SetParentCelestialObject(CelestialObjectBase celestialObject) { m_ParentCelestialObject = celestialObject; }
        public float GetOrbitRadius() { return m_OrbitRadius; }
        public void SetOrbitRadius(float value) { m_OrbitRadius = value; }
    }
}

