using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Manager
{
    public class MusicManager : MonoBehaviour
    {
        [Header("Music Tracks")]
        public AudioClip menuMusic;
        public AudioClip gameMusic;

        [Header("Sound Effects")]
        public AudioClip buttonClickSound;
        // Add more sound effects as needed

        private AudioSource musicAudioSource;
        private AudioSource soundEffectAudioSource;

        private static MusicManager instance;
        public static MusicManager Instance => instance;

        void Awake()
        {
            if (instance != null && instance != this)
            {
                Destroy(gameObject);
                return;
            }

            instance = this;
            DontDestroyOnLoad(gameObject);

            // Initialize audio sources
            musicAudioSource = gameObject.AddComponent<AudioSource>();
            soundEffectAudioSource = gameObject.AddComponent<AudioSource>();
        }

        public void PlayMusic(AudioClip clip)
        {
            if (musicAudioSource.isPlaying)
                musicAudioSource.Stop();

            musicAudioSource.clip = clip;
            musicAudioSource.loop = true; // Loop background music
            musicAudioSource.Play();
        }

        public void PlaySoundEffect(AudioClip clip)
        {
            soundEffectAudioSource.PlayOneShot(clip);
        }

        public void SetMusicVolume(float volume)
        {
            musicAudioSource.volume = Mathf.Clamp01(volume);
        }
        public float GetMusicVolume()
        {
            return musicAudioSource.volume;
        }

        public void SetSoundEffectVolume(float volume)
        {
            soundEffectAudioSource.volume = Mathf.Clamp01(volume);
        }


        // Example methods to play specific tracks
        public void PlayMenuMusic()
        {
            PlayMusic(menuMusic);
        }

        public void PlayGameMusic()
        {
            PlayMusic(gameMusic);
        }

        public void PlayButtonClickSound()
        {
            PlaySoundEffect(buttonClickSound);
        }
    }

}
