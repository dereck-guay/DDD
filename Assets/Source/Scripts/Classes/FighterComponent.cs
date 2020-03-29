using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Miscellaneous;
using System.Text;
using Interfaces;
using System.Linq;
[RequireComponent(typeof(Stats))]
public class FighterComponent : PlayerMonoBehaviour
{
    public Camera camera;

    #region Stuff for inspector
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
    public class AutoAttack
    {
        public GameObject autoAttackPrefab;
    };
    [System.Serializable]
    public class Shield
    {
        public GameObject shieldObject;
    };
    [HideInInspector]
    public bool spellLocked = false;

    
    [System.Serializable]
    public class Slam {
        public float range;
    };

    [Header("Spell Settings")]
    public AutoAttack autoAttack;
    public Slam slam;
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

    private KeyBindings keyBindings;
    private Rigidbody rigidBody;
    private void Awake()
    {
        keyBindings = new KeyBindings(
            new Action[]
            {
                () => {
                    if (canAttack)
                    {
                        if (!IsOnCooldown(typeof(MeleeAutoAttackSpell)))
                        {
                            var autoAttackSpell = gameObject.AddComponent<MeleeAutoAttackSpell>();
                            autoAttackSpell.autoAttackPrefab = autoAttack.autoAttackPrefab;
                            autoAttackSpell.damage = entityStats.AtkDamage.Current;
                            autoAttackSpell.Cast(entityStats.AtkSpeed.Current, transform.position, GetMouseDirection());
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
                    if (! IsOnCooldown(typeof(SlamSpell)))
                    {
                        var slamSpell = gameObject.AddComponent<SlamSpell>();
                        slamSpell.range = slam.range;
                        slamSpell.position = GetMousePositionOn2DPlane();
                        slamSpell.Cast(entityStats.XP.Level);
                    }
                },
                () =>{
                    if(!IsOnCooldown(typeof(RageSpell)) && !IsOnCooldown(typeof(TakeABreatherSpell)))
                    {
                        var rageSpell = gameObject.AddComponent<RageSpell>();
                        rageSpell.Cast(entityStats.XP.Level);
                    }
                },
                () =>{
                    if (!IsOnCooldown(typeof(RageSpell)) && !IsOnCooldown(typeof(TakeABreatherSpell)))
                    {
                        var takeABreatherSpell = gameObject.AddComponent<TakeABreatherSpell>();
                        takeABreatherSpell.Cast(entityStats.XP.Level);
                    }
                },
                () =>{
                    if (!IsOnCooldown(typeof(ShieldSpell)))
                    {
                        var shieldSpell = gameObject.AddComponent<ShieldSpell>();
                        shieldSpell.shield = shield.shieldObject;
                        shieldSpell.bodyToChange = characterParts.body;
                        shieldSpell.Cast(entityStats.XP.Level);
                    }
                }
            }, inputs
        );
    }
    private void Start()
    {
        isStunned = false;
        shield.shieldObject.SetActive(false);
        rigidBody = GetComponentInChildren<Rigidbody>();
        entityStats = GetComponent<Stats>();
        entityStats.ApplyStats(statsInit.attackDamage, statsInit.attackSpeed, statsInit.maxHp, statsInit.hpRegen, statsInit.maxMana, statsInit.manaRegen, statsInit.range, statsInit.speed);
        SetTimeSinceLastAttack(0);
    }

    private void Update()
    {
        SetTimeSinceLastAttack(GetTimeSinceLastAttack() + Time.deltaTime);
        if (!(isStunned && spellLocked))
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
        if(!isStunned)
            rigidBody.AddForce(direction * entityStats.Speed.Current * Time.deltaTime * 100f);
    }
    void DirectCharacter() //make the character face the direction of the mouse
    {
        var directionToLookAt = transform.position + GetMouseDirection();
        directionToLookAt.y = transform.position.y;
        transform.LookAt(directionToLookAt);
    }
}
