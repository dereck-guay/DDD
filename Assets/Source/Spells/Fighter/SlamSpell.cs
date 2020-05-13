using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlamSpell : SpellMonoBehavior
{
    public float range;
    public float shockWaveRadius;
    public float damage;
    public float knockbackForce;
    public LayerMask hitLayers;
    public PlayerMonoBehaviour caster;

    public Vector3 landingPosition;

    readonly string audioName = "Fighter Slam";
    private bool hasLanded = false;
    private float initialY;

    #region PhysicsOverride
    // override UnityPhysics engine pour avoir plus de controle.
    private float gravity = 20f;
    private float jumpAngle = 60.0f;
    private float airTime = 0f;
    private float Vx;
    private float Vy;
    private float flightDuration;
    private float fighterVelocity;
    private float targetDistance;

    private void SetupJump()
    {
        // Set initial fighter height.
        initialY = transform.position.y;

        // Calculate distance to target.
        targetDistance = Vector3.Distance(transform.position, landingPosition);

        // Calculate the velocity needed to throw the object to the target at specified angle.
        fighterVelocity = targetDistance / (Mathf.Sin(2 * jumpAngle * Mathf.Deg2Rad) / gravity);

        // X and Y componenent of the velocity.
        Vx = Mathf.Sqrt(fighterVelocity) * Mathf.Cos(jumpAngle * Mathf.Deg2Rad);
        Vy = Mathf.Sqrt(fighterVelocity) * Mathf.Sin(jumpAngle * Mathf.Deg2Rad);

        // Calculate flight time.
        flightDuration = targetDistance / Vx;

        // Rotate projectile to face the target.
        transform.rotation = Quaternion.LookRotation(landingPosition - transform.position);
    }
    #endregion

    public void Cast()
    {
        Play(audioName);
        // Locks player movement.
        GetComponent<FighterComponent>().spellLocked = true;
        if (landingPosition.magnitude >= range)
            landingPosition = landingPosition.normalized * range;
        SetupJump();
    }

    private void Update()
    {
        if (currentLifeTime >= cooldown)
            Destroy(this);

        // Mouvement projectile vers la position
        if (!hasLanded)
            HandleJump();
            
        currentLifeTime += Time.deltaTime;
    }

    private void HandleJump()
    {
        // Calculate next frame position with direction.
        transform.Translate(0, (Vy - (gravity * airTime)) * Time.deltaTime, Vx * Time.deltaTime);

        airTime += Time.deltaTime;
        if (airTime > flightDuration)
        {
            hasLanded = true;
            ShockWave();
        }
    }

    private void ShockWave()
    {
        // Puts fighter to initial height.
        transform.position = new Vector3(transform.position.x, initialY, transform.position.z);

        // Creates a shockwave that deals damage and knocks back enemies.
        // Init sprite on the floor.


        // Get enemies in range. (Seperate colliders with layerMask)
        var entitiesColliderHit = Physics.OverlapSphere(transform.position, shockWaveRadius, hitLayers);

        // Apply force and damage.
        foreach(var entityCollider in entitiesColliderHit)
        {
            if (entityCollider.name != "Fighter")
            {
                var GOHit = entityCollider.gameObject;
                var target = GOHit.GetComponentInParent<EntityMonoBehaviour>();

                target.entityStats.HP.TakeDamage(damage, caster, target);
                GOHit.GetComponentInChildren<Rigidbody>().AddForce(
                    (GOHit.transform.position - transform.position).normalized * knockbackForce
                );
            }
        }

        // Disable spellLocked.
        GetComponent<FighterComponent>().spellLocked = false;
    }
}
