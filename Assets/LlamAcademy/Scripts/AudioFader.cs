using System.Collections;
using UnityEngine;

namespace LlamAcademy
{
    public class AudioFader : MonoBehaviour
    {
        public float FadeDuration = 2;

        [SerializeField] private float MaxVolume = 0.5f;
        [SerializeField] private AudioSource[] AudioSources;

        private int LastAudioSourceIndex;

        public void ToggleMusic()
        {
            StopAllCoroutines();
                StartCoroutine(FadeSources());
        }

        private IEnumerator FadeSources()
        {
            float time = 0;
            int newTargetIndex = LastAudioSourceIndex == 0 ? 1 : 0;
            
            AudioSource lastSource = AudioSources[LastAudioSourceIndex];
            AudioSource newSource = AudioSources[newTargetIndex];

            LastAudioSourceIndex = newTargetIndex;
            newSource.Play();

            float lastInitialVolume = lastSource.volume;
            float newInitialVolume = newSource.volume;

            while (time < 1)
            {
                lastSource.volume = Mathf.Lerp(lastInitialVolume, 0, time);
                newSource.volume = Mathf.Lerp(newInitialVolume, MaxVolume, time);

                time += Time.deltaTime * (1 / FadeDuration);
                yield return null;
            }

            lastSource.volume = 0;
            newSource.volume = MaxVolume;
            lastSource.Stop();
        }
    }
}