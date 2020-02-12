using System;
using System.Collections.Generic;
using System.Text;
using Unity;
using UnityEngine;
public class Spell
{
    #region Props
    float cooldown;
    float currentCooldown;
    int level;
    int maxLevel;
    float duration;

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
            if (value < 0)
                value = 0;
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
    #region Non-mechanical props
    public string Name { get; private set; }
    public string Description { get; private set; }
    #endregion
    
    public IEffect Effect { get; private set; }
    public int ManaCost { get; private set; }
    public ITypeOfSpell TypeOfSpell { get; private set; }
    public Action OnLevelUp { get; set; }
    #endregion
    public Spell(float cooldownI, string nameI, string descriptionI, IEffect effectI, ITypeOfSpell typeI, int maxLevelI)
    {
        Cooldown = cooldownI;
        Name = nameI;
        Description = descriptionI;
        CurrentCooldown = 0;
        Effect = effectI;
        Level = 1;
        MaxLevel = maxLevelI;
        TypeOfSpell = typeI;
    }
    public void UpdateCooldown(float elapsedTime)
    {
        if (CurrentCooldown - elapsedTime < 0)
            CurrentCooldown = 0;
        else
            CurrentCooldown -= elapsedTime;
    }
    public void Cast(Character cha)
    {
        CurrentCooldown = Cooldown;
        cha.CharacterClass.Mana.UseMana(ManaCost);
    }
    public void Cast(SpellTester spe)
    {
        CurrentCooldown = Cooldown;
        spe.playableClass.Mana.UseMana(ManaCost);
    }
    public void LevelUp()
    {
        Level += 1;
    }
    public override string ToString()
    {
        StringBuilder sb = new StringBuilder();
        sb.AppendLine($"{Name}, {Description}");
        sb.AppendLine($"\t Type : {TypeOfSpell.ToString()}");
        return sb.ToString();
    }

}
