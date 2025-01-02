using UnityEngine;
using Vehicles.Constants;

namespace Vehicles.Configs
{
    [CreateAssetMenu(fileName = "VehicleConfigBase", menuName = VehicleConstants.S_VehicleConfigsMenuName + "VehicleConfigBase")]
    public class VehicleConfigBase : ScriptableObject
    {
        [SerializeField] int m_VehicleSize = 1;
        [SerializeField] float m_VehicleSpeed = 5f;
        [SerializeField] float m_AvoidanceRadius = 2f;
        [SerializeField] float m_ConeAngle = 45f; //Degrees
        
        //Getters
        public float GetVehicleSpeed() => m_VehicleSpeed;
        public float GetAvoidanceRadius() => m_AvoidanceRadius;
        public float GetConeAngle() => m_ConeAngle;
        public int GetVehicleSize() => m_VehicleSize;
    }
}