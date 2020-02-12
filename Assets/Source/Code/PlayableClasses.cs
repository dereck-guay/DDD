using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Miscellaneous;
using System.Text;

public interface IPlayableClass
{
    AtkSpeed AtkSpeed { get; }
    AtkDamage AtkDamage { get; }
    HP HP { get; }
    Mana Mana { get; }
    Speed Speed { get; }
    XP XP { get; }
    Spell[] Spells { get; }
    DictionaryCreator<Spell> SpellDictionary { get; }
}
public class Fighter : IPlayableClass
{
    #region Stats
    public AtkSpeed AtkSpeed { get; private set; }
    public AtkDamage AtkDamage { get; private set; }
    public HP HP { get; private set; }
    public Mana Mana { get; private set; }
    public Speed Speed { get; private set; }
    public XP XP { get; private set; }
    #endregion
    public Spell[] Spells { get; private set; }
    public DictionaryCreator<Spell> SpellDictionary { get; private set; }
    public string Name { get; private set; }
    public Fighter()
    {
        Name = "Mike the Fighter";
        AtkSpeed = new AtkSpeed(1);
        AtkDamage = new AtkDamage(2);
        HP = new HP(50, 2.5f);
        Mana = new Mana(50, 2.5f);
        Speed = new Speed(30);
        XP = new XP();
        Spells = CreateSpells();
        SpellDictionary = new DictionaryCreator<Spell>(Spells);
    }
    Spell[] CreateSpells()
    {
        Spell[] spells = new Spell[4]
        {
            new Spell(4, "Fireball", $"Shoot a large fireball that deals {5} damage on-hit", new Damage(5), new SkillShot(10, 4), 4),
            new Spell(4, "Heal", $"Heal a target within range for {5} health", new Heal(5), new SingleTarget(), 4),
            new Spell(4, "Slow", "Decrease a target's speed", new Damage(5), new SkillShot(10, 4), 4),
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
public class Wizard : IPlayableClass
{
    #region Stats
    public AtkSpeed AtkSpeed { get; private set; }
    public AtkDamage AtkDamage { get; private set; }
    public HP HP { get; private set; }
    public Mana Mana { get; private set; }
    public Speed Speed { get; private set; }
    public XP XP { get; private set; }
    #endregion
    public Spell[] Spells { get; private set; }
    public DictionaryCreator<Spell> SpellDictionary { get; private set; }
    public string Name { get; private set; }
    public Wizard()
    {
        Name = "Tim the Wizard";
        AtkDamage = new AtkDamage(2);
        AtkSpeed = new AtkSpeed(1);
        HP = new HP(50, 2.5f);
        Mana = new Mana(50, 2.5f);
        Speed = new Speed(30);
        XP = new XP();
        Spells = CreateSpells();
        SpellDictionary = new DictionaryCreator<Spell>(Spells);
    }
    Spell[] CreateSpells()
    {
        Spell[] spells = new Spell[4]
        {
            new Spell(4, "Fireball", $"Shoot a large fireball that deals {5} damage on-hit", new Damage(5), new SkillShot(10, 4), 4),
            new Spell(4, "Heal", $"Heal a target within range for {5} health", new Heal(5), new SingleTarget(), 4),
            new Spell(4, "Slow", "Decrease a target's speed", new Damage(5), new SkillShot(10, 4), 4),
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
public enum SpellList { Fireball, Heal, Slow }
