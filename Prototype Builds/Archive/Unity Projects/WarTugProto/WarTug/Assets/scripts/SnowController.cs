using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnowController : MonoBehaviour
{
    public float cloudSpeed = 1.6f;

    void Start()
    { }

    //Transform moves clouds to right.
    void FixedUpdate()
    {
        transform.Translate(Vector3.left * cloudSpeed * Time.deltaTime);

        if (Input.GetMouseButtonDown(0))
            Destroy(gameObject);

    }

    //When gameobject moves offscreen it is destroyed.
    void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
