using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OxygenBar : MonoBehaviour
{
    public Slider slider;
    public Gradient gradient;
    public Image fill;

    public void SetMaxOxygen(float maxOxygen)
    {
        slider.maxValue = maxOxygen;
        slider.value = maxOxygen;

        fill.color = gradient.Evaluate(1f);
    }

    public void SetOxygen(float oxygen)
    {
        slider.value = oxygen;
        fill.color = gradient.Evaluate(slider.normalizedValue);
    }
}
