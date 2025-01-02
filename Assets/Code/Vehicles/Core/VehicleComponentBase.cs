using System;
using UnityEngine;
using Vehicles.Configs;

namespace Vehicles.Core
{
    public class VehicleComponentBase : MonoBehaviour
    {
        [Header("Config")]
        [SerializeField] VehicleConfigBase m_VehicleConfig;
        [SerializeField] Material m_Material;
        
        public Transform m_StarTransform;
        public Transform m_TargetTransform;
        public float m_StarRadius = 1f;
        public LineRenderer m_TrajectoryRenderer;
        bool m_IsInitialized = false;
        
        public void StartTravel(Transform starTransform, Transform targetTransform, float starRadius)
        {
            m_StarTransform = starTransform;
            m_TargetTransform = targetTransform;
            m_StarRadius = starRadius;
            m_IsInitialized = true;
        }
        
        void Start()
        {
            int vehicleSize = m_VehicleConfig.GetVehicleSize();
            transform.localScale = new Vector3(vehicleSize, vehicleSize, 1f);
            m_TrajectoryRenderer = gameObject.AddComponent<LineRenderer>();
        }

        void Update()
        {
            if (!m_IsInitialized)
                return;
            
            UpdateMoveDirection();
        }

        void UpdateMoveDirection()
        {
            Vector3 vehiclePosition = transform.position;
            Vector3 starPosition = m_StarTransform.position;
            Vector3 targetPosition = m_TargetTransform.position;
            
            Vector3 toTarget = targetPosition - vehiclePosition;
            Vector3 toStar = starPosition - vehiclePosition;

            Vector3 moveDirection = Vector3.zero;
            if (IsStarInAvoidanceCone(toTarget, toStar))
            {
                moveDirection = CalculateAvoidanceDirection(toTarget, toStar);
            }
            else
            {
                moveDirection = toTarget;
            }
            
            MoveInDirection(moveDirection);
            RotateTowardsDirection(moveDirection);

            if (m_TrajectoryRenderer != null)
            {
                UpdateTrajectory();
            }
        }

        bool IsStarInAvoidanceCone(Vector3 toTarget, Vector3 toStar)
        {
            if (toStar.magnitude > m_VehicleConfig.GetAvoidanceRadius() + m_StarRadius)
                return false;
            
            //Check the angle between the direction to the target and the direction to the star
            float angle = Vector3.Angle(toTarget, toStar);
            return angle < m_VehicleConfig.GetConeAngle();
        }

        Vector3 CalculateAvoidanceDirection(Vector3 toTarget, Vector3 toStar)
        {
            //Calculate a tangent vector to steer around the star
            Vector3 perpendicular = Vector3.Cross(toStar, Vector3.forward).normalized;
            
            //Decide which direction to steer based on the cross product
            if (Vector3.Dot(perpendicular, toTarget) < 0)
            {
                perpendicular = -perpendicular;
            }
            
            //Blend the perpendicula direction with the toTarget direction for smoother transition
            return Vector3.Lerp(toTarget.normalized, perpendicular, 0.5f).normalized;
        }

        void MoveInDirection(Vector3 direction)
        {
            transform.position += direction.normalized * (m_VehicleConfig.GetVehicleSpeed() * Time.deltaTime);
        }

        void RotateTowardsDirection(Vector3 direction)
        {
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0, 0, angle);
        }

        void UpdateTrajectory()
        {
            Vector3 shipPosition = transform.position;
            Vector3 goalPosition = m_TargetTransform.position;
            Vector3 starPosition = m_StarTransform.position;

            // Predict points along the trajectory
            Vector3 currentPosition = shipPosition;
            Vector3[] points = new Vector3[2]; // Adjust number of points for smoother debug line

            Vector3 toGoal = goalPosition - currentPosition;
            Vector3 toStar = starPosition - currentPosition;
            points[0] = currentPosition;
            points[1] = goalPosition;

            m_TrajectoryRenderer.material = m_Material;
            m_TrajectoryRenderer.endColor = Color.cyan;
            m_TrajectoryRenderer.startColor = Color.cyan;
            m_TrajectoryRenderer.startWidth = 1.5f;
            m_TrajectoryRenderer.endWidth = 1.5f;
            m_TrajectoryRenderer.positionCount = points.Length;
            m_TrajectoryRenderer.SetPositions(points);
        }
    }
}

