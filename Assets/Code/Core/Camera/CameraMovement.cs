using Core.Input;
using UnityEngine;

namespace Core.Camera
{
    public class CameraMovement : MonoBehaviour
    {
        [SerializeField] float m_DefaultZPosition = -10f;
        [SerializeField] float m_CameraMoveSpeed = 5f;
        [SerializeField] float m_CameraSmoothing = 0.1f;

        Vector3 m_TargetPosition;
        Vector3 m_Velocity = Vector3.zero;
        Vector2 m_LastMousePosition;
        bool m_IsDragging = false;
        
        GameplayActionMaps m_GameplayActionMapsRef;
        
        void Start()
        {
            InputManager inputManager = InputManager.GetInstance();
            if (inputManager == null)
            {
                Debug.LogError("Input Manager is null.");
                return;
            }

            GameplayActionMaps defaultInputActions = inputManager.GetDefaultInputActions();
            defaultInputActions.Default.CameraMove.started += _ => StartDragging();
            defaultInputActions.Default.CameraMove.canceled += _ => StopDragging();
            
            m_GameplayActionMapsRef = defaultInputActions;
        }

        // Update is called once per frame
        void Update()
        {
            if (m_IsDragging)
            {
                MoveCameraFromInput();
            }
            
            Vector3 newPosition = Vector3.SmoothDamp(transform.position, m_TargetPosition, ref m_Velocity, m_CameraSmoothing);
            newPosition.z = m_DefaultZPosition;
            transform.position = newPosition;
        }

        private void StartDragging()
        {
            m_IsDragging = true;
            m_LastMousePosition = m_GameplayActionMapsRef.Default.MousePosition.ReadValue<Vector2>();
        }

        private void StopDragging()
        {
            m_IsDragging = false;
        }

        private void MoveCameraFromInput()
        {
            Vector2 currentMousePosition = m_GameplayActionMapsRef.Default.MousePosition.ReadValue<Vector2>();
            Vector2 deltaMousePosition = currentMousePosition - m_LastMousePosition;
            
            Vector3 movement = new Vector3(-deltaMousePosition.x, -deltaMousePosition.y, 0);
            m_TargetPosition += movement * m_CameraMoveSpeed;

            m_LastMousePosition = currentMousePosition;
        }
    }
}

