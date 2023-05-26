using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class YouWon : MonoBehaviour
{
    public static bool GameIsPaused = false;
    public bool bossDeath { get; set; }
    public GameObject youwonUI;


    private void Awake()
    {
        Time.timeScale = 1f;
    }

    // Update is called once per frame
    void Update()
    {
        if (bossDeath)
        {
            youwonUI.SetActive(true);
            Time.timeScale = 0f;
            GameIsPaused = true;

        }
    }
}
