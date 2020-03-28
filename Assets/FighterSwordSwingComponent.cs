using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FighterSwordSwingComponent : MonoBehaviour
{
    float direction;
    GameObject parent;
    // Start is called before the first frame update
    void Start()
    {
        parent = gameObject.transform.parent.gameObject;
        direction = -90;
        transform.Rotate(new Vector3(0, direction * 0.005f, 0));
    }
    
    // Update is called once per frame
    void Update()
    {
        if (transform.localEulerAngles.y < 270)
            Destroy(parent);
        transform.Rotate(new Vector3(0, direction * Time.deltaTime * 5, 0));
    }
}
