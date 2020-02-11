using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using Miscellaneous;
using System.Reflection;
using System.Linq;

public enum ClassNames { Fighter, Wizard }
public class Character : MonoBehaviour
{
    DictionaryCreator<IPlayableClass> pairs;
    public ClassNames ClassName;
    public IPlayableClass playableClass;
    public Spell[] Spells;

    #region Methods
    public void CastSpell(Spell spell)
    {
        spell.Cast(this);
    }
    #endregion
    private void Awake()
    {
        var playableClasses = new IPlayableClass[2]
        {
            new Fighter(),
            new Wizard(),
        };
        pairs = new DictionaryCreator<IPlayableClass>(playableClasses);
        // Ceci est surtout pour faire les tests, pouvoir rapidement changer de classe dans l'inspecteur par le enum
    }
    private void Start()
    {
        playableClass = pairs[(int)ClassName];
        Spells = playableClass.Spells;
        Debug.Log(playableClass.ToString());
    }
    private void Update()
    {
        
    }

}
