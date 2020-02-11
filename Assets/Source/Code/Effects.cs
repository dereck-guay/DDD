using System;
using System.Collections.Generic;
using System.Text;

public interface IEffect
{
    void ApplyEffect();
}
public class Buff : IEffect
{
    public float NumberValue { get; private set; }
    public ModifiableStat StatToBuff { get; private set; }
    public Buff(float buffValue, ModifiableStat statToBuff)
    {
        NumberValue = buffValue;
        StatToBuff = statToBuff;
    }
    public void ApplyEffect()
    {
        //Caster.Whatever method buffs the casters stats
    }
}
public class Heal : IEffect
{
    public float NumberValue { get; private set; }
    public Heal(float healingValue)
    {
        NumberValue = healingValue;
    }
    public void ApplyEffect()
    {
        //Caster.Whatever method heals the caster
    }
}
public class Damage : IEffect
{
    public float NumberValue { get; private set; }
    public Damage(float damageValue)
    {
        NumberValue = damageValue;
    }
    public void ApplyEffect()
    {
        //Caster.Whatever method damage the caster
    }
}
