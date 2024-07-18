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

    public float GetHealthMultiplier()
    {
        switch (currentDifficulty)
        {
            case DifficultyLevel.Easy:
                return 0.5f;
            case DifficultyLevel.Hard:
                return 2f;
            default:
                return 1f;
        }
    }

    public float GetDamageMultiplier()
    {
        switch (currentDifficulty)
        {
            case DifficultyLevel.Easy:
                return 0.5f;
            case DifficultyLevel.Hard:
                return 2f;
            default:
                return 1f;
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
