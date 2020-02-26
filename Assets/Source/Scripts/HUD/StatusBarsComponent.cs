using Enums;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatusBarsComponent : MonoBehaviour
{
    public Slider statusSlider;
    public Gradient gradient;
    public Image fill;

    public void SetMax(float amount)
    {
        statusSlider.maxValue = amount;
        statusSlider.value = amount;

        fill.color = gradient.Evaluate(1f);
    }

    public void SetCurrent(float amount)
    {
        statusSlider.value = amount;

        fill.color = gradient.Evaluate(statusSlider.normalizedValue);
    }
}
