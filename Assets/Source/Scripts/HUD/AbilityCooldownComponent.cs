using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AbilityCooldownComponent : MonoBehaviour
{
    public float cooldownTime;
    private float nextFireTime;
    public Image mask;

    void Update()
    {
        mask.fillAmount = (nextFireTime / cooldownTime);

        if (Input.GetMouseButtonDown(0) && nextFireTime == 0)
            nextFireTime = cooldownTime;
        if (nextFireTime <= 0)
            nextFireTime = 0;
        else if (nextFireTime > 0)
            nextFireTime -= Time.deltaTime;
    }
}
