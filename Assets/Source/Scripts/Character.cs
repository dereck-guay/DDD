using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using Miscellaneous;
using System.Reflection;
using System.Linq;

public enum ClassNames { Fighter, Wizard }
public class Character : MonoBehaviour, ITargetable, IDamageable
{
    IPlayableClass characterClass;
    DictionaryCreator<IPlayableClass> pairs;
    public ClassNames CharacterClassName;
    
    public Spell[] Spells;
    public IPlayableClass CharacterClass
    {
        get { return characterClass; }
        private set
        {
            characterClass = value;
            AssignStats();
            Debug.Log(CharacterClass.ToString());
        }
    }
    public AtkDamage AtkDamage { get; private set; }
    public AtkSpeed AtkSpeed { get; private set; }
    public HP HP { get; private set; }
    public Mana Mana { get; private set; }
    public Speed Speed { get; private set; }
    public XP XP { get; private set; }

    IStat[] classSpecificStats;
    #region Methods
    public void CastSpell(Spell spell)
    {
        spell.Cast(this);
    }
    public void Attack(Entity target)
    {
        //target.TakeDamage(CharacterClass.AtkDamage.Current);
    }
    public void Attack(Character target)
    {
        target.TakeDamage(CharacterClass.AtkDamage.Current);
    }
    public void TakeDamage(float damage)
    {
        CharacterClass.HP.TakeDamage(damage);
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
        classSpecificStats = new IStat[]
        {
            AtkDamage,
            AtkSpeed,
            HP,
            Mana,
            Speed,
        };
        // Ceci est surtout pour faire les tests, pouvoir rapidement changer de classe dans l'inspecteur par le enum
    }
    private void Start()
    {
        CharacterClass = pairs[(int)CharacterClassName];
        Spells = CharacterClass.Spells;
    }
    private void Update()
    {
        if(CharacterClass != pairs[(int)CharacterClassName])
        {
            CharacterClass = pairs[(int)CharacterClassName];
            CharacterClass.ToString();
        }
    }
    void AssignStats()
    {
        for (int i = 0; i < classSpecificStats.Length; ++i)
            classSpecificStats[i] = CharacterClass.ClassSpecificStats[i];
    }

}


