using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Miscellaneous;

namespace Interfaces
{
    public interface IPlayableClass : ICaster
    {
    }
    public interface IRegenerativeStat
    {
        void Regen(float time);
    }
    public interface IModifiableStat
    {
        void ApplyModifier(float modifier);
        void EndModifier(float modifier);
        float Base { get; }
        float Current { get; }
    }
    public interface ITypeOfSpell
    {
        float Range { get; }
        float Radius { get; }
    }
    public interface ICaster
    {
    }
}
