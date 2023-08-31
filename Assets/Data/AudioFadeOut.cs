using System.Collections;
using UnityEngine;

namespace Assets.Data
{
    public static class AudioFadeOut
    {
        public static IEnumerator FadeOut(AudioSource audioSource, float fadeTime)
        {
            var startVolume = audioSource.volume;

            while (audioSource.volume > 0)
            {
                audioSource.volume -= startVolume * Time.deltaTime / fadeTime;

                yield return null;
            }

            audioSource.Stop();
            audioSource.volume = startVolume;
        }

        public static IEnumerator FadeIn(AudioSource audioSource, float fadeTime)
        {
            var startVolume = 0.2f;

            audioSource.volume = 0;
            audioSource.Play();

            while (audioSource.volume < 1.0f)
            {
                audioSource.volume += startVolume * Time.deltaTime / fadeTime;

                yield return null;
            }

            audioSource.volume = 1f;
        }
    }
}