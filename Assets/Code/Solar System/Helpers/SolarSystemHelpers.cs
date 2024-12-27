using System.Collections;
using System.Collections.Generic;
using CelestialObjects;
using UnityEngine;

public static class SolarSystemHelpers
{
    public static Planet GenerateNewPlanet(PlanetConfig config)
    {
        Planet newPlanet = GameObject.Instantiate(config.GetPlanetPrefab());
        return newPlanet;
    }

    public static void RotateCelestialObject(CelestialObjectBase objectToRotate)
    {
        float currentOrbitAngle = objectToRotate.GetCurrentAngleToParentObject();
        float orbitSpeed = objectToRotate.GetOrbitSpeed();
        objectToRotate.SetCurrentAngleToParentObject(currentOrbitAngle + orbitSpeed * Time.deltaTime);

        Vector3 parentCelestialObjectPosition = objectToRotate.GetParentCelestialObject().transform.position;
        float orbitRadius = objectToRotate.GetOrbitRadius();
        float angleInRadians = currentOrbitAngle * Mathf.Deg2Rad;
        
        Vector3 newPosition = new Vector3(
            Mathf.Cos(angleInRadians) * orbitRadius,
            Mathf.Sin(angleInRadians) * orbitRadius,
            0f
        );
        
        objectToRotate.transform.position = parentCelestialObjectPosition + newPosition;
    }

    public static void DrawOrbit(CelestialObjectConfigBase config)
    {
        
    }
}
