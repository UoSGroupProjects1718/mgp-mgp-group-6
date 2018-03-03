using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortraitCamAutoRatio : MonoBehaviour
{

	// Use this for initialization
	void Start ()
    {
        float X = Screen.width;
        float Y = Screen.height;

        float aspectRatio = Mathf.RoundToInt((X / Y) * 100f) / 100f;

        if (aspectRatio == 0.75f)
            aspectRatio = 0.65f;

        gameObject.GetComponent<Camera>().aspect = aspectRatio;
	}
}
