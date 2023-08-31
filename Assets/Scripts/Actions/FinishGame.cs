using Assets.Data;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.Scripts.Actions
{
    public class FinishGame : MonoBehaviour
    {
        public AudioSource BackgroundMusic;
        public GameObject Fireworks;

        private void Awake()
        {
            Fireworks.SetActive(false);
            gameObject.SetActive(false);

            CharacterEvents.AllItemsCollected += OnAllItemsCollected;
        }

        private void OnAllItemsCollected()
        {
            gameObject.SetActive(true);
        }

        public void OnTriggerEnter2D(Collider2D collision)
        {
            BackgroundMusic.Stop();
            Fireworks.SetActive(true);

            var finishAudio = gameObject.GetComponent<AudioSource>();
            finishAudio.Play();

            Invoke(nameof(LoadEndGameScene), 3f);
        }

        private void LoadEndGameScene()
        {
            var nextLevelIndex = SceneManager.GetActiveScene().buildIndex + 1;
            SceneManager.LoadScene(nextLevelIndex);
        }
    }
}