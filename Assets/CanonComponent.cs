using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanonComponent : MonoBehaviour
{
    public GameObject projectileToShoot;
    public KeyCode shootKey;
    public Transform projectileExit;

    private Vector3 previousPosition;

    void Update()
    {
        if (Input.GetKeyUp(shootKey))
            Shoot();
    }

    private void Shoot()
    {
        Instantiate(projectileToShoot, projectileExit.position, projectileExit.rotation).GetComponent<Rigidbody>().AddForce(transform.forward * 5); ;
    }
}
