using System;
using UnityEngine;
using UnityEngine.Serialization;
using Vehicles.Configs;

namespace Vehicles.Core
{
    public class VehicleComponentBase : MonoBehaviour
    {
        [Header("Config")]
        [SerializeField] VehicleConfigBase m_VehicleConfig;
        [SerializeField] Material m_TrajectoryMaterial;

        PathData m_PathData = null;
        LineRenderer m_TrajectoryRenderer;
        
        public void StartTravel(PathData pathData)
        {
            m_PathData = pathData;
        }
        
        void Start()
        {
            int vehicleSize = m_VehicleConfig.GetVehicleSize();
            transform.localScale = new Vector3(vehicleSize, vehicleSize, 1f);
            m_TrajectoryRenderer = gameObject.AddComponent<LineRenderer>();
        }

        void Update()
        {
            UpdateMoveDirection();
        }

        void UpdateMoveDirection()
        {
            if (m_PathData == null)
                return;
            
            Vector3 vehiclePosition = transform.position;
            
            Vector3 toTarget = m_PathData.m_DestinationTransform.position - vehiclePosition;
            Vector3 toStar = m_PathData.m_StarTransform.position - vehiclePosition;

            Vector3 moveDirection;
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
            if (toStar.magnitude > m_VehicleConfig.GetAvoidanceRadius() + m_PathData.m_StarRadius)
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
            if (m_PathData == null)
                return;
            
            Vector3 vehiclePosition = transform.position;
            Vector3 destinationPosition = m_PathData.m_DestinationTransform.position;
            Vector3[] points = new Vector3[2];
            
            Vector3 toGoal = destinationPosition - vehiclePosition;
            Vector3 toStar = m_PathData.m_StarTransform.position - vehiclePosition;
            points[0] = vehiclePosition;
            points[1] = destinationPosition;

            m_TrajectoryRenderer.material = m_TrajectoryMaterial;
            m_TrajectoryRenderer.endColor = Color.cyan;
            m_TrajectoryRenderer.startColor = Color.cyan;
            m_TrajectoryRenderer.startWidth = 1.5f;
            m_TrajectoryRenderer.endWidth = 1.5f;
            m_TrajectoryRenderer.positionCount = points.Length;
            m_TrajectoryRenderer.SetPositions(points);
        }
    }
}

