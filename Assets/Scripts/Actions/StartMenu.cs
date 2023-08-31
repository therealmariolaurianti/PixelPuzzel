using Assets.Data;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.Scripts.Actions
{
    public class StartMenu : MonoBehaviour
    {
        public GameObject LevelSelect;

        private void Start()
        {
            LevelSelect.SetActive(false);
        }

        public void ShowLevelSelect()
        {
            LevelSelect.SetActive(true);
        }

        public void CloseLevelSelect()
        {
            LevelSelect.SetActive(false);
        }

        public void StartLevel1()
        {
            LoadLevel1();
        }

        public void StartLevel2()
        {
            SceneManager.LoadScene(2);
        }

        public void StartLevel3()
        {
            SceneManager.LoadScene(3);
        }

        public void StartLevel4()
        {
            SceneManager.LoadScene(4);
        }

        public void StartGame()
        {
            var startAudio = GetComponent<AudioSource>();
            startAudio.Play();

            Cursor.visible = false;

            Invoke(nameof(LoadLevel1), 1.5f);
        }

        private void LoadLevel1()
        {
            var sceneBuildIndex = SceneManager.GetActiveScene().buildIndex + 1;
            SceneManager.LoadScene(sceneBuildIndex);
        }

        public void Quit()
        {
            Helper.Quit(name, GetType());
        }
    }
}