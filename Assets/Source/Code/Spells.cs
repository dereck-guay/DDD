using System;
using System.Collections.Generic;
using System.Text;
public class Spell
{
    #region Props
    float cooldown;
    float currentCooldown;
    int level;

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
    public string Name { get; private set; }
    public string Description { get; private set; }
    public Effect Effect { get; private set; }
    public int ManaCost { get; private set; }
    public TypeOfSpell TypeOfSpell { get; private set; }
    #endregion
    public Spell(float cooldownI, string nameI, string descriptionI, Effect effectI, TypeOfSpell typeI)
    {
        Cooldown = cooldownI;
        Name = nameI;
        Description = descriptionI;
        CurrentCooldown = 0;
        Effect = effectI;
        Level = 1;
        TypeOfSpell = typeI;
    }
    public void UpdateCooldown(float elapsedTime)
    {
        if (CurrentCooldown - elapsedTime < 0)
            CurrentCooldown = 0;
        else
            CurrentCooldown -= elapsedTime;
    }
    public void Cast(Character caster)
    {
        CurrentCooldown = Cooldown;
        caster.Mana.UseMana(ManaCost);
    }
    public void LevelUp()
    {
        Level += 1;
    }

}
