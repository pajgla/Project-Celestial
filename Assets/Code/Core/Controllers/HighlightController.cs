using Core.Components;
using Core.Input;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Core.Controllers
{
    //#TODO change name since we can also select objects
    public class HighlightController : ControllerBase
    {
        ObjectHighlighterComponent m_ObjectHighlighterComponent = null;
        
        public override void Initialize()
        {
            InputManager inputManager = InputManager.GetInstance();
            if (inputManager == null)
            {
                Debug.LogError("Input Manager is null.");
                return;
            }
            
            GameplayActionMaps gameplayActionMaps = inputManager.GetGameplayActionMaps();
            gameplayActionMaps.Default.Select.performed += OnSelectInputAction;
        }

        void Update()
        {
            DetectHighlight();
        }

        private void DetectHighlight()
        {
            Vector2 mousePosition = GetMousePosition();
            Vector2 mouseWorldPosition = UnityEngine.Camera.main.ScreenToWorldPoint(mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(mouseWorldPosition, Vector2.zero);

            if (hit.collider != null)
            {
                ObjectHighlighterComponent component = hit.collider.gameObject.GetComponent<ObjectHighlighterComponent>();
                if (component == null)
                {
                    Unhighlight();
                }
                else if (component != m_ObjectHighlighterComponent)
                {
                    Highlight(component);
                }
            }
            else
            {
                Unhighlight();
            }
        }

        private void Unhighlight()
        {
            if (m_ObjectHighlighterComponent != null)
            {
                m_ObjectHighlighterComponent.OnUnhighlight();
                m_ObjectHighlighterComponent = null;
            }
        }

        private void Highlight(ObjectHighlighterComponent component)
        {
            Unhighlight();
            m_ObjectHighlighterComponent = component;
            m_ObjectHighlighterComponent.OnHighlight();
            
            print("Highlighted");
        }

        Vector2 GetMousePosition()
        {
            InputManager inputManager = InputManager.GetInstance();
            if (inputManager == null)
            {
                Debug.LogError("Input Manager is null");
                return Vector2.zero;
            }

            GameplayActionMaps gameplayActionMaps = inputManager.GetGameplayActionMaps();

            return gameplayActionMaps.Default.MousePosition.ReadValue<Vector2>();
        }

        void OnSelectInputAction(InputAction.CallbackContext context)
        {
            if (m_ObjectHighlighterComponent != null)
            {
                m_ObjectHighlighterComponent.OnSelect();
            }
        }
    }
}

