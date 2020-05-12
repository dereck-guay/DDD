using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Interfaces;
using Miscellaneous;

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
    public float xpYield;
};

public class Stats : MonoBehaviour
{
    public AtkDamage AtkDamage { get; private set; }
    public AtkSpeed AtkSpeed { get; private set; }
    public HP HP { get; private set; }
    public Mana Mana { get; private set; }
    public Range Range { get; private set; }
    public Speed Speed { get; private set; }
    public XP XP { get; private set; }
    public DictionaryCreator<IModifiableStat> ModifiableStats;
    public void ApplyStats(StatsInit statsInit)
    {
        AtkDamage = new AtkDamage(statsInit.attackDamage);
        AtkSpeed = new AtkSpeed(statsInit.attackSpeed);
        HP = new HP(statsInit.maxHp, statsInit.hpRegen);
        Mana = new Mana(statsInit.maxMana, statsInit.manaRegen);
        Range = new Range(statsInit.range);
        Speed = new Speed(statsInit.speed);
        XP = new XP(statsInit.xpYield);
        var stats = new IModifiableStat[]
        {
            AtkDamage,
            AtkSpeed,
            HP,
            Mana,
            Speed
        };
        ModifiableStats = new DictionaryCreator<IModifiableStat>(stats);
    }
    public void Regen()
    {
        HP.Regen(Time.deltaTime);
        Mana.Regen(Time.deltaTime);
    }
}
