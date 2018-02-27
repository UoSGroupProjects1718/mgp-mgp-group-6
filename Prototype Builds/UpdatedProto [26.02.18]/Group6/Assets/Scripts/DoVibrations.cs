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
    { }
    
    public void Vibrate10()
    {
        Vibration.Vibrate(10);
    }

    public void Vibrate100()
    {
        Vibration.Vibrate(100);
    }

    public void Vibrate1000()
    {
        Vibration.Vibrate(1000);
    }
}
