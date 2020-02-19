using System.Text;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Miscellaneous;
using Interfaces;
public class Mana : IModifiableStat
{
    float currentMana;
    float maxMana;

    public float ManaRegen { get; set; }
    public float Current
    {
        get { return currentMana; }
        set
        {
            if (value < 0)
                value = 0;
            currentMana = value;
        }
    }
    public float Base
    {
        get { return maxMana; }
        set
        {
            if (value < 1)
                value = 1;
            maxMana = value;
        }
    }
    public Mana(float manaBase, float manaRegen)
    {
        Base = manaBase;
        Current = manaBase;
        ManaRegen = manaRegen;
    }
    public string Name = "Mana";
    public void UseMana(float manaUsed) => Current -= manaUsed;
    public void ApplyModifier(float modifier) => ManaRegen *= modifier;
    public void EndModifier(float modifier) => ManaRegen /= modifier;
    public void Regen(float time) => Current += ManaRegen * time;
    public override string ToString()
    {
        StringBuilder sb = new StringBuilder();
        sb.AppendLine(Name);
        sb.AppendLine($"\t Base {Name} : {Base}");
        sb.AppendLine($"\t Current {Name} : {Current}");
        return sb.ToString();
    }
}
