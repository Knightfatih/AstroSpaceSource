using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOver : MonoBehaviour
{
    public static bool GameIsPaused = false;

    public GameObject gameoverUI;

    public bool playerDeath { get; set; }



    void Update()
    {
        if (playerDeath)
        {
            gameoverUI.SetActive(true);
            Time.timeScale = 0f;
            GameIsPaused = true;

        }
    }

}
