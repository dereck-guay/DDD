using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Miscellaneous;
using System.Text;
using Interfaces;
using System.Linq;

public class RogueComponent : PlayerMonoBehaviour
{
    #region Stuff for inspector

    [Serializable]
    public class AutoAttack
    {
        public GameObject autoAttackPrefab;
        public string audioName = "Rogue Auto Attack";
    };

    [Serializable]
    public class StunningBlade
    {
        public GameObject stunningBladePrefab;
        public float manaCost;
        public float[] cooldowns = { 7f, 5f, 4f };
        public float[] effectDurations = { 2f, 3f, 3.5f };
    };
    [Serializable]
    public class Dash
    {
        public float manaCost;
        public float[] cooldowns = { 4f, 3f, 2f };
        public float dashMultiplier;
    };
    [Serializable]
    public class SmokeScreen
    {
        public ParticleSystem smoke;
        public float manaCost;
        public float[] cooldowns = { 4f, 3f, 2f };
    };
    [Serializable]
    public class FanOfKnives
    {
        public GameObject daggerPrefab;
        public float manaCost;
        public float[] cooldowns = { 4f, 3f, 2f };
        public float damage;
    }

    [Header("Spell Settings")]
    public AutoAttack autoAttack;
    public StunningBlade stunningBlade;
    public Dash dash;
    public SmokeScreen smokeScreen;
    public FanOfKnives fanOfKnives;

    #endregion
    private bool isDashing;
    
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
                    autoAttackSpell.Cast(entityStats.AtkSpeed.Current, transform.position, this, autoAttack.audioName);
                    TimeSinceLastAttack = 0;
                    canAttack = false;
                }
            }
        }
        if (Input.GetKey(KeybindManager.Instance.ActionBinds["ACT1"]))
        {
            if (CanCast(stunningBlade.manaCost, typeof(StunningBladeSpell)))
            {
                var stunningBladeSpell = gameObject.AddComponent<StunningBladeSpell>();
                stunningBladeSpell.direction = GetMouseDirection();
                stunningBladeSpell.stunningBladePrefab = stunningBlade.stunningBladePrefab;
                stunningBladeSpell.cooldown = stunningBlade.cooldowns[entityStats.XP.Level - 1];
                stunningBladeSpell.effectDuration = stunningBlade.effectDurations[entityStats.XP.Level - 1];
                stunningBladeSpell.Cast();
            }
        }
        if (Input.GetKey(KeybindManager.Instance.ActionBinds["ACT2"]))
        {
            if (CanCast(dash.manaCost, typeof(DashSpell)))
            {
                var dashSpell = gameObject.AddComponent<DashSpell>();
                dashSpell.cooldown = dash.cooldowns[entityStats.XP.Level];
                dashSpell.player = this;
                dashSpell.Cast(GetMouseDirection(), dash.dashMultiplier);
            }
        }
        if (Input.GetKey(KeybindManager.Instance.ActionBinds["ACT3"]))
        {
            if (CanCast(smokeScreen.manaCost, typeof(SmokeScreenSpell)))
            {
                var smokeScreenSpell = gameObject.AddComponent<SmokeScreenSpell>();
                smokeScreenSpell.cooldown = smokeScreen.cooldowns[entityStats.XP.Level - 1];
                smokeScreenSpell.player = gameObject;
                smokeScreenSpell.smoke = smokeScreen.smoke;
                smokeScreenSpell.Cast();
            }
        }
        if (Input.GetKey(KeybindManager.Instance.ActionBinds["ACT4"]))
        {
            if (CanCast(fanOfKnives.manaCost, typeof(FanOfKnivesSpell)))
            {
                var fanOfKnivesSpell = gameObject.AddComponent<FanOfKnivesSpell>();
                fanOfKnivesSpell.cooldown = fanOfKnives.cooldowns[entityStats.XP.Level - 1];
                fanOfKnivesSpell.daggerPrebab = fanOfKnives.daggerPrefab;
                fanOfKnivesSpell.direction = GetMouseDirection();
                fanOfKnivesSpell.damage = fanOfKnives.damage;
                fanOfKnivesSpell.Cast();
            }
        }
    }
}
