using Assets.Data;
using UnityEngine;

namespace Assets.Scripts.Enemies
{
    public class Rhino : MonoBehaviour
    {
        private Animator _animator;

        private void Awake()
        {
            _animator = GetComponent<Animator>();
            CharacterEvents.AllItemsCollected += OnAllItemsCollected;
        }

        private void OnAllItemsCollected()
        {
            CharacterEvents.AllItemsCollected -= OnAllItemsCollected;
            Destroy(gameObject);
        }
    }
}