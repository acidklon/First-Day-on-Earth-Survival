using System.Collections.Generic;
using UnityEngine;

namespace Watermelon
{
    public class FishingController : MonoBehaviour
    {
        private static FishingController instance;

        [SerializeField] FishingSettings fishingSettings;

        private static List<FishingPlaceBehavior> registeredFishingPlaces = new List<FishingPlaceBehavior>();

        public void Initialise()
        {
            instance = this;
        }

        public void SpawnFishingPlaces()
        {
            if(!registeredFishingPlaces.IsNullOrEmpty())
            {
                float spawnPercent = fishingSettings.SpawnPercent;

                // Shuffle fishing places list
                registeredFishingPlaces.Shuffle();

                int filteredPlacesCount = Mathf.Clamp(Mathf.RoundToInt(registeredFishingPlaces.Count * spawnPercent), 1, registeredFishingPlaces.Count);
                for(int i = 0; i < filteredPlacesCount; i++)
                {
                    registeredFishingPlaces[i].Spawn();
                }
            }
        }

        public static void SpawnRandomFishingPlace()
        {
            int fishingPlacesCount = registeredFishingPlaces.Count;
            int randomStartPoint = Random.Range(0, fishingPlacesCount);
            for (int i = 0; i < fishingPlacesCount; i++)
            {
                int currentIndex = (randomStartPoint + i) % fishingPlacesCount;
                if (registeredFishingPlaces[currentIndex].CanBeRespawn())
                {
                    registeredFishingPlaces[currentIndex].Spawn();

                    break;
                }
            }
        }

        public static void AddFishingPlace(FishingPlaceBehavior fishingPlaceBehavior, bool spawnIfRequired = false)
        {
            registeredFishingPlaces.Add(fishingPlaceBehavior);

            if(spawnIfRequired)
            {
                int activePlacesCount = 0;
                foreach(var fishingPlace in registeredFishingPlaces)
                {
                    if(fishingPlace.IsActive)
                    {
                        activePlacesCount++;
                    }
                }

                int requiredPlacesCount = Mathf.Clamp(Mathf.RoundToInt(registeredFishingPlaces.Count * instance.fishingSettings.SpawnPercent), 1, registeredFishingPlaces.Count);
                int countDiff = requiredPlacesCount - activePlacesCount;
                if(countDiff > 0)
                {
                    for(int i = 0; i < countDiff; i++)
                    {
                        SpawnRandomFishingPlace();
                    }
                }
            }
        }

        public static void RemoveFishingPlace(FishingPlaceBehavior fishingPlaceBehavior)
        {
            registeredFishingPlaces.Remove(fishingPlaceBehavior);
        }

        public static void Unload()
        {
            registeredFishingPlaces.Clear();
        }
    }
}