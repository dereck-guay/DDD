using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class XPBar : MonoBehaviour
{
    public Slider xpSlider;
    public Gradient gradient;
    public Image fill;

    public void SetXP(float xp)
    {
        xpSlider.value = xp;

        fill.color = gradient.Evaluate(xpSlider.normalizedValue);
    }
}
