using System.Collections;
using System.Collections.Generic;
using System.Text;
using System;
using UnityEngine;
using Miscellaneous;
using Interfaces;
public class Speed : IModifiableStat
{
    float current;

    public float Base { get; set; }
    public float Current
    {
        get { return current; }
        set
        {
            current = value;
            OnSpeedChanged?.Invoke(value);
        }
    }
    public string Name = "Speed";
    public Speed(float speedBase)
    {
        Base = speedBase;
        Current = speedBase;
    }

    public Action<float> OnSpeedChanged { get; set; }
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
