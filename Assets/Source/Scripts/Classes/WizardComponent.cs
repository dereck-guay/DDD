using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Miscellaneous;
using System.Text;
using Interfaces;
public class WizardComponent : PlayerMonoBehaviour
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

    [System.Serializable]
    public class Fireball
    {
        public GameObject fireballPrefab;
    };
    [System.Serializable]
    public class Slow
    {
        public float slowValue;
    };
    [System.Serializable]
    public class Heal
    {
        public float[] healValues;
    };

    [Header("Spell Settings")]
    public Fireball fireball;
    public Slow slow;
    public Heal heal;

    private KeyBindings keyBindings;
    private void Awake()
    {
        stats = new Stats(
                statsInit.attackDamage,
                statsInit.attackSpeed,
                statsInit.maxHp,
                statsInit.hpRegen,
                statsInit.maxMana,
                statsInit.manaRegen,
                statsInit.range,
                statsInit.speed
        );
        keyBindings = new KeyBindings(
            new Action[]
            {
                () => AutoAttack(),
                () => Move(Vector3.forward),
                () => Move(Vector3.left),
                () => Move(Vector3.back),
                () => Move(Vector3.right),
                () => {
                   if (! IsOnCooldown(typeof(FireballSpell)))
                    {
                        var fireballSpell = gameObject.AddComponent<FireballSpell>();
                        fireballSpell.fireballPrefab = fireball.fireballPrefab;
                        fireballSpell.direction = GetMouseDirection();
                        fireballSpell.Cast(stats.XP.Level);
                    }
                },
                () => {
                    if (! IsOnCooldown(typeof(SlowSpell)))
                    {
                        var slowSpell = gameObject.AddComponent<SlowSpell>();
                        var target = GetEntityAtMousePosition();
                        if (target) // target != null
                        {
                            // Get parent object because the collider is in the body of the character.
                            slowSpell.target = target.transform.parent.gameObject;
                            slowSpell.slowValue = slow.slowValue;
                            slowSpell.Cast(stats.XP.Level);
                        } else { Destroy(target); }
                    }
                },
                () => {
                    if (! IsOnCooldown(typeof(HealSpell)))
                    {
                        var healSpell = gameObject.AddComponent<HealSpell>();
                        var target = GetEntityAtMousePosition();
                        if (target) // target != null
                        {
                            // Get parent object because the collider is in the body of the character.
                            healSpell.target = target.transform.parent.gameObject;
                            healSpell.healValue = heal.healValues;
                            healSpell.Cast(stats.XP.Level);
                        } else { Destroy(target); }
                    }
                },
                () => {
                    Debug.Log("3");
                },
                () => {
                    Debug.Log("4");
                }
            }, inputs
        );
    }

    private void Move(Vector3 direction)
    {
        transform.LookAt(transform.position + direction);
        transform.Translate(direction * stats.Speed.Current * Time.deltaTime, Space.World);
    }

    private void AutoAttack()
    {
        var target = GetEntityAtMousePosition();
        if (target)
        {
            Debug.Log(target.transform.parent.gameObject.name);
        }
    }

    private void Update() => keyBindings.CallBindings();
}