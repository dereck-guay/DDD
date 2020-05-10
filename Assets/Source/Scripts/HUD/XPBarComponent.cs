using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class XPBarComponent : MonoBehaviour
{
    public Text barText;
    public Text levelText;
    public Slider xpSlider;
    public Gradient gradient;
    public Image fill;

    public void GainXP(float xp, float max, int level)
    {
        xpSlider.maxValue = max;
        barText.text = xp.ToString("0 / ") + xpSlider.maxValue;
        xpSlider.value = xp;
        levelText.text = level.ToString();

        fill.color = gradient.Evaluate(xpSlider.normalizedValue);
    }
}
