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
    EntityMonoBehaviour attacker;
    float xpValue;

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
                //OnDeath?.Invoke();
            }
            else if (value < Current)
            {
                OnTakeDamage?.Invoke(Current - value);
            }
            else if (value > Current)
            {
                if (value > @base)
                    value = @base;
                OnHeal?.Invoke(value - Current);
                
            }
            
            current = value;

            if (current == 0)
            {
                OnDeath?.Invoke();
                if (attacker)
                    attacker.entityStats.XP.AddXP(xpValue);
                Debug.Log(attacker.gameObject);
            }
            //Debug.Log($"New hp is {Current}");
        }
    }
    public bool IsInvulnerable { get; set; }
    public HP(float hPBase, float hPRegen, float initXpValue)
    {
        Base = hPBase;
        Current = hPBase;
        HPRegen = hPRegen;
        xpValue = initXpValue;
        OnDeath += () => Debug.Log("target has died");
        IsInvulnerable = false;
    }
    public string Name = "HP";
    public Action OnDeath { get; set; }
    public Action<float> OnTakeDamage { get; set; }
    public Action<float> OnHeal { get; set; }
    public void TakeDamage(float damage, EntityMonoBehaviour initAttacker)
    {
        attacker = initAttacker;
        if(!IsInvulnerable)
            Current -= damage;
        attacker = null;

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
