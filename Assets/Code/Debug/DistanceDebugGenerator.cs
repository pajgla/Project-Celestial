using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SolarSystem.Debugging
{
    public class DistanceDebugGenerator : MonoBehaviour
    {
        [SerializeField] Sprite m_DebugSprite;
        [SerializeField] Color m_SpriteColor;
    
        [SerializeField] int m_SpawnAmount = 10;
        [SerializeField] bool m_SpawnNegative = false;
    
        // Start is called before the first frame update
        void Start()
        {
#if UNITY_EDITOR
            //Spawn initial
            SpawnNewGameObject(0);

            for (int i = 1; i < m_SpawnAmount; i++)
            {
                SpawnNewGameObject(i);
            }

            if (m_SpawnNegative)
            {
                for (int i = 1; i < m_SpawnAmount; i++)
                {
                    SpawnNewGameObject(-i);
                }
            }
        
#endif
        }

        GameObject SpawnNewGameObject(int x)
        {
            GameObject newGameObject = new GameObject();
            newGameObject.transform.parent = transform;
            newGameObject.transform.position = new Vector3(x, 0, 0);
            SpriteRenderer spriteRenderer = newGameObject.AddComponent<SpriteRenderer>();
            spriteRenderer.sprite = m_DebugSprite;
            spriteRenderer.sortingOrder = 100;
            spriteRenderer.color = m_SpriteColor;
        
            return newGameObject;
        }

        // Update is called once per frame
        void Update()
        {
        
        }
    }
}

