using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Miscellaneous;
using System.Text;
using Interfaces;
[RequireComponent(typeof(Stats))]
[RequireComponent(typeof(Rigidbody))]
public class WizardComponent : PlayerMonoBehaviour
{
    public Camera camera;
       
    [Header("Stats")]
    public StatsInit statsInit;

    public StatusBarsComponent[] statusBars;

    [Header("Inputs")]
    public KeyCode[] inputs;
    [System.Serializable]
    public class AutoAttack
    {
        public GameObject autoAttackPrefab;
    };
    [System.Serializable]
    public class Fireball
    {
        public GameObject fireballPrefab;
        public float manaCost;
    };
    [System.Serializable]
    public class Slow
    {
        public float slowValue;
        public float manaCost;
    };
    [System.Serializable]
    public class RayOfFrost
    {
        public GameObject rayOfFrostPrefab;
        public GameObject icePatchPrefab;
        public float manaCost;
        public float slowValue;
    };
    [System.Serializable]
    public class Heal
    {
        public float[] healValues;
        public float manaCost;
    };

    [Header("Spell Settings")]
    public AutoAttack autoAttack;
    public Fireball fireball;
    public Slow slow;
    public RayOfFrost rayOfFrost;
    public Heal heal;

    private KeyBindings keyBindings;
    private bool canAttack;
    private float timeSinceLastAttack;
    private Rigidbody rigidbody;
    
    private float GetTimeSinceLastAttack()
    { return timeSinceLastAttack; }
    private void SetTimeSinceLastAttack(float value)
    {
        if (value > 1 / entityStats.AtkSpeed.Current)
            canAttack = true;
        timeSinceLastAttack = value;
    }
    private void Awake()
    {
        canAttack = true;
        keyBindings = new KeyBindings(
            new Action[]
            {
                () => {
                    if (canAttack)
                    {
                        var target = GetEntityAtMousePosition();
                        if (target && TargetIsWithinRange(target, entityStats.Range.Current))
                        {
                            var autoAttackSpell = gameObject.AddComponent<AutoAttackSpell>();
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
                   if (CanCast(fireball.manaCost, typeof(FireballSpell)))
                    {
                        entityStats.Mana.UseMana(fireball.manaCost);
                        var fireballSpell = gameObject.AddComponent<FireballSpell>();
                        fireballSpell.fireballPrefab = fireball.fireballPrefab;
                        fireballSpell.direction = GetMouseDirection();
                        fireballSpell.Cast(entityStats.XP.Level);
                    }
                },
                () => {
                    if (CanCast(slow.manaCost, typeof(SlowSpell)))
                    {
                        var slowSpell = gameObject.AddComponent<SlowSpell>();
                        var target = GetEntityAtMousePosition();
                        if (target) // target != null
                        {
                            // Get parent object because the collider is in the body of the character.
                            slowSpell.target = target.transform.parent.gameObject;
                            slowSpell.slowValue = slow.slowValue;
                            slowSpell.Cast(entityStats.XP.Level);
                        } else { Destroy(target); }
                    }
                },
                () => {
                    if (CanCast(heal.manaCost, typeof(HealSpell)))
                    {
                        var healSpell = gameObject.AddComponent<HealSpell>();
                        var target = GetEntityAtMousePosition();
                        if (target) // target != null
                        {
                            // Get parent object because the collider is in the body of the character.
                            healSpell.target = target.transform.parent.gameObject;
                            healSpell.healValue = heal.healValues;
                            healSpell.Cast(entityStats.XP.Level);
                        } else { Destroy(target); }
                    }
                },
                () => {
                    if (CanCast(rayOfFrost.manaCost, typeof(RayOfFrostSpell)))
                    {
                        entityStats.Mana.UseMana(fireball.manaCost);
                        var rayOfFrostSpell = gameObject.AddComponent<RayOfFrostSpell>();
                        rayOfFrostSpell.rayOfFrostPrefab = rayOfFrost.rayOfFrostPrefab;
                        rayOfFrostSpell.icePatchPrefab = rayOfFrost.icePatchPrefab;
                        rayOfFrostSpell.slowValue = rayOfFrost.slowValue;
                        rayOfFrostSpell.direction = GetMouseDirection();
                        rayOfFrostSpell.Cast(entityStats.XP.Level, this);
                    }
                },
                () => { entityStats.HP.TakeDamage(1); }
            }, inputs
        );
    }

    private void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
        entityStats = GetComponent<Stats>();
        entityStats.ApplyStats(statsInit.attackDamage, statsInit.attackSpeed, statsInit.maxHp, statsInit.hpRegen, statsInit.maxMana, statsInit.manaRegen, statsInit.range, statsInit.speed);
        SetTimeSinceLastAttack(0);

        statusBars[0].SetMax(entityStats.HP.Base);
        statusBars[1].SetMax(entityStats.Mana.Base);
        entityStats.HP.OnTakeDamage += damage => statusBars[0].SetCurrent(entityStats.HP.Current);
        entityStats.HP.OnHeal += regen => statusBars[0].SetCurrent(entityStats.HP.Current);
        entityStats.Mana.OnUse += manaCost => statusBars[1].SetCurrent(entityStats.Mana.Current);
        entityStats.Mana.OnRegen += manaCost => statusBars[1].SetCurrent(entityStats.Mana.Current);
    }
    private void Update()
    {
        DirectCharacter();
        SetTimeSinceLastAttack(GetTimeSinceLastAttack() + Time.deltaTime);
        //rigidbody.velocity = Vector3.zero; //Stop rigidbodies from moving the character
        keyBindings.CallBindings();
        entityStats.Regen();

        camera.transform.position = new Vector3(
            transform.position.x,
            camera.transform.position.y,
            transform.position.z - 5
        ); // Moves the camera according to the player.
   }

    private void Move(Vector3 direction)
    {
      //var displacement = direction * entityStats.Speed.Current * Time.deltaTime;
      //transform.Translate(displacement, Space.World);

      rigidbody.AddForce(direction * entityStats.Speed.Current * Time.deltaTime); //Sqrt?

        
    }

    void DirectCharacter() //make the character face the direction of the mouse
    {
        var directionToLookAt = transform.position + GetMouseDirection();
        directionToLookAt.y = transform.position.y;
        transform.LookAt(directionToLookAt);
    }

    private bool TargetIsWithinRange(GameObject target, float range) =>
       (target.transform.position - transform.position).magnitude < range;

    private bool CanCast(float manaCost, Type spell) =>
        entityStats.Mana.Current > manaCost && !IsOnCooldown(spell);
}