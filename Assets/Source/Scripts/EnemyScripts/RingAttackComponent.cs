using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class RingAttackComponent : MonoBehaviour
{
    public StraightProjectile projectilePrefab; //BossProjectileCollisionComponent
    public EntityMonoBehaviour caster;

    [SerializeField]
    float maxRingAngularSpeed = Mathf.PI;
    [SerializeField]
    float delayBetweenConsecutiveAttacks = 0.25f;
    [SerializeField]
    int nbOfConsecutiveAttacks = 5;
    [SerializeField]
    int nbOfProjectiles;
    [SerializeField]
    float ringDistance;

    Rigidbody rb;
    GameObject[] exitPoints;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        caster = GetComponentInParent<EntityMonoBehaviour>();

        ArrangeExitPoints();
    }

    public void Attack() => StartCoroutine("Launch");

    IEnumerator Launch()
    {
        UpdateRingRotation();
        caster.IsStunned = true;

        for (int i = 0; i < nbOfConsecutiveAttacks; ++i)
        {
            foreach (var e in exitPoints)
                Instantiate(projectilePrefab, e.transform.position, e.transform.rotation).damage = caster.entityStats.AtkDamage.Current;

            yield return new WaitForSeconds(delayBetweenConsecutiveAttacks);
        }

        caster.IsStunned = false;
    }

    void UpdateRingRotation() => rb.angularVelocity = new Vector3(0, Random.Range(-maxRingAngularSpeed, maxRingAngularSpeed), 0);

    void ArrangeExitPoints()
    {
        exitPoints = new GameObject[nbOfProjectiles];

        float theta;
        GameObject currentExit;
        GameObject exit = new GameObject("Exit");

        for (int i = 0; i < nbOfProjectiles; i++)
        {
            theta = i * 2 * Mathf.PI / nbOfProjectiles;

            currentExit = Instantiate(exit, transform);

            currentExit.transform.forward = RadiusVector(theta);
            currentExit.transform.localPosition = ringDistance * currentExit.transform.forward;

            exitPoints[i] = currentExit;
        }

        Destroy(exit);
    }

    Vector3 RadiusVector(float theta) => new Vector3(Mathf.Cos(theta), 0, Mathf.Sin(theta));
}
