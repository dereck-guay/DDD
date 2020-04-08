using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PlayerMonoBehaviour : EntityMonoBehaviour
{
    public List<LayerMask> selectableEntities;
    public LayerMask rayCastHitLayer;

    [Serializable]
    public class CharacterParts
    {
        public GameObject body; // Used to determine wether a GameObject being targeted is itself
                                //Only useful for character with targeting abilities (Wizard) or classes that interact with their own bodies
    };

    [Header("Character Parts")]
    public CharacterParts characterParts;

    protected bool IsOnCooldown(Type spellComponent) => GetComponent(spellComponent) != null;

    protected Vector3 GetMouseDirection() =>
        (GetMousePositionOn2DPlane() - transform.position).normalized;

    //------------------------------
    // NE VIENS PAS DE NOUS
    //
    protected Vector3 GetMousePositionOn2DPlane()
    {
        var position = Vector3.zero;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, Mathf.Infinity, rayCastHitLayer))
            position = hit.point;

        return position;
    }
    //
    //-------------------------------

    protected GameObject GetEntityAtMousePosition()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        foreach (var layer in selectableEntities)
            if (Physics.Raycast(ray, out hit, Mathf.Infinity, layer))
                return hit.collider.gameObject;

        return null;
    }

    protected bool TargetIsWithinRange(GameObject target, float range) =>
       (target.transform.position - transform.position).magnitude < range;
    protected bool CanCast(float manaCost, Type spell) =>
        entityStats.Mana.Current > manaCost && !IsOnCooldown(spell);
    protected bool ExistsAndIsntSelf(GameObject target) => target && target != this.gameObject;
    protected bool ExistsAndIsSelf(GameObject target) => target && target == this.gameObject;
}
