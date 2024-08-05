using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadingScreen : MonoBehaviour
{
    [SerializeField] private GameObject loadingPanel;
    [SerializeField] private Slider loadingSlider;
    [SerializeField] private float loadingDuration;

    public static bool loadingBool;

    [SerializeField] private GameObject healthbarUI;
    [SerializeField] private GameObject oxygenbarUI;
    [SerializeField] private GameObject weaponUI;

    private void Start()
    {
        if (loadingPanel != null && loadingSlider != null)
        {
            loadingBool = true;
            StartCoroutine(ShowLoadingScreen());
        }
        else
        {
            Debug.LogError("Loading panel or loading slider is not assigned in the inspector.");
        }
    }

    private IEnumerator ShowLoadingScreen()
    {
        if (loadingPanel != null)
        {
            loadingPanel.SetActive(true);
        }

        float elapsedTime = 0f;
        while (elapsedTime < loadingDuration)
        {
            elapsedTime += Time.deltaTime;
            if (loadingSlider != null)
            {
                loadingSlider.value = Mathf.Clamp01(elapsedTime / loadingDuration);
            }
            yield return null;
        }

        if (loadingPanel != null)
        {
            loadingPanel.SetActive(false);

            loadingBool = false;

            healthbarUI.SetActive(true);
            oxygenbarUI.SetActive(true);
            weaponUI.SetActive(true);
        }
    }
}
