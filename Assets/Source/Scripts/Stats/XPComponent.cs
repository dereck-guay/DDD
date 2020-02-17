using System.Text;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Miscellaneous;
using Interfaces;
public class XPComponent : MonoBehaviour
{
    public float Current { get; set; }
    public int Level { get; } // Level is going to be calculated with xp, can't be set.

    public string Name = "XP";
    public void AddXP(float xpAmount) => Current += xpAmount;
    public override string ToString()
    {
        StringBuilder sb = new StringBuilder();
        sb.AppendLine(Name);
        sb.AppendLine($"\t Current {Name} : {Current}");
        return sb.ToString();
    }
}