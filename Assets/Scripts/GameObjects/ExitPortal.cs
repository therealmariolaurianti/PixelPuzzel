using Assets.Data;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.Scripts.GameObjects
{
    public class ExitPortal : MonoBehaviour
    {
        private Animator _animator;
        public BoxCollider2D BoxCollider;

        public GameObject Player;
        public AudioSource PortalIdleSound;

        private void OnEnable()
        {
            CharacterEvents.PortalOpened += OnPortalOpened;
        }

        private void OnDisable()
        {
            CharacterEvents.PortalOpened -= OnPortalOpened;
        }

        private void Awake()
        {
            _animator = GetComponent<Animator>();
        }

        private void OnPortalOpened()
        {
            _animator.SetTrigger(AnimationStrings.OpenPortal);
            BoxCollider.enabled = true;

            AudioSource.PlayClipAtPoint(PortalIdleSound.clip, gameObject.transform.position, PortalIdleSound.volume);
        }

        public void OnTriggerEnter2D(Collider2D collision)
        {
            Destroy(Player);
            _animator.SetTrigger(AnimationStrings.ClosePortal);

            CharacterEvents.StopTimer.Invoke();

            Invoke(nameof(LoadNextLevel), 2f);
        }

        private void LoadNextLevel()
        {
            var nextLevelIndex = SceneManager.GetActiveScene().buildIndex + 1;
            SceneManager.LoadScene(nextLevelIndex);
        }
    }
}