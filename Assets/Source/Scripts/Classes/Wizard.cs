using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Miscellaneous;
using System.Text;
using Interfaces;


public class Wizard : MonoBehaviour, IPlayableClass
{
    #region Stats
    
    #endregion
    public Spell[] Spells { get; private set; }
    public DictionaryCreator<Spell> SpellDictionary { get; private set; }
    public string Name;
    public AtkSpeed AtkSpeed;
    public AtkDamage AtkDamage;
    public HP HP;
    public Mana Mana;
    public Speed Speed;
    public XP XP;
    public IModifiableStat[] ClassStats { get; private set; }
    private void Awake()
    {
        AtkDamage = GetComponent<AtkDamage>();
        AtkSpeed = GetComponent<AtkSpeed>();
        HP = GetComponent<HP>();
        Mana = GetComponent<Mana>();
        Speed = GetComponent<Speed>();
        XP = GetComponent<XP>();
    }
    private void Start()
    {
        Name = "Tim the Wizard";
        AtkSpeed.ApplyStats(30);
        AtkDamage.ApplyStats(20);
        HP.ApplyStats(25, 1);
        Mana.ApplyStats(50, 2.5f);
        Speed.ApplyStats(30);
        XP.Current = 0;
        Spells = CreateSpells();
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
    Spell[] CreateSpells()
    {
        Spell[] spells = new Spell[4]
        {
            new Spell(4, "Fireball", $"Shoot a large fireball that deals {5} damage on-hit", new Damage(5), new SkillShot(10, 4), 4),
            new Spell(4, "Heal", $"Heal a target within range for {5} health", new Heal(5), new SingleTarget(2), 4),
            new Spell(4, "Slow", "Decrease a target's speed", new Buff(-4, 4), new SkillShot(10, 4), 4),
            new Spell(4, "Fireball", "Shoot a dart of red crackling energy towards a point before exploding upon impact with the ground into a large fireball", new Damage(5), new SkillShot(10, 4), 4),
        };
        return spells;
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
