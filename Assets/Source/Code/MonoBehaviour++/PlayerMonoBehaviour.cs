using System;
using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
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
    public XPBarComponent xPBar;

    [SerializeField]
    uint deathScorePenalty = 50;
    public int Score { get; private set; }

    [Serializable]
    public class CharacterParts
    {
        public GameObject body; // Used to determine wether a GameObject being targeted is itself
                                //Only useful for character with targeting abilities (Wizard) or classes that interact with their own bodies
    };

    [Header("Character Parts")]
    public CharacterParts characterParts;

    [Header("Audio Settings")]
    public string DeathSoundName = "Death Sound";
    public string SpawnSoundName = "Spawn Sound";

    Vector3 movementDirection;

    protected bool IsOnCooldown(Type spellComponent) => GetComponent(spellComponent) != null;

    protected Vector3 GetMouseDirection() =>
        (GetMousePositionOn2DPlane() - transform.position).normalized;

    public void AddScore(int value) => Score += value;

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
            rigidBody.AddForce(direction.normalized * entityStats.Speed.Current * Time.deltaTime * 100f);
    }

    // Makes the character face the direction of the mouse
    protected void DirectCharacter()
    {
        if (PauseMenuComponent.GameIsPaused == false)
        {
            var directionToLookAt = transform.position + GetMouseDirection();
            directionToLookAt.y = transform.position.y;
            transform.LookAt(directionToLookAt);
        }
    }

    protected void InitializePlayer()
    {
        canAttack = true;

        entityStats = GetComponent<Stats>();
        entityStats.ApplyStats(statsInit);

        rigidBody = GetComponent<Rigidbody>();
        TimeSinceLastAttack = 0;

        Score = 0;

        statusBars[0].SetMax(entityStats.HP.Base);
        statusBars[1].SetMax(entityStats.Mana.Base);
        entityStats.HP.OnTakeDamage += (damage, player) => statusBars[0].SetCurrent(entityStats.HP.Current);
        entityStats.HP.OnHeal += regen => statusBars[0].SetCurrent(entityStats.HP.Current);
        entityStats.Mana.OnUse += manaCost => statusBars[1].SetCurrent(entityStats.Mana.Current);
        entityStats.Mana.OnRegen += manaCost => statusBars[1].SetCurrent(entityStats.Mana.Current);

        entityStats.HP.OnDeath += () => Respawn();
        entityStats.HP.OnDeath += () => FindObjectOfType<AudioManager>().Play(DeathSoundName);
        entityStats.HP.OnDeath += () => AddScore(-(int)deathScorePenalty);

        transform.position = RespawnManagerComponent.RMC.GetRandomRespawnPoint();
    }

    protected void UpdatePlayer()
    {
        TimeSinceLastAttack += Time.deltaTime;
        if (!(IsStunned || spellLocked))
        {
            DirectCharacter();
            ManageInputs();
        }
        xPBar.GainXP(entityStats.XP.Current, entityStats.XP.requiredXPPerLevel[entityStats.XP.Level - 1], entityStats.XP.Level);
        entityStats.Regen();

        camera.transform.position = new Vector3(
            transform.position.x,
            camera.transform.position.y,
            transform.position.z - 5
        ); // Moves the camera according to the player.
    }

    protected virtual void ManageInputs()
    {
        movementDirection = Vector3.zero;

        //Movement
        if (Input.GetKey(KeybindManager.Instance.KeyBinds["UP"]))
            movementDirection += Vector3.forward;
        if (Input.GetKey(KeybindManager.Instance.KeyBinds["LEFT"]))
            movementDirection += Vector3.left;
        if (Input.GetKey(KeybindManager.Instance.KeyBinds["DOWN"]))
            movementDirection += Vector3.back;
        if (Input.GetKey(KeybindManager.Instance.KeyBinds["RIGHT"]))
            movementDirection += Vector3.right;

        Move(movementDirection);
    }

    IEnumerator CoRespawn(Vector3 respawnPoint)
    {
        IsStunned = true;
        transform.Translate(20 * Vector3.down);

        yield return new WaitForSeconds(respawnManager.respawnDelay);
        FindObjectOfType<AudioManager>().Play(SpawnSoundName);
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
