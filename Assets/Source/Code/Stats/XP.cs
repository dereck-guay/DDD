using System.Text;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Miscellaneous;
using Interfaces;
public class XP
{
    float current;
    int level;
    const int MaxLevel = 3;
    public readonly float[] requiredXPPerLevel = new float[] { 15,30,50 };
    public float Current { get; private set; }
    public float XPYield { get; private set; }
    public int Level
    {
        get 
        {
            return level;
        }
        private set
        {
            if (value > MaxLevel)
                Debug.Log("Player is already max level.");
            else
                level = value;
        }
    }
    public XP(float xpYield)
    {
        Current = 0;
        Level = 1;
        XPYield = xpYield;
    }
    public void AddXP(float value)
    {
        if (Current + value > requiredXPPerLevel[Level - 1])
        {
            ++Level;
            AddXP(value);
            Debug.Log(value + " XP has been added");
        }
        else 
        {
            Current += value;
            Debug.Log(value + " XP has been added");
        }
    }

    public string Name = "XP";
    public override string ToString()
    {
        StringBuilder sb = new StringBuilder();
        sb.AppendLine(Name);
        sb.AppendLine($"\t Current {Name} : {Current}");
        return sb.ToString();
    }
}