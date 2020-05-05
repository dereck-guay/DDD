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
    public Camera camera;

    #region Stuff for inspector

    [Serializable]
    public class AutoAttack
    {
        public GameObject autoAttackPrefab;
        public GameObject staff;
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
    #region Auto-attack stuff
    private bool canAttack;
    private float timeSinceLastAttack;
    private float GetTimeSinceLastAttack()
    { return timeSinceLastAttack; }
    private void SetTimeSinceLastAttack(float value)
    {
        if (value > 1 / entityStats.AtkSpeed.Current)
            canAttack = true;
        timeSinceLastAttack = value;
    }
    #endregion

    private Rigidbody rigidBody;
    private void Awake()
    {
        canAttack = true;
    }

    private void Start()
    {
        entityStats = GetComponent<Stats>();
        entityStats.ApplyStats(statsInit);

        rigidBody = GetComponent<Rigidbody>();
        SetTimeSinceLastAttack(0);

        statusBars[0].SetMax(entityStats.HP.Base);
        statusBars[1].SetMax(entityStats.Mana.Base);
        entityStats.HP.OnTakeDamage += damage => statusBars[0].SetCurrent(entityStats.HP.Current);
        entityStats.HP.OnHeal += regen => statusBars[0].SetCurrent(entityStats.HP.Current);
        entityStats.Mana.OnUse += manaCost => statusBars[1].SetCurrent(entityStats.Mana.Current);
        entityStats.Mana.OnRegen += manaCost => statusBars[1].SetCurrent(entityStats.Mana.Current);

        entityStats.HP.OnDeath += () => Respawn();
    }
    private void Update()
    {
        //autoAttack.staff.transform.rotation = Quaternion.Euler(transform.rotation.x + 5, transform.rotation.y, transform.rotation.z);
        SetTimeSinceLastAttack(GetTimeSinceLastAttack() + Time.deltaTime);
        if (!IsStunned)
        {
            DirectCharacter();
            GetInput();
        }
        entityStats.Regen();

        camera.transform.position = new Vector3(
            transform.position.x,
            camera.transform.position.y,
            transform.position.z - 5
        ); // Moves the camera according to the player.
   }

    private void Move(Vector3 direction)
    {
        if(!IsStunned)
            rigidBody.AddForce(direction * entityStats.Speed.Current * Time.deltaTime * 100f);
    }
    void DirectCharacter() //make the character face the direction of the mouse
    {
        var directionToLookAt = transform.position + GetMouseDirection();
        directionToLookAt.y = transform.position.y;
        transform.LookAt(directionToLookAt);
    }

    private void GetInput()
    {
        //Movement
        if (Input.GetKey(KeybindManager.Instance.KeyBinds["UP"]))
        {
            Move(Vector3.forward);
        }
        if (Input.GetKey(KeybindManager.Instance.KeyBinds["LEFT"]))
        {
            Move(Vector3.left);
        }
        if (Input.GetKey(KeybindManager.Instance.KeyBinds["DOWN"]))
        {
            Move(Vector3.back);
        }
        if (Input.GetKey(KeybindManager.Instance.KeyBinds["RIGHT"]))
        {
            Move(Vector3.right);
        }
        //Actions
        if (Input.GetKey(KeybindManager.Instance.ActionBinds["ACTAA"]))
        {
            if (canAttack)
            {
                var target = GetEntityAtMousePosition();
                if (ExistsAndIsntSelf(target) && TargetIsWithinRange(target, entityStats.Range.Current))
                {
                    autoAttack.staff.AddComponent<StaffRotationComponent>();
                    Debug.Log("auto has been cast");
                    var autoAttackSpell = gameObject.AddComponent<RangedAutoAttackSpell>();
                    autoAttackSpell.autoAttackPrefab = autoAttack.autoAttackPrefab;
                    autoAttackSpell.damage = entityStats.AtkDamage.Current;
                    autoAttackSpell.target = target.GetComponent<Transform>().gameObject;
                    autoAttackSpell.Cast(entityStats.AtkSpeed.Current, transform.position);
                    SetTimeSinceLastAttack(0);
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