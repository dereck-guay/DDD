using System;
using System.Collections.Generic;
using System.Text;
using Unity;
using UnityEngine;
using Miscellaneous;
using Interfaces;
public class Spell
{
    #region Props
    float cooldown;
    float currentCooldown;
    int level;
    int maxLevel;
    float duration;

    public bool IsOnCooldown { get; private set; }

    public float Cooldown
    {
        get { return cooldown; }
        private set
        {
            if (value < 0)
                throw new Exception("Cooldown must be higher than 0");
            cooldown = value;
        }
    }
    public float CurrentCooldown
    {
        get { return currentCooldown; }
        private set
        {
            if (value <= 0)
            {
                value = 0;
                IsOnCooldown = false;
            }
            else if(!IsOnCooldown)
                IsOnCooldown = true;
            currentCooldown = value;
        }
    }
    public int Level
    {
        get { return level; }
        private set
        {
            if (value < 1)
                value = 1;
            level = value;
        }
    }
    public int MaxLevel
    {
        get { return maxLevel; }
        private set
        {
            if (value < 1)
                value = 1;
            maxLevel = value;
        }
    }
    public ICaster Caster { get; private set; }
    #region Non-mechanical props
    public string Name { get; private set; }
    public string Description { get; private set; }
    #endregion
    
    public IEffect Effect { get; private set; }
    public int ManaCost { get; private set; }
    public ITypeOfSpell TypeOfSpell { get; private set; }
    public Action Actions { get; set; }
    public Action OnLevelUp { get; set; }
    #endregion
    public Spell(float cooldownI, string nameI, string descriptionI, IEffect effectI, ITypeOfSpell typeI, int maxLevelI, ICaster caster)
    {
        Cooldown = cooldownI;
        Name = nameI;
        Description = descriptionI;
        CurrentCooldown = 0;
        Effect = effectI;
        Level = 1;
        MaxLevel = maxLevelI;
        TypeOfSpell = typeI;
        IsOnCooldown = false;
        Caster = caster;
    }
    public void UpdateCooldown(float elapsedTime)
    {
        if (CurrentCooldown - elapsedTime < 0)
            CurrentCooldown = 0;
        else
            CurrentCooldown -= elapsedTime;
    }
    public void Cast()
    {
        if (!IsOnCooldown)
        {
            CurrentCooldown = Cooldown;
            Caster.Mana.UseMana(ManaCost);
            Actions?.Invoke();
        }
    }
    public void LevelUp() => Level += 1;
    public override string ToString()
    {
        StringBuilder sb = new StringBuilder();
        sb.AppendLine($"{Name}, {Description}");
        sb.AppendLine($"\t Type : {TypeOfSpell.ToString()}");
        return sb.ToString();
    }
    public Spell Clone(ICaster caster) => new Spell(Cooldown, Name, Description, Effect, TypeOfSpell, MaxLevel, caster);
}
