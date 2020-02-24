using System.Text;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Miscellaneous;
using Interfaces;
public class XP
{
    int level;
    int maxLevel = 4;
    readonly float[] requiredXPPerLevel = new float[] { 1,2,3 };
    public float Current { get; private set; }
    public int Level
    {
        get { return level; }
        private set
        {
            if (value > maxLevel)
                Debug.Log("Player is already max level.");
            else
                level = value;
        }
    }
    public XP()
    {
        Current = 0;
        Level = 1;
    }
    void AddXP(float value)
    {
        if (Current + value > requiredXPPerLevel[Level - 1])
        {
            ++Level;
            Current += requiredXPPerLevel[Level];
            value -= requiredXPPerLevel[Level];
            AddXP(value);
        }
        else { Current += value; }
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