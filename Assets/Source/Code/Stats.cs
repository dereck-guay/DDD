using System.Text;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Miscellaneous;
using Interfaces;
public enum ModifiableStats { AtkDamage, AtkSpeed, HP, Mana, Speed };
public class HP : MonoBehaviour, IModifiableStat, IRegenerativeStat
{
    #region Props
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
    #region Actions
    public Action OnDeath { get; set; }
    public Action OnTakeDamage { get; set; }
    #endregion
    #endregion
    private void Update()
    {
        Regen(Time.deltaTime);
    }
    #region Methods
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
    #endregion
}
public class AtkDamage : MonoBehaviour, IModifiableStat //auto-attack damage
{
    #region Props
    public float Base { get; set; }
    public float Current { get; set; }
    public string Name = "Auto-Attack Damage";
    #region Methods
    public void ApplyModifier(float modifier) => Current *= modifier;
    public void ApplyStats(float iBase) => Base = iBase;
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
public class AtkSpeed : MonoBehaviour, IModifiableStat
{
    #region Props
    float baseAtkSpeed;
    public float Base
    {
        get { return baseAtkSpeed; }
        set
        {
            if (value < 0)
                throw new ArgumentException("BaseAtkSpeed must be higher than 0");
            baseAtkSpeed = value;
        }
    }
    public float Current { get; set; }
    public string Name = "Attack Speed";
    #endregion
    #region Methods
    public void ApplyModifier(float modifier) => Current *= modifier;
    public void EndModifier(float modifier) => Current /= modifier;
    public void ApplyStats(float iBase) => Base = iBase;
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
public class Speed : MonoBehaviour, IModifiableStat
{
    #region Props
    public float Base { get; set; }
    public float Current { get; set; }
    public string Name = "Speed";
    #endregion
    #region Methods
    public void ApplyModifier(float modifier) => Current *= modifier;
    public void EndModifier(float modifier) => Current /= modifier;
    public void ApplyStats(float iBase) => Base = iBase;
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
public class Mana : MonoBehaviour, IModifiableStat, IRegenerativeStat
{
    #region Props
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
    #endregion
    private void Update() => Regen(Time.deltaTime);
    #region Methods
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
    #endregion
}
public class XP : MonoBehaviour
{
    #region Props
    public float Current { get; set; }
    public string Name = "XP";
    #endregion
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
public class StatChangeArgs
{

}
#endregion
