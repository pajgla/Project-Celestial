using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Resources
{
    public enum EResourceRarity
    {
        Common, //Can be found in large quantities
        Uncommon, //Can be found in modest quantities
        Rare, //Hard to find
        Celestial, //Extremely hard to find
        
        None //Used when nothing can be found, ever!
    }
}