using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Enums
{
    public enum ModifiableStats { AtkDamage, AtkSpeed, HPRegen, ManaRegen, Speed };
    public enum ClassNames { Fighter, Wizard }
    public enum SpellList { Fireball, Heal, Slow }
    public enum Layers { Default, TransparentFX, IgnoreRaycast, Water = 4, UI, PlayerMousePosition = 8, Objects, Players, Floor, Hole, Projectile, Enemies, EnemyProjectile = 16 }
}
