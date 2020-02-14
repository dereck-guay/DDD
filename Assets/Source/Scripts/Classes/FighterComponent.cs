using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Miscellaneous;
using System.Text;
using Interfaces;

[RequireComponent(typeof(AtkDamageComponent))]
[RequireComponent(typeof(AtkSpeedComponent))]
[RequireComponent(typeof(HPComponent))]
[RequireComponent(typeof(ManaComponent))]
[RequireComponent(typeof(SpeedComponent))]
[RequireComponent(typeof(XPComponent))]
public class FighterComponent : MonoBehaviour, ICaster, IPlayableClass
{
    #region Stats
    public AtkSpeedComponent AtkSpeed;
    public AtkDamageComponent AtkDamage;
    public HPComponent HP;
    public ManaComponent Mana;
    public SpeedComponent Speed;
    public XPComponent XP;
    #endregion
    public Spell[] Spells { get; private set; }
    public DictionaryCreator<Spell> SpellDictionary { get; private set; }
    public string Name;
    public IModifiableStat[] ClassStats { get; private set; }

    ManaComponent ICaster.Mana => Mana;

    private void Awake()
    {
        AtkDamage = GetComponent<AtkDamageComponent>();
        AtkSpeed = GetComponent<AtkSpeedComponent>();
        HP = GetComponent<HPComponent>();
        Mana = GetComponent<ManaComponent>();
        Speed = GetComponent<SpeedComponent>();
        XP = GetComponent<XPComponent>();
        CreateSpells();
    }
    private void Start()
    {
        Name = "Mike the Fighter";
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
            new Spell(4, "Slow", "Decrease a target's speed", new Buff(-4, 4), new SingleTarget(2), 4, this),
            new Spell(4, "Fireball", "Shoot a large fireball", new Damage(5), new SkillShot(10, 4), 4, this),
        };
    }
    void ApplyAllStats()
    {
        AtkSpeed.ApplyStats(30);
        AtkDamage.ApplyStats(20);
        HP.ApplyStats(25, 1);
        Mana.ApplyStats(50, 2.5f);
        Speed.ApplyStats(30);
        XP.Current = 0;
    }
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
