using System.Collections;
using System.Collections.Generic;
using System.Text;
using System;
using UnityEngine;
using Miscellaneous;
using Interfaces;

public class AtkSpeedComponent : MonoBehaviour, IModifiableStat
{
    float baseAtkSpeed;
    public float Base
    {
        get { return baseAtkSpeed; }
        set
        {
            if (value < 0)
                throw new ArgumentException("BaseAtkSpeed must be higher than 0");
            baseAtkSpeed = value;
        }
    }
    public float Current { get; set; }
    public string Name = "Attack Speed";
    public void ApplyModifier(float modifier) => Current *= modifier;
    public void EndModifier(float modifier) => Current /= modifier;
    public void ApplyStats(float iBase) => Base = iBase;
    public override string ToString()
    {
        StringBuilder sb = new StringBuilder();
        sb.AppendLine(Name);
        sb.AppendLine($"\t Base {Name} : {Base}");
        sb.AppendLine($"\t Current {Name} : {Current}");
        return sb.ToString();
    }
}
