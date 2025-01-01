using System.Collections.Generic;
using UnityEngine;

namespace Resources
{
    [System.Serializable]
    public class RarityChance
    {
        public EResourceRarity m_Rarity;
        [Range(0, 100)] 
        public int m_Chance;
    }
    
    [CreateAssetMenu(fileName = "New Rarity Chances Config", menuName = Constants.ResourcesConstants.S_CelestialResourcesConfigMenuName + "Rarity Chances Config")]
    public class RarityChancesConfig : ScriptableObject
    {
        [SerializeField] List<RarityChance> m_RarityChances = new List<RarityChance>();

        public int GetSpawnChanceFor(EResourceRarity rarity)
        {
            foreach (RarityChance rarityChance in m_RarityChances)
            {
                if (rarityChance.m_Rarity == rarity)
                {
                    return rarityChance.m_Chance;
                }
            }
            
            Debug.LogError($"Missing Rarity Chance Config for {rarity.ToString()}");
            return 0;
        }

        public bool IsChanceCheckPassed(EResourceRarity rarity, int chance)
        {
            return chance <= GetSpawnChanceFor(rarity);
        }
    }
}

