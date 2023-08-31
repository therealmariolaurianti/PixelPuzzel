using Assets.Data;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.Scripts.Actions
{
    public class StartMenu : MonoBehaviour
    {
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