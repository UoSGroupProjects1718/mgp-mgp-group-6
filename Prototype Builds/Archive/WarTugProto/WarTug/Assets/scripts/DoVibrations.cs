using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoVibrations : MonoBehaviour
{

	// Use this for initialization
	void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
		if (Input.GetMouseButtonDown(0))
        {
            Vibration.Vibrate(100);
            System.Console.WriteLine("IT IS VIBRATING");
        }
	}
}
