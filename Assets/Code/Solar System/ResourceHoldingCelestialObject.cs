using Resources;

namespace SolarSystem
{
    public class ResourceHoldingCelestialObject : CelestialObjectBase
    {
        CelestialObjectResourcesHolder m_CelestialObjectResourcesHolder = new CelestialObjectResourcesHolder();
        
        //Getters
        public CelestialObjectResourcesHolder GetCelestialObjectResourcesHolder() => m_CelestialObjectResourcesHolder;
    }
}

