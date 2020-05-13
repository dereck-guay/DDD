using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FighterSwordSwingComponent : MonoBehaviour
{
    float direction;
    GameObject parent;
    Rigidbody body;
    float spinIntensity = 5;
    float maxTime = 0.25f;
    float currentTime = 0;
    // Start is called before the first frame update
    void Start()
    {
        parent = gameObject.transform.parent.gameObject;

        direction = -270;
        transform.Rotate(new Vector3(0, direction * Time.deltaTime, 0), Space.World);

    }
    
    // Update is called once per frame
    void Update()
    {
        if (currentTime >= maxTime)
            Destroy(parent);
        transform.position = new Vector3(parent.transform.position.x, 0.5f , parent.transform.position.z);
        transform.Rotate(new Vector3(0, direction * Time.deltaTime, 0));
        currentTime += Time.deltaTime;
    }
}
