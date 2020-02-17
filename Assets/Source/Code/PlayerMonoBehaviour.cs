using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PlayerMonoBehaviour : MonoBehaviour
{
    public LayerMask selectableEntities;
    public LayerMask rayCastHitLayers;

    protected bool IsOnCooldown(Type spellComponent) => GetComponent(spellComponent) != null;

    protected Vector3 GetMouseDirection() =>
        (GetMousePositionOn2DPlane() - transform.position).normalized;

    protected Vector3 GetMousePositionOn2DPlane()
    {
        var position = Vector3.zero;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, Mathf.Infinity, rayCastHitLayers))
            position = hit.point;

        return position;
    }

    protected GameObject GetEntityAtMousePosition()
    {
        // Create disabled GO.
        GameObject GO = new GameObject();
        GO.SetActive(false);

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        // Fill GO with hit value.
        if (Physics.Raycast(ray, out hit, Mathf.Infinity, selectableEntities))
            GO = hit.collider.gameObject;

        // In function call always check:
        // if (GO.activeSelf) Do stuff;
        return GO;
    }
}
