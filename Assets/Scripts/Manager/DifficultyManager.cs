using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum DifficultyLevel
{
    Easy,
    Normal,
    Hard
}

public class DifficultyManager : MonoBehaviour
{
    public static DifficultyManager Instance { get; private set; }

    public DifficultyLevel currentDifficulty = DifficultyLevel.Normal;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    public int GetMaxHealth()
    {
        switch (currentDifficulty)
        {
            case DifficultyLevel.Easy:
                return 100;
            case DifficultyLevel.Hard:
                return 300;
            default:
                return 200;
        }
    }

    public float GetDamageMultiplier()
    {
        switch (currentDifficulty)
        {
            case DifficultyLevel.Easy:
                return 10;
            case DifficultyLevel.Hard:
                return 30;
            default:
                return 20;
        }
    }

    public void AdjustLightingBasedOnDifficulty()
    {
        switch (currentDifficulty)
        {
            case DifficultyLevel.Easy:
                // Set lighting for easy mode
                break;
            case DifficultyLevel.Normal:
                // Set lighting for normal mode
                break;
            case DifficultyLevel.Hard:
                // Set lighting for hard mode
                break;
        }
    }
}
