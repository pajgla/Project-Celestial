using System.Collections;
using System.Collections.Generic;
using CelestialObjects;
using UnityEditor.Networking.PlayerConnection;
using UnityEngine;

public static class SolarSystemHelpers
{
    public static Star GenerateNewStar(SolarSystemConfig config, Transform parentTransform)
    {
        Star newStar = GameObject.Instantiate(config.GetStarPrefab(), parentTransform.position, Quaternion.identity, parentTransform);
        SpriteRenderer spriteRenderer = newStar.GetComponent<SpriteRenderer>();
        if (spriteRenderer == null)
        {
            GameObject.Destroy(newStar.gameObject);
            
            Debug.LogError("Star Prefab doesn't have SpriteRenderer");
            return null;
        }

        //#TODO Choose random
        spriteRenderer.sprite = config.GetStarSprites()[0];
        
        //Radius
        float randomRadius = config.GetStarRadiusRange().GetRandomValueFromRange();
        newStar.SetRadius(randomRadius);
        
        
        return newStar;
    }
    
    public static Planet GenerateNewPlanet(PlanetConfig config)
    {
        Planet newPlanet = GameObject.Instantiate(config.GetPlanetPrefab());
        
        //Chose color
        SpriteRenderer spriteRenderer = newPlanet.GetComponent<SpriteRenderer>();
        if (spriteRenderer == null)
        {
            GameObject.Destroy(newPlanet.gameObject);
            Debug.LogError("Planet Prefab doesn't have SpriteRenderer");
            return null;
        }
        
        spriteRenderer.color = config.GetRandomColor(newPlanet);
        
        //Planet size
        float randomRadius = config.GetObjectRadiusRange().GetRandomValueFromRange();
        newPlanet.SetRadius(randomRadius);
        
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
