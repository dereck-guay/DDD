using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PlayerMonoBehaviour : EntityMonoBehaviour
{
    public new Camera camera;
    [HideInInspector]
    public bool spellLocked = false;

    #region Auto-attack stuff
    protected bool canAttack;
    private float timeSinceLastAttack;
    protected float TimeSinceLastAttack
    {
        get { return timeSinceLastAttack; }
        set
        {
            if (value > 1 / entityStats.AtkSpeed.Current)
                canAttack = true;
            timeSinceLastAttack = value;
        }
    }
    #endregion
    protected Rigidbody rigidBody;

    public List<LayerMask> selectableEntities;
    public LayerMask rayCastHitLayer;
    public RespawnManagerComponent respawnManager;

    public StatusBarsComponent[] statusBars;

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

    protected void Move(Vector3 direction)
    {
        if (!IsStunned)
            rigidBody.AddForce(direction * entityStats.Speed.Current * Time.deltaTime * 100f);
    }

    // Makes the character face the direction of the mouse
    protected void DirectCharacter()
    {
        var directionToLookAt = transform.position + GetMouseDirection();
        directionToLookAt.y = transform.position.y;
        transform.LookAt(directionToLookAt);
    }

    protected void InitializePlayer()
    {
        canAttack = true;

        entityStats = GetComponent<Stats>();
        entityStats.ApplyStats(statsInit);

        rigidBody = GetComponent<Rigidbody>();
        TimeSinceLastAttack = 0;

        statusBars[0].SetMax(entityStats.HP.Base);
        statusBars[1].SetMax(entityStats.Mana.Base);
        entityStats.HP.OnTakeDamage += damage => statusBars[0].SetCurrent(entityStats.HP.Current);
        entityStats.HP.OnHeal += regen => statusBars[0].SetCurrent(entityStats.HP.Current);
        entityStats.Mana.OnUse += manaCost => statusBars[1].SetCurrent(entityStats.Mana.Current);
        entityStats.Mana.OnRegen += manaCost => statusBars[1].SetCurrent(entityStats.Mana.Current);

        entityStats.HP.OnDeath += () => Respawn();
    }

    protected void UpdatePlayer()
    {
        TimeSinceLastAttack += Time.deltaTime;
        if (!(IsStunned || spellLocked))
        {
            DirectCharacter();
            ManageInputs();
        }
        entityStats.Regen();

        camera.transform.position = new Vector3(
            transform.position.x,
            camera.transform.position.y,
            transform.position.z - 5
        ); // Moves the camera according to the player.
    }

    protected virtual void ManageInputs()
    {
        //Movement
        if (Input.GetKey(KeybindManager.Instance.KeyBinds["UP"]))
        {
            Move(Vector3.forward);
        }
        if (Input.GetKey(KeybindManager.Instance.KeyBinds["LEFT"]))
        {
            Move(Vector3.left);
        }
        if (Input.GetKey(KeybindManager.Instance.KeyBinds["DOWN"]))
        {
            Move(Vector3.back);
        }
        if (Input.GetKey(KeybindManager.Instance.KeyBinds["RIGHT"]))
        {
            Move(Vector3.right);
        }
    }

    IEnumerator CoRespawn(Vector3 respawnPoint)
    {
        IsStunned = true;
        transform.Translate(20 * Vector3.down);

        yield return new WaitForSeconds(respawnManager.respawnDelay);

        entityStats.HP.Heal(entityStats.HP.Base);
        statusBars[0].SetCurrent(entityStats.HP.Current);

        transform.position = respawnPoint;

        IsStunned = false;
    }
    public void Respawn(Vector3 respawnPoint) => StartCoroutine("CoRespawn", respawnPoint);
    public void Respawn() => Respawn(respawnManager.GetRandomRespawnPoint());

    protected bool TargetIsWithinRange(GameObject target, float range) =>
       (target.transform.position - transform.position).magnitude < range;
    protected bool CanCast(float manaCost, Type spell) =>
        entityStats.Mana.Current > manaCost && !IsOnCooldown(spell);
    protected bool ExistsAndIsntSelf(GameObject target) => target && target != this.gameObject;
    protected bool ExistsAndIsSelf(GameObject target) => target && target == this.gameObject;
}
