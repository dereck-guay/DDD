using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPlayableClass
{
    AtkSpeed AtkSpeed { get; }
    AtkDamage AtkDamage { get; }
    HP HP { get; }
    Mana Mana { get; }
    Speed Speed { get; }
    XP XP { get; }
    Spell[] Spells { get; }
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
    public Fighter()
    {
        AtkSpeed = new AtkSpeed(1);
        AtkDamage = new AtkDamage(2);
        HP = new HP(50, 2.5f);
        Mana = new Mana(50, 2.5f);
        Speed = new Speed(30);
        XP = new XP();
        //Spells = CreateSpells();
    }
    /*Spell[] CreateSpells()
    {
        Spell[] spells = new Spell[4]
        {
            new Spell(1, "")
        }
    }*/
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
    public Wizard()
    {
        AtkSpeed = new AtkSpeed(1);
        AtkDamage = new AtkDamage(2);
        HP = new HP(50, 2.5f);
        Mana = new Mana(50, 2.5f);
        Speed = new Speed(30);
        XP = new XP();
        Spells = CreateSpells();
    }
    Spell[] CreateSpells()
    {
        Spell[] spells = new Spell[4]
        {
            new Spell(4, "Fireball", $"Shoot a large fireball that deals {5} damage on-hit", new Damage(5), new SkillShot(10, 4), 4),
            new Spell(4, "Heal", $"Heal a target within range for {5} health", new Heal(5), new SingleTarget(), 4),
            new Spell(4, "Slow", "Decrease a target's speed", new Buff(-2, ), new SkillShot(10, 4), 4),
            new Spell(4, "Fireball", "Shoot a dart of red crackling energy towards a point before exploding upon impact with the ground into a large fireball", new Damage(5), new SkillShot(10, 4), 4),
        };
        return spells;
    }
}
