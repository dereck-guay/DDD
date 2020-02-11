using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public interface ITypeOfSpell
{
    float Range { get; }
    float Radius { get; }
}
public class SingleTarget : ITypeOfSpell
{
    float range;
    float radius;
    public float Range
    {
        get { return range; }
        private set
        {
            if (value < 0)
                value = 0;
            range = value;
        }
    }
    public float Radius
    {
        get { return radius; }
        private set
        {
            if (value < 0)
                value = 0;
            radius = value;
        }
    }

    //public Entity Target { get; private set; }

    /*public SingleTarget(float range, Entity target)
    {

    }*/ // Create entity you coward
    public override string ToString()
    {
        StringBuilder sb = new StringBuilder();
        sb.AppendLine("Single Target spell");
        sb.AppendLine($"Range : {Range}");
        sb.AppendLine($"Radius : {Radius}");
        return sb.ToString();
    }
}
public class SkillShot : ITypeOfSpell
{
    float range;
    float radius;
    public float Range
    {
        get { return range; }
        private set
        {
            if (value < 0)
                value = 0;
            range = value;
        }
    }
    public float Radius
    {
        get { return radius; }
        private set
        {
            if (value < 0)
                value = 0;
            radius = value;
        }
    }
    public SkillShot(float initRange, float initRadius)
    {
        Range = initRange;
        Radius = initRadius;
    }

}
