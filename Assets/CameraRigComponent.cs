using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRigComponent : MonoBehaviour
{
    public Vector3 initPos;
    public GameObject go;
    Rigidbody body;
    float currentTime;
    public float forwardPower;
    public float leftPower;
    private void Awake()
    {
        initPos = transform.position;
    }
    private void Start()
    {
        body = GetComponent<Rigidbody>();
        currentTime = 1000;
    }
    // Update is called once per frame
    void Update()
    {
        transform.LookAt(go.transform);
        if (Input.GetKeyDown(KeyCode.K))
        {
            transform.position = initPos;
            currentTime = 0;
            body.velocity = Vector3.zero;
            body.AddForce(Vector3.back * forwardPower);
        }
        if (currentTime >= 2 && currentTime < 1000)
        {
            body.AddForce(Vector3.right * leftPower);
            currentTime = 1000;
        }
        currentTime += Time.deltaTime;
    }
}
