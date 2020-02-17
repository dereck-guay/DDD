using System.Collections;
using System.Collections.Generic;
using System.Text;
using System;
using UnityEngine;
using Miscellaneous;
using Interfaces;

public class HPComponent : MonoBehaviour, IModifiableStat, IRegenerativeStat
{
    float @base;
    float current;

    public float HPRegen { get; set; }
    public float Base
    {
        get { return @base; }
        set
        {
            if (value < 1)
                value = 1;
            @base = value;
        }
    }
    public float Current
    {
        get { return current; }
        set
        {
            if (value <= 0)
            {
                value = 0;
                OnDeath?.Invoke();
            }
            else if (value < Current)
                OnTakeDamage?.Invoke();
            else if (value > @base)
                value = @base;
            current = value;
        }
    }
    public string Name = "HP";
    public Action OnDeath { get; set; }
    public Action OnTakeDamage { get; set; }
    private void Update()
    {
        Regen(Time.deltaTime);
    }
    public void ApplyStats(float iBase, float iRegen)
    {
        Base = iBase;
        Current = iBase;
        HPRegen = iRegen;
    }
    public void TakeDamage(float damage) => Current -= damage;
    public void Heal(float hPToHeal) => Current += hPToHeal;
    public void Regen(float time) => Current += time * HPRegen;
    public void ApplyModifier(float modifier) => HPRegen *= modifier;
    public void EndModifier(float modifier) => HPRegen /= modifier;
    public override string ToString()
    {
        StringBuilder sb = new StringBuilder();
        sb.AppendLine(Name);
        sb.AppendLine($"\t MaxHP : {Base}");
        sb.AppendLine($"\t CurrentHP : {Current}");
        sb.AppendLine($"\t HPRegen : {HPRegen}");
        return sb.ToString();
    }
}
