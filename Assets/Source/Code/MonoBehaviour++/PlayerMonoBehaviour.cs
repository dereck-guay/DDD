using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PlayerMonoBehaviour : MonoBehaviour
{
    public List<LayerMask> selectableEntities;
    public LayerMask rayCastHitLayers;

    [Serializable]
    public class CharacterParts
    {
        public GameObject body; // Used to determine wether a GameObject being targeted is itself
                                //Only useful for character with targeting abilities (Wizard) or classes that interact with their own bodies
    };

    [Header("Character Parts")]
    public CharacterParts characterParts;

    [HideInInspector]
    public Stats entityStats;

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

        if (Physics.Raycast(ray, out hit, Mathf.Infinity, rayCastHitLayers))
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
    protected bool ExistsAndIsntSelf(GameObject target) => target && target != characterParts.body;
    protected bool ExistsAndIsSelf(GameObject target) => target && target == characterParts.body;
}
