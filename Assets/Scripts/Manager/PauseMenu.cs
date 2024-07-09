using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace UI
{
    public class PauseMenu : MonoBehaviour
    {
        [SerializeField] private GameObject _pauseMenuUI;
        [SerializeField] private string _mainMenuSceneName = "Main Menu";

        private static bool _gameIsPaused;

        public static bool GameIsPaused
        {
            get { return _gameIsPaused; }
            private set { _gameIsPaused = value; }
        }

        void Update()
        {
            // Toggle pause state when the Escape key is pressed
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                if (GameIsPaused)
                {
                    Resume();
                }
                else
                {
                    Pause();
                }
            }
        }

        private void Resume()
        {
            // Deactivate pause menu UI and resume game time
            if (_pauseMenuUI != null)
            {
                _pauseMenuUI.SetActive(false);
            }

            Time.timeScale = 1f;
            GameIsPaused = false;
        }

        private void Pause()
        {
            // Activate pause menu UI and pause game time
            if (_pauseMenuUI != null)
            {
                _pauseMenuUI.SetActive(true);
            }

            Time.timeScale = 0f;
            GameIsPaused = true;
        }

        public void LoadMenu()
        {
            // Load the main menu scene
            Time.timeScale = 1f;
            GameIsPaused = false;
            SceneManager.LoadScene(_mainMenuSceneName);
        }

        public void ExitGame()
        {
            // Log and handle game exit
            Debug.Log("Exiting game");
            Application.Quit();
        }

    }

}
