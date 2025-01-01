using SolarSystem.Configs;
using UnityEngine;

namespace SolarSystem.Factory
{
    public static class MoonFactory
    {
        public static Moon GenerateMoon(MoonConfig moonConfig, float orbitRadius)
        {
            Moon newMoon = GameObject.Instantiate(moonConfig.GetMoonPrefab());
            if (!ValidateMoonPrefab(newMoon))
            {
                GameObject.Destroy(newMoon);
                return null;
            }

            SetRandomAngleToPlanet(newMoon);
            ChooseRandomMoonType(newMoon);
            RandomizeMoonRadius(moonConfig, newMoon);
            DetermineMoonSprite(moonConfig, newMoon);
            DetermineMoonColor(moonConfig, newMoon);
            
            Resources.ResourcesGenerator.GenerateMoonResources(moonConfig, newMoon);
            return newMoon;
        }
        
        static bool ValidateMoonPrefab(Moon moon)
        {
            SpriteRenderer spriteRenderer = moon.GetComponent<SpriteRenderer>();
            if (spriteRenderer == null)
            {
                Debug.LogError("Moon Prefab doesn't have a SpriteRenderer!");
                return false;
            }

            return true;
        }
        
        static void SetRandomAngleToPlanet(Moon moon)
        {
            float randomAngle = Random.Range(0.0f, 360.0f);
            moon.SetCurrentAngleToParentObject(randomAngle);
        }

        static void ChooseRandomMoonType(Moon moon)
        {
            int lastEnumIndex = (int)EMoonType.Count;
            int choosenIndex = Random.Range(1, lastEnumIndex);
            EMoonType choosenType = (EMoonType)choosenIndex;
            moon.SetMoonType(choosenType);
        }

        static void RandomizeMoonRadius(MoonConfig config, Moon moon)
        {
            float randomDiameter = config.GetObjectDiameterRange().GetRandomValueFromRange();
            moon.SetDiameter(randomDiameter);
        }

        static void DetermineMoonSprite(MoonConfig config, Moon moon)
        {
            moon.GetComponent<SpriteRenderer>().sprite = config.GetAllowedSprites()[0];
        }

        static void DetermineMoonColor(MoonConfig config, Moon moon)
        {
            MoonColorConfig colorConfig = config.GetMoonColorConfig();
            if (colorConfig == null)
            {
                Debug.LogError("Moon Color Config is null!");
                return;
            }

            Color color = colorConfig.GetMoonColor(moon.GetMoonType());
            moon.GetComponent<SpriteRenderer>().color = color;
        }
    }
}

