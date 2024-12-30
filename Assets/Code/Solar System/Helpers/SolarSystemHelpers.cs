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
        newStar.SetDiameter(randomRadius);
        
        
        return newStar;
    }
    
    public static Planet GenerateNewPlanet(PlanetConfig config)
    {
        Planet newPlanet = GameObject.Instantiate(config.GetPlanetPrefab());
        
        //Choose color
        SpriteRenderer spriteRenderer = newPlanet.GetComponent<SpriteRenderer>();
        if (spriteRenderer == null)
        {
            GameObject.Destroy(newPlanet.gameObject);
            Debug.LogError("Planet Prefab doesn't have SpriteRenderer");
            return null;
        }
        
        spriteRenderer.color = config.GetRandomColor(newPlanet);
        
        //Random angle to the star
        newPlanet.SetCurrentAngleToParentObject(Random.Range(0.0f, 360.0f));
        
        //Planet size
        float planetRadius = 0;
        
        float giantPlanetChance = Random.Range(0, 101);
        float requiredGiantPlanetChance = config.GetGiantPlanetChance();
        if (giantPlanetChance <= requiredGiantPlanetChance)
        {
            planetRadius = config.GetGiantPlanetRadius().GetRandomValueFromRange();
            newPlanet.SetIsGiantPlanet(true);
            Debug.Log("Spawned a giant!");
        }
        else
        {
            planetRadius = config.GetObjectRadiusRange().GetRandomValueFromRange();   
        }
        
        newPlanet.SetDiameter(planetRadius);
        
        //Generate Moons
        newPlanet.GenerateMoons(config);
        newPlanet.CalculateRadiusWithMoons();
        
        return newPlanet;
    }

    public static Moon GenerateNewMoon(MoonConfig config)
    {
        Moon newMoon = GameObject.Instantiate(config.GetMoonPrefab());

        float randomRadius = config.GetObjectRadiusRange().GetRandomValueFromRange();
        newMoon.SetDiameter(randomRadius);
        
        return newMoon;
    }

    public static void RotateCelestialObject(CelestialObjectBase objectToRotate)
    {
        //Problem here is that the speed of the planet changes if we simply use GetOrbitSpeed() since not all planets
        //have the same circumference of the orbit, thus even with the same speed set, planets will travel
        //at different speed based on their orbit circumference. The fix is to calculate angular speed instead
        
        float orbitRadius = objectToRotate.GetOrbitRadius();
        float linearOrbitSpeed = objectToRotate.GetOrbitSpeed();
        
        //Angular velocity in radians per second
        float angularOrbitSpeed = linearOrbitSpeed / orbitRadius;
        
        float currentOrbitAngle = objectToRotate.GetCurrentAngleToParentObject();
        currentOrbitAngle += angularOrbitSpeed * Mathf.Rad2Deg * Time.deltaTime;
        objectToRotate.SetCurrentAngleToParentObject(currentOrbitAngle);

        Vector3 parentCelestialObjectPosition = objectToRotate.GetParentCelestialObject().transform.position;
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
