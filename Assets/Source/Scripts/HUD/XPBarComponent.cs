using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class XPBarComponent : MonoBehaviour
{
    public Slider xpSlider;
    public Gradient gradient;
    public Image fill;

    public void GainXP(int xp)
    {
        xpSlider.value = xp;

        fill.color = gradient.Evaluate(xpSlider.normalizedValue);
    }
}
