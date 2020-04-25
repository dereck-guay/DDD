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
    public Camera camera;

    #region Stuff for inspector
    [Header("Inputs")]
    public KeyCode[] inputs;

    [Serializable]
    public class AutoAttack
    {
        public GameObject autoAttackPrefab;
    };
    [HideInInspector]
    public bool spellLocked = false;


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
        public float smokeDuration;
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
    private KeyBindings keyBindings;
    private Rigidbody rigidBody;
    private bool isDashing;
    private void Awake()
    {
        keyBindings = new KeyBindings(
            new Action[]
            {
                () => {
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
                            SetTimeSinceLastAttack(0) ;
                            canAttack = false;
                        }
                    }
                },
                () => Move(Vector3.forward),
                () => Move(Vector3.left),
                () => Move(Vector3.back),
                () => Move(Vector3.right),
                () => {
                    if (CanCast(stunningBlade.manaCost, typeof(StunningBladeSpell)))
                    {
                        var stunningBladeSpell = gameObject.AddComponent<StunningBladeSpell>();
                        stunningBladeSpell.direction = GetMouseDirection();
                        stunningBladeSpell.stunningBladePrefab = stunningBlade.stunningBladePrefab;
                        stunningBladeSpell.cooldown = stunningBlade.cooldowns[entityStats.XP.Level - 1];
                        stunningBladeSpell.effectDuration = stunningBlade.effectDurations[entityStats.XP.Level - 1];
                        stunningBladeSpell.Cast();
                    }
                },
                () =>{
                    if(CanCast(dash.manaCost, typeof(DashSpell)))
                    {
                        var dashSpell = gameObject.AddComponent<DashSpell>();
                        dashSpell.cooldown = dash.cooldowns[entityStats.XP.Level];
                        dashSpell.player = this;
                        dashSpell.Cast(GetMouseDirection(), dash.dashMultiplier);
                    }
                },
                () =>{
                    if (CanCast(smokeScreen.manaCost, typeof(SmokeScreenSpell)))
                    {
                        var smokeScreenSpell = gameObject.AddComponent<SmokeScreenSpell>();
                        smokeScreenSpell.cooldown = smokeScreen.cooldowns[entityStats.XP.Level - 1];
                        smokeScreenSpell.player = gameObject;
                        smokeScreenSpell.smoke = smokeScreen.smoke;
                        smokeScreenSpell.smokeDuration = smokeScreen.smokeDuration;
                        smokeScreenSpell.Cast();
                    }
                },
                () =>{
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
            }, inputs
        );
    }
    private void Start()
    {
        entityStats = GetComponent<Stats>();
        entityStats.ApplyStats(statsInit);

        rigidBody = GetComponentInChildren<Rigidbody>();
        SetTimeSinceLastAttack(0);
    }

    private void Update()
    {
        SetTimeSinceLastAttack(GetTimeSinceLastAttack() + Time.deltaTime);
        if (!IsStunned)
        {
            DirectCharacter();
            keyBindings.CallBindings();
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
        if (!IsStunned && !isDashing)
            rigidBody.AddForce(direction * entityStats.Speed.Current * Time.deltaTime * 100f);
    }
    void DirectCharacter() //make the character face the direction of the mouse
    {
        var directionToLookAt = transform.position + GetMouseDirection();
        directionToLookAt.y = transform.position.y;
        transform.LookAt(directionToLookAt);
    }
}
