using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Core
{
    public abstract class RangeValueBase<T>
    {
        [SerializeField] protected T m_MinValue = default(T);
        [SerializeField] protected T m_MaxValue = default(T);

        public abstract T GetRandomValueFromRange();
        
        public T GetMinValue() { return m_MinValue; }
        public T GetMaxValue() { return m_MaxValue; }
    }
    
    [Serializable]
    public class RangeValueFloat : RangeValueBase<float>
    {
        public override float GetRandomValueFromRange()
        {
            return Random.Range(m_MinValue, m_MaxValue);
        }
    }

    [Serializable]
    public class RangeValueInt : RangeValueBase<int>
    {
        public override int GetRandomValueFromRange()
        {
            return Random.Range(m_MinValue, m_MaxValue);
        }
    }
}

