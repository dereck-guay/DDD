using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashSpell : MonoBehaviour
{
    public float cooldown = 1;
    public PlayerMonoBehaviour player;
    private float currentLifeTime;
    private float effectDuration;
    public void Cast(Vector3 direction, float multiplier)
    {
        player.gameObject.AddComponent<StraightProjectile>().direction = direction;
        player.gameObject.GetComponent<StraightProjectile>().speed = 10 * multiplier;
    }

    private void Update()
    {
        if (currentLifeTime >= cooldown)
        {
            Destroy(player.gameObject.GetComponent<StraightProjectile>());
            Destroy(this);
        }
        currentLifeTime += Time.deltaTime;
    }
}
