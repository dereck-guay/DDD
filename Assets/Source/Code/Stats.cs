using System;

public interface ModifiableStat
{
    void ApplyModifier(float modifier);
    void EndModifier(float modifier);
}
public interface RegenerativeStat
{
    void Update(float time);
}
#region Stats
public class HP : ModifiableStat, RegenerativeStat
{
    #region Props
    float maxHP;
    float hPValue;

    public float HPRegen { get; private set; }
    public float MaxHP
    {
        get { return maxHP; }
        private set
        {
            if (value < 1)
                value = 1;
            maxHP = value;
        }
    }
    public float CurrentHP
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
    public string Name { get; private set; }

    #endregion
    public HP(float initMaxHP, float hPRegen)
    {
        HPRegen = hPRegen;
        MaxHP = initMaxHP;
        CurrentHP = initMaxHP;
        Name = "HP";
    }

    #region Methods
    public void TakeDamage(float damage) => CurrentHP -= damage;
    public void Heal(float hPToHeal) => CurrentHP += hPToHeal;
    public void Update(float time) => CurrentHP += time * HPRegen;
    public void ApplyModifier(float modifier) => HPRegen *= modifier;
    public void EndModifier(float modifier) => HPRegen /= modifier;
    #endregion
}
public class AtkDamage : ModifiableStat //auto-attack damage
{
    #region Props
    public float BaseDmg { get; private set; }
    public float CurrentDmg { get; private set; }
    public string Name { get; private set; }
    #endregion

    public AtkDamage(float baseDmg)
    {
        BaseDmg = baseDmg;
        CurrentDmg = baseDmg;
        Name = "Auto-Attack Damage";
    }
    #region Methods
    public void ApplyModifier(float modifier) => CurrentDmg *= modifier;
    public void EndModifier(float modifier) => CurrentDmg /= modifier;
    #endregion
}
public class AtkSpeed : ModifiableStat
{
    #region Props
    float baseAtkSpeed;
    public float BaseAtkSpeed
    {
        get { return baseAtkSpeed; }
        private set
        {
            if (value < 0)
                throw new ArgumentException("BaseAtkSpeed must be higher than 0");
            baseAtkSpeed = value;
        }
    }
    public float CurrentAtkSpeed { get; private set; }
    public string Name { get; private set; }
    #endregion

    public AtkSpeed(float baseAtkSpeedI)
    {
        BaseAtkSpeed = baseAtkSpeedI;
        CurrentAtkSpeed = baseAtkSpeed;
        Name = "AtkSpeed";
    }
    #region Methods
    public void ApplyModifier(float modifier) => CurrentAtkSpeed *= modifier;
    public void EndModifier(float modifier) => CurrentAtkSpeed /= modifier;
    #endregion
}
public class Speed : ModifiableStat
{
    #region Props
    public float BaseSpeed { get; private set; }
    public float CurrentSpeed { get; private set; }
    public string Name { get; private set; }
    #endregion

    public Speed(float baseSpeed)
    {
        BaseSpeed = baseSpeed;
        CurrentSpeed = baseSpeed;
        Name = "Speed";
    }
    #region Methods
    public void ApplyModifier(float modifier) => CurrentSpeed *= modifier;
    public void EndModifier(float modifier) => CurrentSpeed /= modifier;
    #endregion
}
public class Mana : ModifiableStat, RegenerativeStat
{
    #region Props
    float currentMana;
    float maxMana;

    public float ManaRegen { get; private set; }
    public float CurrentMana
    {
        get { return currentMana; }
        private set
        {
            if (value < 0)
                value = 0;
            currentMana = value;
        }
    }
    public float MaxMana
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
        MaxMana = maxManaI;
        CurrentMana = maxManaI;
        Name = "Mana";
    }
    #region Methods
    public void UseMana(float manaUsed) => CurrentMana -= manaUsed;
    public void ApplyModifier(float modifier) => ManaRegen *= modifier;
    public void EndModifier(float modifier) => ManaRegen /= modifier;
    public void Update(float time) => CurrentMana += ManaRegen * time;
    #endregion
}
public class XP
{
    #region Props
    public float CurrentXP { get; private set; }
    public string Name { get; private set; }
    #endregion
    public XP()
    {
        CurrentXP = 0;
        Name = "XP";
    }
    #region Methods
    public void AddXP(float xpAmount) => CurrentXP += xpAmount;
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
