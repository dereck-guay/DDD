using System.Text;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Miscellaneous;
using Interfaces;
public class ManaComponent : MonoBehaviour, IModifiableStat, IRegenerativeStat
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
    public string Name = "Mana";
    private void Update() => Regen(Time.deltaTime);
    public void UseMana(float manaUsed) => Current -= manaUsed;
    public void ApplyModifier(float modifier) => ManaRegen *= modifier;
    public void EndModifier(float modifier) => ManaRegen /= modifier;
    public void Regen(float time) => Current += ManaRegen * time;
    public void ApplyStats(float iBase, float iRegen)
    { Base = iBase; ManaRegen = iRegen; }
    public override string ToString()
    {
        StringBuilder sb = new StringBuilder();
        sb.AppendLine(Name);
        sb.AppendLine($"\t Base {Name} : {Base}");
        sb.AppendLine($"\t Current {Name} : {Current}");
        return sb.ToString();
    }
}
