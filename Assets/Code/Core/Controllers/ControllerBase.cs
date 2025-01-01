using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Core.Controllers
{
    //Controllers are a single-responsibility units that are neither managers nor components
    public abstract class ControllerBase : MonoBehaviour
    {
        public abstract void Initialize();
    }
}

