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
            if (value <= 0)
                value = 0;
            else if (value < Current)
                OnUse?.Invoke(Current - value);
            else if (value > Current)
            {
                if (value > Base)
                    value = Base;
                OnRegen?.Invoke(value - Current);
            }

            currentMana = value;
        }
    }
    public float Base
    {
        get { return maxMana; }
        set
        {
            if (value < 0)
                throw new ArgumentException("MaxMana cannot be smaller than zero");
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
    public Action<float> OnUse { get; set; }
    public Action<float> OnRegen { get; set; }
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
