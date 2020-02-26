using Enums;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatusBarsComponent : MonoBehaviour
{
    public Text barText;
    public Slider statusSlider;
    public Gradient gradient;
    public Image fill;

    public void SetMax(float amount)
    {
        statusSlider.maxValue = amount;
        SetCurrent(amount);
    }

    public void SetCurrent(float amount)
    {
        barText.text = amount.ToString("0 / ") + statusSlider.maxValue;
        statusSlider.value = amount;
        fill.color = gradient.Evaluate(statusSlider.normalizedValue);
    }
}
