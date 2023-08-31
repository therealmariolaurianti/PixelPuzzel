using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Actions
{
    public class FpsCounter : MonoBehaviour
    {
        private float count;

        private IEnumerator Start()
        {
            GUI.depth = 2;
            while (true)
            {
                count = 1f / Time.unscaledDeltaTime;
                yield return new WaitForSeconds(0.1f);
            }
        }

        private void OnGUI()
        {
            var location = new Rect(Screen.width - 85, Screen.height - 25, 85, 25);
            var text = $"FPS: {Mathf.Round(count)}";
            Texture black = Texture2D.blackTexture;
            GUI.DrawTexture(location, black, ScaleMode.StretchToFill);
            GUI.color = Color.white;
            GUI.backgroundColor = Color.clear;
            GUI.skin.label.fontSize = 18;
            GUI.Label(location, text);
        }
    }
}