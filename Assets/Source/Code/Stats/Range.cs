using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text;

public class Range
{
    public float Base { get; set; }
    public float Current { get; set; }
    public string Name = "Range";
    public Range(float rangeBase)
    {
        Base = rangeBase;
        Current = rangeBase;
    }
    public void ApplyModifier(float modifier) => Current *= modifier;
    public void EndModifier(float modifier) => Current /= modifier;
    public override string ToString()
    {
        StringBuilder sb = new StringBuilder();
        sb.AppendLine(Name);
        sb.AppendLine($"\t Base {Name} : {Base}");
        sb.AppendLine($"\t Current {Name} : {Current}");
        return sb.ToString();
    }
}
