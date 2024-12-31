using Core.Input;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Core.Camera
{
    [RequireComponent(typeof(UnityEngine.Camera))]
    public class CameraMovement : MonoBehaviour
    {
        [Header("Movement")]
        [SerializeField] float m_DefaultZPosition = -10f;
        [SerializeField] float m_CameraMoveSpeed = 5f;
        [SerializeField] float m_CameraSmoothing = 0.1f;

        [Header("Zooming")] 
        [SerializeField] float m_ZoomSpeed = 2f;
        [SerializeField] float m_MinZoom = 5f;
        [SerializeField] float m_MaxZoom = 2000f;
        [SerializeField] float m_ZoomSmoothing = 0.1f;
        [SerializeField] float m_InitialZoom = 2000f;

        Vector3 m_TargetPosition;
        Vector3 m_MoveVelocity = Vector3.zero;
        Vector2 m_LastMousePosition;
        float m_TargetZoom;
        bool m_IsDragging = false;
        UnityEngine.Camera m_CameraComponentRef = null;
        
        GameplayActionMaps m_GameplayActionMapsRef;
        
        void Start()
        {
            InitializeInputCallbacks();
            
            m_TargetZoom = m_InitialZoom;
        }

        // Update is called once per frame
        void Update()
        {
            UpdateCameraMovement();
            UpdateCameraZoom();
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

        void UpdateCameraTargetPosition()
        {
            Vector2 currentMousePosition = m_GameplayActionMapsRef.Default.MousePosition.ReadValue<Vector2>();
            Vector2 deltaMousePosition = currentMousePosition - m_LastMousePosition;
            
            //Scale movement by zoom level
            float zoomScale = m_CameraComponentRef.orthographicSize / m_InitialZoom;
            Vector3 movement = new Vector3(-deltaMousePosition.x, -deltaMousePosition.y, 0) * zoomScale;
            m_TargetPosition += movement * m_CameraMoveSpeed;

            m_LastMousePosition = currentMousePosition;
        }

        void UpdateCameraMovement()
        {
            if (m_IsDragging)
            {
                UpdateCameraTargetPosition();
            }
            
            Vector3 newPosition = Vector3.SmoothDamp(transform.position, m_TargetPosition, ref m_MoveVelocity, m_CameraSmoothing);
            newPosition.z = m_DefaultZPosition;
            transform.position = newPosition;
        }

        void UpdateCameraZoom()
        {
            if (m_CameraComponentRef == null)
            {
                //This component requires Camera component, so no need to nullcheck
                m_CameraComponentRef = GetComponent<UnityEngine.Camera>();                
            }
            
            m_CameraComponentRef.orthographicSize = Mathf.Lerp(m_CameraComponentRef.orthographicSize, m_TargetZoom, m_ZoomSmoothing);
        }

        void OnMouseScroll(InputAction.CallbackContext context)
        {
            float scrollValue = context.ReadValue<Vector2>().y;
            
            m_TargetZoom -= scrollValue * m_ZoomSpeed;
            m_TargetZoom = Mathf.Clamp(m_TargetZoom, m_MinZoom, m_MaxZoom);
        }

        void InitializeInputCallbacks()
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
            defaultInputActions.Default.CameraZoom.performed += OnMouseScroll;
            
            m_GameplayActionMapsRef = defaultInputActions;
        }
    }
}

