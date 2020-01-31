using System;
using System.Collections.Generic;
using System.Text;

public abstract class Effect
{
    public string Name { get; private set; }
}
public class Buff : Effect
{
    int valueOfBuff;
    public int ValueOfBuff
    {
        get { return valueOfBuff; }
        set { valueOfBuff = value; }

    }
    public Stat StatToBuff { get; private set; }
    public Buff(int buffValue, Stat statToBuff)
    {
        ValueOfBuff = buffValue;
        StatToBuff = statToBuff;
    }
}
public class AOE : Effect
{

}
public class SingleTarget : Effect
{

}
public class RangedAttack : Effect
{

}
