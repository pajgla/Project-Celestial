using UnityEngine;

namespace SolarSystem
{
    public static class SolarSystemHelpers
    {
        public static Star GenerateNewStar(Configs.SolarSystemConfig config, Transform parentTransform)
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

        public static void DrawOrbit(Configs.CelestialObjectConfigBase config)
        {

        }

        public static ECelestialRegionType GetCelestialRegionTypeForOrbitRadius(Configs.CelestialRegionConfig config,
            float orbitRadius)
        {
            if (config == null)
            {
                Debug.LogError("CelestialRegionConfig is null");
                return ECelestialRegionType.Inner;
            }

            if (orbitRadius <= config.GetInnerRegionRadius())
            {
                return ECelestialRegionType.Inner;
            }
            else if (orbitRadius <= config.GetMiddleRegionRadius())
            {
                return ECelestialRegionType.Middle;
            }

            return ECelestialRegionType.Outer;
        }
    }
}
