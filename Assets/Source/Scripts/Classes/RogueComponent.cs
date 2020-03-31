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

    [System.Serializable]
    public class AutoAttack
    {
        public GameObject autoAttackPrefab;
    };
    [HideInInspector]
    public bool spellLocked = false;


    [System.Serializable]
    public class StunningBlade
    {
        public GameObject stunningBladePrefab;
    };

    [Header("Spell Settings")]
    public AutoAttack autoAttack;
    public StunningBlade stunningBlade;
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
                    if (! IsOnCooldown(typeof(StunningBladeSpell)))
                    {
                        var stunningBladeSpell = gameObject.AddComponent<StunningBladeSpell>();
                        stunningBladeSpell.direction = GetMouseDirection();
                        stunningBladeSpell.stunningBladePrefab = stunningBlade.stunningBladePrefab;
                        stunningBladeSpell.Cast(entityStats.XP.Level);
                        // Pour pas que le player puisse bouger pendant l'ainimation du spell
                        // spellLocked = true;
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
                    //if (!IsOnCooldown(typeof(ShieldSpell)))
                    //{
                    //    var shieldSpell = gameObject.AddComponent<ShieldSpell>();
                    //    shieldSpell.shield = shield.shieldObject;
                    //    shieldSpell.bodyToChange = characterParts.body;
                    //    shieldSpell.Cast(entityStats.XP.Level);
                    //}
                }
            }, inputs
        );
    }
    private void Start()
    {
        entityStats = GetComponent<Stats>();
        entityStats.ApplyStats(statsInit);
        isStunned = false;

        rigidBody = GetComponentInChildren<Rigidbody>();
        SetTimeSinceLastAttack(0);
    }

    private void Update()
    {
        SetTimeSinceLastAttack(GetTimeSinceLastAttack() + Time.deltaTime);
        if (!isStunned)
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
        if (!isStunned)
            rigidBody.AddForce(direction * entityStats.Speed.Current * Time.deltaTime * 100f);
    }
    void DirectCharacter() //make the character face the direction of the mouse
    {
        var directionToLookAt = transform.position + GetMouseDirection();
        directionToLookAt.y = transform.position.y;
        transform.LookAt(directionToLookAt);
    }
}
