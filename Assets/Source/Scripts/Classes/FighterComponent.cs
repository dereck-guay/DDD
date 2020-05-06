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
                    var autoAttackSpell = gameObject.AddComponent<RangedAutoAttackSpell>();
                    autoAttackSpell.autoAttackPrefab = autoAttack.autoAttackPrefab;
                    autoAttackSpell.damage = entityStats.AtkDamage.Current;
                    autoAttackSpell.target = target.GetComponent<Transform>().gameObject;
                    autoAttackSpell.Cast(entityStats.AtkSpeed.Current, transform.position);
                    TimeSinceLastAttack = 0;
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
                slamSpell.caster = this;
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
