using Assets.Data;
using UnityEngine;

namespace Assets.Scripts.Actions
{
    public class MelonPickup : MonoBehaviour
    {
        private Animator _animator;
        public AudioSource AudioSource;

        private void Awake()
        {
            _animator = GetComponent<Animator>();
        }

        public void OnTriggerEnter2D(Collider2D collision)
        {
            _animator.SetTrigger(AnimationStrings.Collected);
            AudioSource.PlayClipAtPoint(AudioSource.clip, gameObject.transform.position, AudioSource.volume);
        }

        public void Kill()
        {
            Destroy(gameObject);
            CharacterEvents.MelonCollected?.Invoke();
            CharacterEvents.Spawn?.Invoke();
        }
    }
}