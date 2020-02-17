<<<<<<< HEAD
﻿using System;
using System.Collections;
=======
﻿using System.Collections;
>>>>>>> 389faaeb42064fd86499599ccd96e3f35746ea25
using System.Collections.Generic;
using UnityEngine;
using Miscellaneous;
using System.Text;
using Interfaces;

<<<<<<< HEAD
public class WizardComponent : PlayerMonoBehaviour
{
    public KeyCode[] inputs;
    private KeyBindings keyBindings;
    private IModifiableStat[] characterStats;

    // Spell Attr
    public float[] slowValues;

    // Character Stats
=======
[RequireComponent(typeof(AtkDamageComponent))]
[RequireComponent(typeof(AtkSpeedComponent))]
[RequireComponent(typeof(HPComponent))]
[RequireComponent(typeof(ManaComponent))]
[RequireComponent(typeof(SpeedComponent))]
[RequireComponent(typeof(XPComponent))]
public class WizardComponent : MonoBehaviour, IPlayableClass, ICaster
{
    #region Stats
>>>>>>> 389faaeb42064fd86499599ccd96e3f35746ea25
    public AtkSpeedComponent AtkSpeed;
    public AtkDamageComponent AtkDamage;
    public HPComponent HP;
    public ManaComponent Mana;
    public SpeedComponent Speed;
    public XPComponent XP;
<<<<<<< HEAD
=======
    #endregion
    public Spell[] Spells { get; private set; }
    public DictionaryCreator<Spell> SpellDictionary { get; private set; }
    public string Name;
    public IModifiableStat[] ClassStats { get; private set; }

    ManaComponent ICaster.Mana => Mana;
>>>>>>> 389faaeb42064fd86499599ccd96e3f35746ea25

    private void Awake()
    {
        AtkDamage = GetComponent<AtkDamageComponent>();
        AtkSpeed = GetComponent<AtkSpeedComponent>();
        HP = GetComponent<HPComponent>();
        Mana = GetComponent<ManaComponent>();
        Speed = GetComponent<SpeedComponent>();
        XP = GetComponent<XPComponent>();
<<<<<<< HEAD

        keyBindings = new KeyBindings(
            new Action[]
            {
                () => Move(Vector3.forward),
                () => Move(Vector3.left),
                () => Move(Vector3.down),
                () => Move(Vector3.right),
                () =>
                {
                    if (! IsOnCooldown(typeof(SlowSpell)))
                    {
                        var slowSpell = gameObject.AddComponent<SlowSpell>();
                        slowSpell.target = GetEntityAtMousePosition();
                        slowSpell.slowValue = slowValues[XP.Level];
                    }
                }
            }, inputs
        );
    }

    private void Start() => ApplyAllStats();

    private void ApplyAllStats()
=======
        CreateSpells();
    }
    private void Start()
    {
        Name = "Tim the Wizard";
        ApplyAllStats();
        SpellDictionary = new DictionaryCreator<Spell>(Spells);
        ClassStats = new IModifiableStat[]
        {
            AtkDamage,
            AtkSpeed,
            HP,
            Mana,
            Speed,
        };
    }
    void CreateSpells()
    {
        Spells = new Spell[4]
        {
            new Spell(4, "Fireball", $"Shoot a large fireball that deals {5} damage on-hit", new Damage(5), new SkillShot(10, 4), 4, this),
            new Spell(4, "Heal", $"Heal a target within range for {5} health", new Heal(5), new SingleTarget(2), 4, this),
            new Spell(4, "Slow", "Decrease a target's speed", new Buff(-4, 4), new SkillShot(10, 4), 4, this),
            new Spell(4, "Fireball", "Shoot a large fireball", new Damage(5), new SkillShot(10, 4), 4, this),
        };
    }
    void ApplyAllStats()
>>>>>>> 389faaeb42064fd86499599ccd96e3f35746ea25
    {
        AtkSpeed.ApplyStats(15);
        AtkDamage.ApplyStats(10);
        HP.ApplyStats(20, 1);
        Mana.ApplyStats(100, 5);
        Speed.ApplyStats(27);
        XP.Current = 0;
    }
<<<<<<< HEAD

    private void Move(Vector3 direction) => transform.Translate(direction * Speed.Current * Time.deltaTime);


    private void Update() => keyBindings.CallBindings();
}

//[RequireComponent(typeof(AtkDamageComponent))]
//[RequireComponent(typeof(AtkSpeedComponent))]
//[RequireComponent(typeof(HPComponent))]
//[RequireComponent(typeof(ManaComponent))]
//[RequireComponent(typeof(SpeedComponent))]
//[RequireComponent(typeof(XPComponent))]
//public class WizardComponent : MonoBehaviour, IPlayableClass, ICaster
//{
//    #region Stats
//    public AtkSpeedComponent AtkSpeed;
//    public AtkDamageComponent AtkDamage;
//    public HPComponent HP;
//    public ManaComponent Mana;
//    public SpeedComponent Speed;
//    public XPComponent XP;
//    #endregion
//    public Spell[] Spells { get; private set; }
//    public DictionaryCreator<Spell> SpellDictionary { get; private set; }
//    public string Name;
//    public IModifiableStat[] ClassStats { get; private set; }

//    ManaComponent ICaster.Mana => Mana;

//    private void Awake()
//    {
//        AtkDamage = GetComponent<AtkDamageComponent>();
//        AtkSpeed = GetComponent<AtkSpeedComponent>();
//        HP = GetComponent<HPComponent>();
//        Mana = GetComponent<ManaComponent>();
//        Speed = GetComponent<SpeedComponent>();
//        XP = GetComponent<XPComponent>();
//        CreateSpells();
//    }
//    private void Start()
//    {
//        Name = "Tim the Wizard";
//        ApplyAllStats();
//        SpellDictionary = new DictionaryCreator<Spell>(Spells);
//        ClassStats = new IModifiableStat[]
//        {
//            AtkDamage,
//            AtkSpeed,
//            HP,
//            Mana,
//            Speed,
//        };
//    }
//    void CreateSpells()
//    {
//        Spells = new Spell[4]
//        {
//            new Spell(4, "Fireball", $"Shoot a large fireball that deals {5} damage on-hit", new Damage(5), new SkillShot(10, 4), 4, this),
//            new Spell(4, "Heal", $"Heal a target within range for {5} health", new Heal(5), new SingleTarget(2), 4, this),
//            new Spell(4, "Slow", "Decrease a target's speed", new Buff(-4, 4), new SkillShot(10, 4), 4, this),
//            new Spell(4, "Fireball", "Shoot a large fireball", new Damage(5), new SkillShot(10, 4), 4, this),
//        };
//    }
//    void ApplyAllStats()
//    {
//        AtkSpeed.ApplyStats(15);
//        AtkDamage.ApplyStats(10);
//        HP.ApplyStats(20, 1);
//        Mana.ApplyStats(100, 5);
//        Speed.ApplyStats(27);
//        XP.Current = 0;
//    }
//    public override string ToString()
//    {
//        StringBuilder sb = new StringBuilder();

//        sb.AppendLine($"Class : {Name}");
//        sb.AppendLine("Base Stats :");
//        sb.AppendLine(AtkDamage.ToString());
//        sb.AppendLine(AtkSpeed.ToString());
//        sb.AppendLine(HP.ToString());
//        sb.AppendLine(Mana.ToString());
//        sb.AppendLine(Speed.ToString());
//        sb.AppendLine(XP.ToString());
//        sb.AppendLine("Spells :");
//        foreach (Spell s in Spells)
//            sb.AppendLine($"\t {s.ToString()}");

//        return sb.ToString();
//    }
//}


=======
    public override string ToString()
    {
        StringBuilder sb = new StringBuilder();

        sb.AppendLine($"Class : {Name}");
        sb.AppendLine("Base Stats :");
        sb.AppendLine(AtkDamage.ToString());
        sb.AppendLine(AtkSpeed.ToString());
        sb.AppendLine(HP.ToString());
        sb.AppendLine(Mana.ToString());
        sb.AppendLine(Speed.ToString());
        sb.AppendLine(XP.ToString());
        sb.AppendLine("Spells :");
        foreach (Spell s in Spells)
            sb.AppendLine($"\t {s.ToString()}");

        return sb.ToString();
    }
}
>>>>>>> 389faaeb42064fd86499599ccd96e3f35746ea25
