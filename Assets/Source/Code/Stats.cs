using System;
using System.Collections.Generic;
using System.Text;

public abstract class Stat
{
    public string Name { get; protected set; }
}
#region Stats
public class HP : Stat
{
    #region Props
    int maxHP;
    int hPValue;
    public int MaxHP
    {
        get { return maxHP; }
        private set
        {
            if (value < 1)
                value = 1;
            maxHP = value;
        }
    }
    public int HPValue 
    { 
        get { return hPValue; }
        private set
        {
            if (value < 0)
            {
                value = 0;
                //Add death method
            }
            else if (value > maxHP)
                value = maxHP;
            hPValue = value;
        }
    }
        
    #endregion
    public HP(int hPValue)
    {
        HPValue = hPValue;
        Name = "HP";
    }
    #region Methods
    public void TakeDamage(int damage) => HPValue -= damage;
    public void Heal(int hPToHeal) => HPValue += hPToHeal;
    #endregion
}
public class AtkSpeed : Stat
{
    public float BaseAtkSpeed { get; private set; }
    public float CurrentAtkSpeed { get; private set; }
    public AtkSpeed(float baseAtkSpeed)
    {
        BaseAtkSpeed = baseAtkSpeed;
        CurrentAtkSpeed = baseAtkSpeed;
        Name = "AtkSpeed";
    }
}
public class Speed : Stat
{
    public float BaseSpeed { get; private set; }
    public float CurrentSpeed { get; private set; }
    public Speed(float baseSpeed)
    {
        BaseSpeed = baseSpeed;
        CurrentSpeed = baseSpeed;
        Name = "Speed";
    }
}
public class Mana : Stat
{
    #region Props
    int currentMana;
    int maxMana;
    public int CurrentMana
    {
        get { return currentMana; }
        private set
        {
            if (value < 0)
                value = 0;
            currentMana = value;
        }
    }
    public int MaxMana
    {
        get { return maxMana; }
        private set
        {
            if (value < 1)
                value = 1;
            maxMana = value;
        }
    }
    #endregion
    public Mana(int maxManaI)
    {
        MaxMana = maxMana;
        Name = "Mana";
    }
    public void UseMana(int manaUsed) => CurrentMana -= manaUsed;
}
public class XP : Stat
{
    float currentXP;
    public float CurrentXP
    {
        get { return currentXP; }
        private set
        {
            currentXP = value;
        }
    }
     
    public XP()
    {
        CurrentXP = 0;
        Name = "XP";
    }
}
#endregion
#region ActionsOnChange
public class OnHeal
{
        
}
public class OnTakenDamage
{

}
#endregion
