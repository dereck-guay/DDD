using System;
using System.Collections.Generic;
using System.Text;
using Miscellaneous;
using Interfaces;

public interface IEffect
{
    void ApplyEffect(ITargetable target);
}
public class Buff : IEffect
{
    public float NumberValue { get; private set; }
    public int IndexOfStatToBuff { get; private set; }
    public Buff(float buffValue, int statIndex)
    {
        NumberValue = buffValue;
        IndexOfStatToBuff = statIndex;
    }
    public void ApplyEffect(ITargetable target)
    {
        target.ModifiableStatDictionary[IndexOfStatToBuff].ApplyModifier(NumberValue);
    }
}
public class Heal : IEffect
{
    public float NumberValue { get; private set; }
    public Heal(float healingValue)
    {
        NumberValue = healingValue;
    }
    public void ApplyEffect(ITargetable target)
    {
        target.HP.Heal(NumberValue);
    }
}
public class Damage : IEffect
{
    public float NumberValue { get; private set; }
    public Damage(float damageValue)
    {
        NumberValue = damageValue;
    }
    public void ApplyEffect(ITargetable target)
    {
        target.HP.TakeDamage(NumberValue);
    }
}
