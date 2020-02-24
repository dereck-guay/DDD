using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Miscellaneous;
using System.Text;
using Interfaces;
[RequireComponent(typeof(Stats))]
public class FighterComponent : PlayerMonoBehaviour
{
    [System.Serializable]
    public class StatsInit
    {
        public float attackDamage;
        public float attackSpeed;
        public float maxHp;
        public float hpRegen;
        public float maxMana;
        public float manaRegen;
        public float range;
        public float speed;

    };
    [Header("Stats")]
    public StatsInit statsInit;

    [Header("Inputs")]
    public KeyCode[] inputs;

    [HideInInspector]
    public bool spellLocked = false;

    
    [System.Serializable]
    public class Slam {
        public float range;
    };

    [Header("Spell Settings")]
    public Slam slam;

    private KeyBindings keyBindings;
    private Rigidbody rigidBody;
    private void Awake()
    {
        keyBindings = new KeyBindings(
            new Action[]
            {
                () => Move(Vector3.forward),
                () => Move(Vector3.left),
                () => Move(Vector3.back),
                () => Move(Vector3.right),
                () => {
                    if (! IsOnCooldown(typeof(SlamSpell)))
                    {
                        var slamSpell = gameObject.AddComponent<SlamSpell>();
                        slamSpell.range = slam.range;
                        slamSpell.position = GetMousePositionOn2DPlane();
                        slamSpell.Cast(entityStats.XP.Level);
                        // Pour pas que le player puisse bouger pendant l'ainimation du spell
                        // spellLocked = true;
                    }
                }
            }, inputs
        );
    }
    private void Start()
    {
        rigidBody = GetComponentInChildren<Rigidbody>();
        entityStats = GetComponent<Stats>();
        entityStats.ApplyStats(statsInit.attackDamage, statsInit.attackSpeed, statsInit.maxHp, statsInit.hpRegen, statsInit.maxMana, statsInit.manaRegen, statsInit.range, statsInit.speed);
    }

    private void Update()
    {
        if (!spellLocked)
        {
            var directionToLookAt = transform.position + GetMouseDirection();
            directionToLookAt.y = transform.position.y;
            transform.LookAt(directionToLookAt);
            rigidBody.velocity = Vector3.zero;
            keyBindings.CallBindings();
        }
    }

    private void Move(Vector3 direction)
    {
        transform.LookAt(transform.position + direction);
        transform.Translate(direction * entityStats.Speed.Current * Time.deltaTime, Space.World);
    }
}
