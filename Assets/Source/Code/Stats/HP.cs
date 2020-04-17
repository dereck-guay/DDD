using System.Collections;
using System.Collections.Generic;
using System.Text;
using System;
using UnityEngine;
using Miscellaneous;
using Interfaces;

public class HP : IModifiableStat
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
                OnTakeDamage?.Invoke(Current - value);
            else if (value > Current)
            {
                if (value > @base)
                    value = @base;
                OnHeal?.Invoke(value - Current);
                
            }
            
            current = value;
            //Debug.Log($"New hp is {Current}");
        }
    }
    public bool IsInvulnerable { get; set; }
    public HP(float hPBase, float hPRegen)
    {
        Base = hPBase;
        Current = hPBase;
        HPRegen = hPRegen;
        IsInvulnerable = false;
        OnTakeDamage += (float damage) => Debug.Log($"target has taken {damage} damage");
        OnDeath += () => Debug.Log("target has died");
        //OnHeal += (float heal) => Debug.Log($"target has healed {heal}");
    }
    public string Name = "HP";
    public Action OnDeath { get; set; }
    public Action<float> OnTakeDamage { get; set; }
    public Action<float> OnHeal { get; set; }
    public void TakeDamage(float damage)
    {
        if(!IsInvulnerable)
            Current -= damage;
    }
    public void Heal(float hPToHeal) => Current += hPToHeal;
    public void Regen(float time)
    {
        if(Current != @base)
            Current += time * HPRegen;
    }
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
