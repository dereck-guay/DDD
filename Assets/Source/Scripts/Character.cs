using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public enum ClassNames { Fighter, Wizard }
public class Character : MonoBehaviour
{
    Dictionary<int, IPlayableClass> pairs = new Dictionary<int, IPlayableClass>();
    public ClassNames ClassName;
    public IPlayableClass playableClass;
    public Spell[] Spells;
    #region Methods
    public void CastSpell(Spell spell)
    {
        spell.Cast(this);
    }
    #endregion
    private void Start()
    {
        pairs.Add(0, new Fighter());
        pairs.Add(1, new Wizard());
    }
    private void Awake()
    {
        pairs.TryGetValue((int)ClassName, out playableClass);
    }
    private void Update()
    {
        
    }

}
