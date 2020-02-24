using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity : MonoBehaviour
{
    public AtkDamage AtkDamage { get; protected set; }
    public AtkSpeed AtkSpeed { get; protected set; }
    public HP HP { get; protected set; }
    public Mana Mana { get; protected set; }
    public Speed Speed { get; protected set; }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
