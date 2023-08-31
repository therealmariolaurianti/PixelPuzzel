using System;
using System.Reflection;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.Data
{
    public static class Helper
    {
        public static Level GetLevel()
        {
            var scene = SceneManager.GetActiveScene().name;
            return scene switch
            {
                "Level_1" => Level.One,
                "Level_2" => Level.Two,
                "Level_3" => Level.Three,
                "Level_4" => Level.Four,
                _ => Level.One
            };
        }

        public static int GetLevelInt()
        {
            var scene = SceneManager.GetActiveScene().name;
            return scene switch
            {
                "Level_1" => 1,
                "Level_2" => 2,
                "Level_3" => 3,
                "Level_4" => 4,
                _ => 1
            };
        }

        public static string TimeFromFloat(float value)
        {
            return TimeSpan.FromSeconds(value).ToString("mm':'ss':'ff");
        }

        public static void Quit(string name, Type type)
        {
            DataWriter.Clear();

#if (UNITY_EDITOR || DEVELOPMENT_BUILD)
            Debug.Log($"{name}, {type}, {MethodBase.GetCurrentMethod()?.Name}");
#endif
#if (UNITY_EDITOR)
            EditorApplication.isPlaying = false;
#elif (UNITY_STANDALONE)
                Application.Quit();
#elif (UNITY_WEBGL)
                SceneManager.LoadScene("QuitScene")
#endif
        }
    }
}