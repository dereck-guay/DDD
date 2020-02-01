using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public abstract class TypeOfSpell
{
    float range;
    public float Range
    {
        get { return range; }
        protected set
        {
            if (value < 0)
                throw new ArgumentException("Range cannot be smaller than 0");
            range = value;
        }
    }
}
public class AOE : TypeOfSpell
{
    float radius;
    public float Radius
    {
        get { return radius; }
        private set
        {
            if (value < 0)
                throw new ArgumentException("Radius cannot be smaller than 0");
            radius = value;
        }
    }
    public override string ToString()
    {
        StringBuilder sb = new StringBuilder("Area of effect spell");
        sb.AppendLine();
        sb.Append($"Range : {Range}");
        sb.AppendLine();
        sb.Append($"Radius : {Radius}");

        return sb.ToString();
    }
}
public class SingleTarget : TypeOfSpell
{
    //public Entity Target { get; private set; }

    /*public SingleTarget(float range, Entity target)
    {

    }*/ // Create entity you coward
    public override string ToString()
    {
        StringBuilder sb = new StringBuilder("Single Target spell");
        sb.AppendLine();
        sb.Append($"Range : {Range}");

        return sb.ToString();
    }
}
public class RangedAttack : TypeOfSpell
{

}
