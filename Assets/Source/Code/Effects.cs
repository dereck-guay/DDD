using System;
using System.Collections.Generic;
using System.Text;

public abstract class Effect
{
    public float NumberValue { get; protected set; }
}
public class Buff : Effect
{
    public Stat StatToBuff { get; private set; }
    public Buff(int buffValue, Stat statToBuff)
    {
        NumberValue = buffValue;
        StatToBuff = statToBuff;
    }
}
public class Heal : Effect
{
    public Heal(int healingValue)
    {
        NumberValue = healingValue;
    }
}
public class Damage : Effect
{
    public Damage(int damageValue)
    {
        NumberValue = damageValue;
    }
}
