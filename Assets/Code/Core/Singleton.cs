using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Core.Singleton
{
    public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
    {
        [SerializeField] bool m_ShouldDestroyOnLoad = false;
        static T sm_Instance = null;
        
        protected virtual void Awake()
        {
            if (sm_Instance != null)
            {
                Debug.LogWarning($"One instance of {typeof(T).FullName} already exists. Destroying the new instance.");
                Destroy(gameObject);
                
                return;
            }
            
            sm_Instance = this as T;

            if (!m_ShouldDestroyOnLoad)
            {
                DontDestroyOnLoad(gameObject);
            }
        }

        public static T GetInstance()
        {
            return sm_Instance;
        }
    }
}