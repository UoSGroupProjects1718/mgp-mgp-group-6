using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketSpin : MonoBehaviour
{
    float timeCounter = 0;

    public float speed;
    public float width;
    public float height;


	// Use this for initialization
	void Start ()
    {

	}
	
	// Update is called once per frame
	void Update ()
    {
        timeCounter += Time.deltaTime * speed;

        float x = Mathf.Cos(timeCounter);
        float y = Mathf.Sin(timeCounter);
        float z = 0;

        transform.position = new Vector3(x, y, z);
	}
}
