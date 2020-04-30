using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Miscellaneous;
using System.Text;
using Interfaces;
using System.Linq;
public class FighterComponent : PlayerMonoBehaviour
{
    public Camera camera;

    #region Stuff for inspector

    [Serializable]
    public class AutoAttack
    {
        public GameObject autoAttackPrefab;
    };
    [Serializable]
    public class Slam 
    {
        public float manaCost;
        public float[] cooldowns;
        public float range;
        public float shockWaveRadius;
        public float damage;
        public float knockbackForce;
        public LayerMask hitLayers;
    };
    [Serializable]
    public class Rage
    {
        public float manaCost;
        public float[] cooldowns;
        public float[] atkSpeedValues;
    };
    [Serializable]
    public class TakeABreather
    {
        public float manaCost;
        public float[] cooldowns;
        public float[] regenValues;
    };
    [Serializable]
    public class Shield
    {
        public float manaCost;
        public GameObject shieldObject;
        public float[] cooldowns;
        public float[] effectiveTimes;
    };
    

    
    

    [Header("Spell Settings")]
    public AutoAttack autoAttack;
    public Slam slam;
    public Rage rage;
    public TakeABreather takeABreather;
    public Shield shield;
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

    [HideInInspector]
    public bool spellLocked = false;
    private Rigidbody rigidBody;
    private void Awake()
    {
        canAttack = true;
    }
    private void Start()
    {
        entityStats = GetComponent<Stats>();
        entityStats.ApplyStats(statsInit);

        shield.shieldObject.SetActive(false);
        rigidBody = GetComponentInChildren<Rigidbody>();
        SetTimeSinceLastAttack(0);
    }

    private void Update()
    {
        SetTimeSinceLastAttack(GetTimeSinceLastAttack() + Time.deltaTime);
        if (!(IsStunned || spellLocked))
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

    // Makes the character face the direction of the mouse
    void DirectCharacter() 
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
            if (CanCast(slam.manaCost, typeof(SlamSpell)))
            {
                var slamSpell = gameObject.AddComponent<SlamSpell>();
                slamSpell.range = slam.range;
                slamSpell.shockWaveRadius = slam.shockWaveRadius;
                slamSpell.hitLayers = slam.hitLayers;
                slamSpell.damage = slam.damage;
                slamSpell.knockbackForce = slam.knockbackForce;
                slamSpell.landingPosition = GetMousePositionOn2DPlane();
                slamSpell.cooldown = slam.cooldowns[entityStats.XP.Level - 1];
                slamSpell.Cast();
            }
        }
        if (Input.GetKey(KeybindManager.Instance.ActionBinds["ACT2"]))
        {
            if (CanCast(rage.manaCost, typeof(RageSpell)) && !IsOnCooldown(typeof(TakeABreatherSpell)))
            {
                var rageSpell = gameObject.AddComponent<RageSpell>();
                rageSpell.cooldown = rage.cooldowns[entityStats.XP.Level - 1];
                rageSpell.atkSpeedValue = rage.atkSpeedValues[entityStats.XP.Level - 1];
                rageSpell.Cast();
            }
        }
        if (Input.GetKey(KeybindManager.Instance.ActionBinds["ACT3"]))
        {
            if (!IsOnCooldown(typeof(RageSpell)) && CanCast(takeABreather.manaCost, typeof(TakeABreatherSpell)))
            {
                var takeABreatherSpell = gameObject.AddComponent<TakeABreatherSpell>();
                takeABreatherSpell.cooldown = takeABreather.cooldowns[entityStats.XP.Level - 1];
                takeABreatherSpell.regenValue = takeABreather.regenValues[entityStats.XP.Level - 1];
                takeABreatherSpell.Cast();
            }
        }
        if (Input.GetKey(KeybindManager.Instance.ActionBinds["ACT4"]))
        {
            if (CanCast(shield.manaCost, typeof(ShieldSpell)))
            {
                var shieldSpell = gameObject.AddComponent<ShieldSpell>();
                shieldSpell.shield = shield.shieldObject;
                shieldSpell.player = gameObject;
                Debug.Log(entityStats.XP.Level);
                shieldSpell.cooldown = shield.cooldowns[entityStats.XP.Level - 1];
                shieldSpell.effectiveTime = shield.effectiveTimes[entityStats.XP.Level - 1];
                shieldSpell.Cast();
            }
        }
    }
}
