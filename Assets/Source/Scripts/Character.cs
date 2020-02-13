using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using Miscellaneous;
using System.Reflection;
using System.Linq;
using Interfaces;


public enum ClassNames { Fighter, Wizard }
public class Character : MonoBehaviour
{
    IPlayableClass characterClass;
    DictionaryCreator<GameObject> pairs;
    public ClassNames CharacterClassName;
    public IPlayableClass CharacterClass
    {
        get { return characterClass; }
        private set
        {
            characterClass = value;
            AssignStats();
        }
    }
    public Spell[] Spells;

    public AtkSpeed AtkSpeed;
    public AtkDamage AtkDamage;
    public HP HP;
    public Mana Mana;
    public Speed Speed;
    public XP XP;
    IModifiableStat[] ClassSpecificStats;
    public DictionaryCreator<IModifiableStat> ModifiableStatDictionary { get; private set; }
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
        target.TakeDamage(AtkDamage.Current);
    }
    public void TakeDamage(float damage)
    {
        HP.TakeDamage(damage);
    }

    #endregion
    private void Awake()
    {
        var playableClasses = new GameObject[2]
        {
            Resources.Load("Prefabs\\Fighter") as GameObject,
            Resources.Load("Prefabs\\Wizard") as GameObject,
        };
        pairs = new DictionaryCreator<GameObject>(playableClasses);
        // Ceci est surtout pour faire les tests, pouvoir rapidement changer de classe dans l'inspecteur par le enum
    }
    private void Start()
    {
        CharacterClass = pairs[(int)CharacterClassName].GetComponent<IPlayableClass>();
        Spells = CharacterClass.Spells;
    }
    private void Update()
    {

    }
    void AssignStats()
    {
        for (int i = 0; i < ClassSpecificStats.Length; ++i)
            ClassSpecificStats[i] = CharacterClass.ClassStats[i];
    }

}


