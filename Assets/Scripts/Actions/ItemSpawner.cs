using System.Collections.Generic;
using System.Linq;
using Assets.Data;
using UnityEngine;

namespace Assets.Scripts.Actions
{
    public class ItemSpawner : MonoBehaviour
    {
        private readonly Dictionary<int, Transform> _locationIndexes = new();
        public int CurrentLocationIndex = 1;
        public int MaxSpawned;

        public GameObject Item;
        public List<Transform> Locations;
        private int _currentSpawned;

        private Transform FirstLocation => Locations?.FirstOrDefault();

        private void Start()
        {
            if (Locations?.Any() ?? false)
            {
                foreach (var location in Locations)
                {
                    var index = Locations.IndexOf(location);
                    _locationIndexes[index] = location;
                }

                CharacterEvents.Spawn += OnSpawnItem;

                Spawn(Locations.Last());
            }
        }

        private void OnSpawnItem()
        {
            if (MaxSpawned == _currentSpawned)
                return;
            _locationIndexes.TryGetValue(CurrentLocationIndex + 1, out var nextLocation);
            if (nextLocation == null)
            {
                nextLocation = FirstLocation;
                CurrentLocationIndex = 0;
            }
            else
            {
                CurrentLocationIndex++;
            }

            Spawn(nextLocation);
        }

        private void Spawn(Transform location)
        {
            var item = Instantiate(Item, location.position, Quaternion.identity);
            item.transform.parent = gameObject.transform.parent;
            _currentSpawned++;
        }
    }
}