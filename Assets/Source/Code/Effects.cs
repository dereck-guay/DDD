using System;
using System.Collections.Generic;
using System.Text;

public interface IEffect
{
    void ApplyEffect();
}
public class Buff : IEffect
{
    public Character Caster { get; private set; }
    public float NumberValue { get; private set; }
    public ModifiableStat StatToBuff { get; private set; }
    public Buff(float buffValue, ModifiableStat statToBuff, Character casterI)
    {
        NumberValue = buffValue;
        StatToBuff = statToBuff;
        Caster = casterI;
    }
    public void ApplyEffect()
    {
        //Caster.Whatever method buffs the casters stats
    }
}
public class Heal : IEffect
{
    public Character Caster { get; private set; }
    public float NumberValue { get; private set; }
    public Heal(float healingValue, Character casterI)
    {
        NumberValue = healingValue;
        Caster = casterI;
    }
    public void ApplyEffect()
    {
        //Caster.Whatever method heals the caster
    }
}
public class Damage : IEffect
{
    public Character Caster { get; private set; }
    public float NumberValue { get; private set; }
    public Damage(float damageValue, Character casterI)
    {
        NumberValue = damageValue;
        Caster = casterI;
    }
    public void ApplyEffect()
    {
        //Caster.Whatever method damage the caster
    }
}
