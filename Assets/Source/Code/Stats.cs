using System.Text;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ModifiableStat
{
    void ApplyModifier(float modifier);
    void EndModifier(float modifier);
    float Base { get; }
    float Current { get; }
}
public enum ModifiableStats { AtkDamage, AtkSpeed, HP, Mana, Speed };

public interface RegenerativeStat
{
    void Update(float time);
}
#region Stats
public class HP : ModifiableStat, RegenerativeStat
{
    #region Props
    float @base;
    float current;

    public float HPRegen { get; private set; }
    public float Base
    {
        get { return @base; }
        private set
        {
            if (value < 1)
                value = 1;
            @base = value;
        }
    }
    public float Current
    {
        get { return current; }
        private set
        {
            if (value < 0)
            {
                value = 0;
                //Add death method
            }
            else if (value > @base)
                value = @base;
            current = value;
        }
    }
    public string Name { get; private set; }

    #endregion
    public HP(float initMaxHP, float hPRegen)
    {
        HPRegen = hPRegen;
        Base = initMaxHP;
        Current = initMaxHP;
        Name = "HP";
    }

    #region Methods
    public void TakeDamage(float damage) => Current -= damage;
    public void Heal(float hPToHeal) => Current += hPToHeal;
    public void Update(float time) => Current += time * HPRegen;
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
    #endregion
}
public class AtkDamage : ModifiableStat //auto-attack damage
{
    #region Props
    public float Base { get; private set; }
    public float Current { get; private set; }
    public string Name { get; private set; }
    #endregion

    public AtkDamage(float baseDmg)
    {
        Base = baseDmg;
        Current = baseDmg;
        Name = "Auto-Attack Damage";
    }
    #region Methods
    public void ApplyModifier(float modifier) => Current *= modifier;
    public void EndModifier(float modifier) => Current /= modifier;
    public override string ToString()
    {
        StringBuilder sb = new StringBuilder();
        sb.AppendLine(Name);
        sb.AppendLine($"\t Base {Name} : {Base}");
        sb.AppendLine($"\t Current {Name} : {Current}");
        return sb.ToString();
    }
    #endregion
}
public class AtkSpeed : ModifiableStat
{
    #region Props
    float baseAtkSpeed;
    public float Base
    {
        get { return baseAtkSpeed; }
        private set
        {
            if (value < 0)
                throw new ArgumentException("BaseAtkSpeed must be higher than 0");
            baseAtkSpeed = value;
        }
    }
    public float Current { get; private set; }
    public string Name { get; private set; }
    #endregion

    public AtkSpeed(float baseAtkSpeedI)
    {
        Base = baseAtkSpeedI;
        Current = baseAtkSpeed;
        Name = "Attack Speed";
    }
    #region Methods
    public void ApplyModifier(float modifier) => Current *= modifier;
    public void EndModifier(float modifier) => Current /= modifier;
    public override string ToString()
    {
        StringBuilder sb = new StringBuilder();
        sb.AppendLine(Name);
        sb.AppendLine($"\t Base {Name} : {Base}");
        sb.AppendLine($"\t Current {Name} : {Current}");
        return sb.ToString();
    }
    #endregion
}
public class Speed : ModifiableStat
{
    #region Props
    public float Base { get; private set; }
    public float Current { get; private set; }
    public string Name { get; private set; }
    #endregion

    public Speed(float baseSpeed)
    {
        Base = baseSpeed;
        Current = baseSpeed;
        Name = "Speed";
    }
    #region Methods
    public void ApplyModifier(float modifier) => Current *= modifier;
    public void EndModifier(float modifier) => Current /= modifier;
    public override string ToString()
    {
        StringBuilder sb = new StringBuilder();
        sb.AppendLine(Name);
        sb.AppendLine($"\t Base {Name} : {Base}");
        sb.AppendLine($"\t Current {Name} : {Current}");
        return sb.ToString();
    }
    #endregion
}
public class Mana : ModifiableStat, RegenerativeStat
{
    #region Props
    float currentMana;
    float maxMana;

    public float ManaRegen { get; private set; }
    public float Current
    {
        get { return currentMana; }
        private set
        {
            if (value < 0)
                value = 0;
            currentMana = value;
        }
    }
    public float Base
    {
        get { return maxMana; }
        private set
        {
            if (value < 1)
                value = 1;
            maxMana = value;
        }
    }
    public string Name { get; private set; }
    #endregion
    public Mana(float maxManaI, float manaRegen)
    {
        ManaRegen = manaRegen;
        Base = maxManaI;
        Current = maxManaI;
        Name = "Mana";
    }
    #region Methods
    public void UseMana(float manaUsed) => Current -= manaUsed;
    public void ApplyModifier(float modifier) => ManaRegen *= modifier;
    public void EndModifier(float modifier) => ManaRegen /= modifier;
    public void Update(float time) => Current += ManaRegen * time;
    public override string ToString()
    {
        StringBuilder sb = new StringBuilder();
        sb.AppendLine(Name);
        sb.AppendLine($"\t Base {Name} : {Base}");
        sb.AppendLine($"\t Current {Name} : {Current}");
        return sb.ToString();
    }
    #endregion
}
public class XP
{
    #region Props
    public float Current { get; private set; }
    public string Name { get; private set; }
    #endregion
    public XP()
    {
        Current = 0;
        Name = "XP";
    }
    #region Methods
    public void AddXP(float xpAmount) => Current += xpAmount;
    public override string ToString()
    {
        StringBuilder sb = new StringBuilder();
        sb.AppendLine(Name);
        sb.AppendLine($"\t Current {Name} : {Current}");
        return sb.ToString();
    }
    #endregion
}
/*public class Level
{
    
    int levelNb;
    public int LevelNb
    {
        get { return levelNb; }
        private set
        {
            if (value < 1)
                value = 1;
            levelNb = value;
        }
    }
    public string Name { get; private set; }
    public Level(int initLevel)
    {
        LevelNb = initLevel;
        Name = "Level";
    }
}*/
#endregion
#region ActionsOnChange
public class OnHeal
{

}
public class OnTakenDamage
{

}
public class OnModifierChanged
{

}
#endregion
