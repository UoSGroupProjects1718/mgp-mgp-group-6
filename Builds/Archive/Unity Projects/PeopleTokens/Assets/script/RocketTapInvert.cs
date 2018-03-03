using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketTapInvert : MonoBehaviour
{
    public float speed;

    // Use this for initialization
    void Start()
    { }
	
	// Update is called once per frame
	void Update ()
    {
        transform.position += Vector3.right * speed;

        if (Input.GetMouseButtonUp(0))
        {
            Vector2 theScale = transform.localScale;
            theScale.x *= -1;
            transform.localScale = theScale;
            speed *= -1;
        }
    }
}
