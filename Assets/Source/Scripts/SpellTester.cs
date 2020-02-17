using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Miscellaneous;
using Interfaces;
using Enums;
public class SpellTester : MonoBehaviour, ICaster
{
    DictionaryCreator<IPlayableClass> pairs;
    DictionaryCreator<Spell> spells;
    public ClassNames ClassName;
    public SpellList Spell;
    public IPlayableClass playableClass;
    public Spell ActivatedSpell;
    public string Name;

    public Spell[] Spells => throw new System.NotImplementedException();

    public ManaComponent Mana => throw new System.NotImplementedException();

    #region Methods
    public void CastSpell(Spell spell)
    {
        spell.Cast();
    }
    #endregion
    private void Awake()
    {
        //var playableClasses = new IPlayableClass[2]
        //{
        //    //new FighterComponent(),
        //    //new WizardComponent(),
        //};
        //pairs = new DictionaryCreator<IPlayableClass>(playableClasses);
        var availableSpells = new Spell[]
        {
            new Spell(4, "Fireball", $"Shoot a large fireball that deals {5} damage on-hit", new Damage(5), new SkillShot(10, 4), 4, this),
            new Spell(4, "Heal", $"Heal a target within range for {5} health", new Heal(5), new SingleTarget(2), 4, this),
            new Spell(4, "Slow", "Decrease a target's speed", new Damage(5), new SkillShot(10, 4), 4, this),
        };
        spells = new DictionaryCreator<Spell>(availableSpells);

        // Ceci est surtout pour faire les tests, pouvoir rapidement changer de classe et de spell dans l'inspecteur par le enum
    }
    private void Start()
    {
        //playableClass = pairs[(int)ClassName];
        //ActivatedSpell = spells[(int)Spell];
        //Name = ActivatedSpell.Name;
    }
    private void Update()
    {
        ActivatedSpell = spells[(int)Spell];
        Name = ActivatedSpell.Name;
    }

}
