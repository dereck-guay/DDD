using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Stats
{
    public AtkDamage AtkDamage { get; private set; }
    public AtkSpeed AtkSpeed { get; private set; }
    public HP HP { get; private set; }
    public Mana Mana { get; private set; }
    public Range Range { get; private set; }
    public Speed Speed { get; private set; }
    public XP XP { get; private set; }
    public Stats(float damageBase, float atkSpeedBase, float hPBase, float hPRegen, float manaBase, float manaRegen, float rangeBase, float speedBase)
    {
        AtkDamage = new AtkDamage(damageBase);
        AtkSpeed = new AtkSpeed(atkSpeedBase);
        HP = new HP(hPBase, hPRegen);
        Mana = new Mana(manaBase, manaRegen);
        Range = new Range(rangeBase);
        Speed = new Speed(speedBase);
        XP = new XP();
    }
    public void Regen(float elaspedTime)
    {
        HP.Regen(elaspedTime);
        Mana.Regen(elaspedTime);
    }
}
