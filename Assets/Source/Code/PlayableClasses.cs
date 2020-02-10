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
    public Wizard()
    {
        AtkSpeed = new AtkSpeed(1);
        AtkDamage = new AtkDamage(2);
        HP = new HP(50, 2.5f);
        Mana = new Mana(50, 2.5f);
        Speed = new Speed(30);
        XP = new XP();
    }
}
