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
    public Stat StatToBuff { get; private set; }
    public Buff(float buffValue, Stat statToBuff, Character casterI)
    {
        NumberValue = buffValue;
        StatToBuff = statToBuff;
        Caster = casterI;
    }
    public void ApplyEffect()
    {
        Caster.
    }
}
public class Heal : IEffect
{
    public float NumberValue { get; private set; }
    public Heal(float healingValue)
    {
        NumberValue = healingValue;
    }
}
public class Damage : IEffect
{
    public float NumberValue { get; private set; }
    public Damage(float damageValue)
    {
        NumberValue = damageValue;
    }
}
