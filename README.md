Currently in game:

1) Fully working solar system:
   - Star in the middle of the solar system with random size
   - Planets spawning on random distance from the star and random distance from other planets. Distances can be configured from the inspector. Planets can have random size too, and sometimes a planet can spawn as a Giant planet.
   - Solar system region - there are currently 3 regions. Each region determines distance from the star. First region is the closest to the star, and planets in that region usually have extreme conditions.
   - Planets can be of various types. For now, we have following types: Rocky, Volcanic, MetalRich, Deset, Terrestrial, Oceanic, Ice, FrozenRock, Cryovolcanic, GasGiant, IceGiant
   - There is a small chance for planets to have a moon, and a bit lower chance for a planet to have multiple moons. Moons can also have their own type: Steelheart, Igniflux, Crystalith, Plasmorph, Celestimoon
   - Each planet have resources. For now, we have following resources in the game: Metal, Fuel, VoidCrystal, OrganicMatter, Water, Plasma, Celestite
   - A resource can have different rarity, based on what planet are they spawning. Rarity does not directly determine if the resource should be spawned or not. Instead, the rarity tells us what is the maximum amount of that resource that we can find on a planet. For example, if the resource on one planet is rare, we can find it in ranges from 100 to 300, but if the same resource is common on another planet type, we can find it in a range from 1000 to 2000.
   - Moons can have only one resource, and their type tells us what resource they hold. They usually hold large amount of resources.

2) Vehicle system:
  - Vehicles can move from point a to point b and they will use fuel while traveling.


The game has a simple graphic - 16x16 sprites. Each planet has the same sprite, but their color is determined by their type. Each planet type has its own unique color. Orbits of the planets are displayed by using the line renderer.
    
