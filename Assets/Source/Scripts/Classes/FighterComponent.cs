﻿using System;
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
public class FighterComponent : PlayerMonoBehaviour
{
    public KeyCode[] inputs;
    private KeyBindings keyBindings;
    private IModifiableStat[] characterStats;

    [HideInInspector]
    public bool spellLocked = false;

    // Spell Attr
    public float slamRange;

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
                () => Move(Vector3.forward),
                () => Move(Vector3.left),
                () => Move(Vector3.back),
                () => Move(Vector3.right),
                () => {
                    if (! IsOnCooldown(typeof(SlamSpell)))
                    {
                        var slamSpell = gameObject.AddComponent<SlamSpell>();
                        slamSpell.range = slamRange;
                        slamSpell.position = GetMousePositionOn2DPlane();
                        slamSpell.Cast();
                        // Pour pas que le player puisse bouger pendant l'ainimation du spell
                        // spellLocked = true;
                    }
                }
            }, inputs
        );
    }

    private void Start() => ApplyAllStats();
    private void Update()
    {
        if (! spellLocked)
            keyBindings.CallBindings();
    }

    private void ApplyAllStats()
    {
        AtkSpeed.ApplyStats(20);
        AtkDamage.ApplyStats(30);
        HP.ApplyStats(50, 3);
        Mana.ApplyStats(60, 2);
        Speed.ApplyStats(20);
        XP.Current = 0;
    }

    private void Move(Vector3 direction)
    {
        transform.LookAt(transform.position + direction);
        transform.Translate(direction * Speed.Current * Time.deltaTime, Space.World);
    }
}
