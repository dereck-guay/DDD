using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Miscellaneous;
using System.Text;
using Interfaces;
[RequireComponent(typeof(Rigidbody))]
public class WizardComponent : PlayerMonoBehaviour
{
    #region Stuff for inspector

    [Serializable]
    public class AutoAttack
    {
        public GameObject autoAttackPrefab;
        public GameObject staff;
        public string audioName;
    };
    [Serializable]
    public class Fireball
    {
        public GameObject fireballPrefab;
        public float manaCost;
        public float[] cooldowns;
        public float[] damage;
    };
    [Serializable]
    public class Slow
    {
        public float manaCost;
        public float slowValue;
        public float[] cooldowns;
        public float[] effectDurations;
    };
    [Serializable]
    public class Heal
    {
        public float manaCost;
        public float[] cooldowns;
        public float[] healValues;
    };
    [Serializable]
    public class RayOfFrost
    {
        public GameObject rayOfFrostPrefab;
        public GameObject icePatchPrefab;
        public float manaCost;
        public float slowValue;
        public float[] cooldowns;
    };

    [Header("Spell Settings")]
    public AutoAttack autoAttack;
    public Fireball fireball;
    public Slow slow;
    public Heal heal;
    public RayOfFrost rayOfFrost;
    #endregion
    
    private void Start() => InitializePlayer();
    private void Update() => UpdatePlayer();

    protected override void ManageInputs()
    {
        base.ManageInputs();
        //Actions
        if (Input.GetKey(KeybindManager.Instance.ActionBinds["ACTAA"]))
        {
            if (canAttack)
            {
                var target = GetEntityAtMousePosition();
                if (ExistsAndIsntSelf(target) && TargetIsWithinRange(target, entityStats.Range.Current))
                {
                    autoAttack.staff.AddComponent<StaffRotationComponent>();
                    var autoAttackSpell = gameObject.AddComponent<RangedAutoAttackSpell>();
                    autoAttackSpell.autoAttackPrefab = autoAttack.autoAttackPrefab;
                    autoAttackSpell.damage = entityStats.AtkDamage.Current;
                    autoAttackSpell.target = target.GetComponent<Transform>().gameObject;
                    autoAttackSpell.Cast(entityStats.AtkSpeed.Current, transform.position, this, autoAttack.audioName);
                    TimeSinceLastAttack = 0;
                    canAttack = false;
                }
            }
        }
        if (Input.GetKey(KeybindManager.Instance.ActionBinds["ACT1"]))
        {
            if (CanCast(fireball.manaCost, typeof(FireballSpell)))
            {
                entityStats.Mana.UseMana(fireball.manaCost);
                var fireballSpell = gameObject.AddComponent<FireballSpell>();
                fireballSpell.fireballPrefab = fireball.fireballPrefab;
                fireballSpell.direction = GetMouseDirection();
                fireballSpell.cooldown = fireball.cooldowns[entityStats.XP.Level - 1];
                fireballSpell.damage = fireball.damage[entityStats.XP.Level - 1];
                fireballSpell.caster = this;
                fireballSpell.Cast();
            }
        }
        if (Input.GetKey(KeybindManager.Instance.ActionBinds["ACT2"]))
        {
            if (CanCast(slow.manaCost, typeof(SlowSpell)))
            {
                var target = GetEntityAtMousePosition();
                if (ExistsAndIsntSelf(target))
                {
                    entityStats.Mana.UseMana(slow.manaCost);
                    var slowSpell = gameObject.AddComponent<SlowSpell>();
                    slowSpell.target = target;
                    slowSpell.slowValue = slow.slowValue;
                    slowSpell.cooldown = slow.cooldowns[entityStats.XP.Level - 1];
                    slowSpell.Cast();
                }
            }
        }
        if (Input.GetKey(KeybindManager.Instance.ActionBinds["ACT3"]))
        {
            if (CanCast(heal.manaCost, typeof(HealSpell)))
            {
                var target = GetEntityAtMousePosition();
                if (target)
                {
                    entityStats.Mana.UseMana(heal.manaCost);
                    var healSpell = gameObject.AddComponent<HealSpell>();
                    healSpell.target = target;
                    healSpell.healValue = heal.healValues[entityStats.XP.Level];
                    healSpell.cooldown = heal.cooldowns[entityStats.XP.Level];
                    healSpell.Cast();
                }
            }
        }
        if (Input.GetKey(KeybindManager.Instance.ActionBinds["ACT4"]))
        {
            if (CanCast(rayOfFrost.manaCost, typeof(RayOfFrostSpell)))
            {
                entityStats.Mana.UseMana(rayOfFrost.manaCost);
                var rayOfFrostSpell = gameObject.AddComponent<RayOfFrostSpell>();
                rayOfFrostSpell.rayOfFrostPrefab = rayOfFrost.rayOfFrostPrefab;
                rayOfFrostSpell.icePatchManagerPrefab = rayOfFrost.icePatchPrefab;
                rayOfFrostSpell.slowValue = rayOfFrost.slowValue;
                rayOfFrostSpell.direction = GetMouseDirection();
                rayOfFrostSpell.cooldown = rayOfFrost.cooldowns[entityStats.XP.Level - 1];
                rayOfFrostSpell.Cast(this);
            }
        }
    }
}