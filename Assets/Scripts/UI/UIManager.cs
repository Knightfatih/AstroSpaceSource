using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Manager
{
    public class UIManager : MonoBehaviour
    {
        // UI Components
        public Slider musicVolumeSlider;
        public Slider soundVolumeSlider;
        public TMP_Dropdown displayDropdown;
        public TMP_Dropdown resolutionDropdown;

        private MusicManager _musicManager;

        private void Start()
        {
            InitializeResolutionDropdown();
            InitializeFullscreenDropdown();

            if (_musicManager != null)
            {
                musicVolumeSlider.value = _musicManager.GetMusicVolume();
            }
        }

        private void InitializeResolutionDropdown()
        {
            resolutionDropdown.ClearOptions();
            List<TMP_Dropdown.OptionData> options = new List<TMP_Dropdown.OptionData>();
            int currentResolutionIndex = 0;

            for (int i = 0; i < Screen.resolutions.Length; i++)
            {
                Resolution resolution = Screen.resolutions[i];
                string optionText = resolution.width + "x" + resolution.height;
                options.Add(new TMP_Dropdown.OptionData(optionText));

                if (resolution.width == Screen.currentResolution.width &&
                    resolution.height == Screen.currentResolution.height)
                {
                    currentResolutionIndex = i;
                    Debug.Log("Current resolution set to: " + optionText);
                }
            }

            resolutionDropdown.AddOptions(options);
            resolutionDropdown.value = currentResolutionIndex;
            resolutionDropdown.RefreshShownValue();
            resolutionDropdown.onValueChanged.AddListener(SetResolution);
        }

        private void InitializeFullscreenDropdown()
        {
            displayDropdown.ClearOptions();
            List<TMP_Dropdown.OptionData> options = new List<TMP_Dropdown.OptionData>
            {
                new TMP_Dropdown.OptionData("Fullscreen"),
                new TMP_Dropdown.OptionData("Windowed")
            };

            displayDropdown.AddOptions(options);
            displayDropdown.value = Screen.fullScreen ? 0 : 1;
            displayDropdown.RefreshShownValue();
            displayDropdown.onValueChanged.AddListener(SetFullscreen);

            Debug.Log("Fullscreen option set to: " + (Screen.fullScreen ? "Fullscreen" : "Windowed"));
        }

        public void SetResolution(int resolutionIndex)
        {
            Resolution resolution = Screen.resolutions[resolutionIndex];
            Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
        }

        public void SetFullscreen(int fullscreenIndex)
        {
            Screen.fullScreen = fullscreenIndex == 0;
        }

        public void LoadScene(string sceneName)
        {
            SceneManager.LoadScene(sceneName);
        }

        public void LoadScene(int sceneBuildIndex)
        {
            SceneManager.LoadScene(sceneBuildIndex);
        }

        public void SetMusicVolume(float value)
        {
            if (_musicManager != null)
            {
                _musicManager.SetMusicVolume(value);
                PlayerPrefs.SetFloat("MusicVolume", value);
                PlayerPrefs.Save();
            }
        }

        public void SetSoundVolume(float value)
        {
            // Implementation depends on your sound management system
            // Placeholder for future implementation
        }

        public void QuitGame()
        {
            Debug.Log("Exit");
            Application.Quit();
        }

        public void SetEasyDifficulty()
        {
            DifficultyManager.Instance.currentDifficulty = DifficultyLevel.Easy;
            Debug.Log("Difficulty set to Easy");
            StartGame();
        }

        public void SetNormalDifficulty()
        {
            DifficultyManager.Instance.currentDifficulty = DifficultyLevel.Normal;
            Debug.Log("Difficulty set to Normal");
            StartGame();
        }

        public void SetHardDifficulty()
        {
            DifficultyManager.Instance.currentDifficulty = DifficultyLevel.Hard;
            Debug.Log("Difficulty set to Hard");
            StartGame();
        }

        private void StartGame()
        {
            SceneManager.LoadScene("Game");
        }
    }

}
