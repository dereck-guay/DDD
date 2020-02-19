using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Miscellaneous;
using System.Text;
using Interfaces;

[RequireComponent(typeof(AtkDamage))]
[RequireComponent(typeof(AtkSpeed))]
[RequireComponent(typeof(HP))]
[RequireComponent(typeof(Mana))]
[RequireComponent(typeof(Speed))]
[RequireComponent(typeof(XP))]
public class WizardComponent : PlayerMonoBehaviour
{
    public KeyCode[] inputs;
    private KeyBindings keyBindings;
    private IModifiableStat[] characterStats;

    // Spell Attr
    [Header("Spell Settings")]

    [Header("Fireball Spell")]
    public GameObject fireballPrefab;

    [Header("Slow Spell")]
    public float slowValue;

    // Character Stats
    private AtkSpeed AtkSpeed;
    private AtkDamage AtkDamage;
    private HP HP;
    private Mana Mana;
    private Speed Speed;
    private XP XP;

    private void Awake()
    {
        AtkDamage = GetComponent<AtkDamage>();
        AtkSpeed = GetComponent<AtkSpeed>();
        HP = GetComponent<HP>();
        Mana = GetComponent<Mana>();
        Speed = GetComponent<Speed>();
        XP = GetComponent<XP>();

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
                        fireballSpell.fireballPrefab = fireballPrefab;
                        fireballSpell.direction = GetMouseDirection();
                        fireballSpell.Cast();
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
                            slowSpell.slowValue = slowValue;
                            slowSpell.Cast();
                        } else { Destroy(target); }
                    }
                }
            }, inputs
        );
    }

    private void Start() => ApplyAllStats();

    private void ApplyAllStats()
    {
        AtkSpeed.ApplyStats(15);
        AtkDamage.ApplyStats(10);
        HP.ApplyStats(20, 1);
        Mana.ApplyStats(100, 5);
        Speed.ApplyStats(27);
        XP.Current = 0;
    }

    private void Move(Vector3 direction)
    {
        transform.LookAt(transform.position + direction);
        transform.Translate(direction * Speed.Current * Time.deltaTime, Space.World);
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