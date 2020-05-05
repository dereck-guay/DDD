using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//The spell instantiates a few daggers in a cone facing a direction
//The daggers are controlled by the StraightProjectile and DaggerCollision components
public class FanOfKnivesSpell : SpellMonoBehavior
{
    public Vector3 direction;
    public GameObject daggerPrebab;
    public float damage;
    public PlayerMonoBehaviour caster;
    private GameObject[] daggers = new GameObject[5];

    public void Cast()
    {
        var spawnPosition = transform.position + 1.5f * direction;
        var directions = GenerateDirections();
        for (int i = 0; i < directions.Length; ++i)
        {
            var dagger = Instantiate(daggerPrebab, spawnPosition, Quaternion.Euler(directions[i]));
            dagger.GetComponent<DaggerCollision>().damage = damage;
            dagger.GetComponent<DaggerCollision>().caster = caster;
            daggers[i] = dagger;
        }
    }

    void Update()
    {
        if (currentLifeTime >= cooldown)
        {
            Destroy(this);
        }

        currentLifeTime += Time.deltaTime;
    }
    private Vector3[] GenerateDirections()
    {
        const int NOFDIRECTIONS = 5;
        Vector3[] directions = new Vector3[NOFDIRECTIONS]
        {
            new Vector3(0, 0, 0),
            new Vector3(0, 10, 0),
            new Vector3(0, 20, 0),
            new Vector3(0, -10, 0),
            new Vector3(0, -20, 0),
        };
        var originalDirection = transform.eulerAngles;
        for (int i = 0; i < NOFDIRECTIONS; ++i)
            directions[i] += originalDirection;

        return directions;
    }
}
