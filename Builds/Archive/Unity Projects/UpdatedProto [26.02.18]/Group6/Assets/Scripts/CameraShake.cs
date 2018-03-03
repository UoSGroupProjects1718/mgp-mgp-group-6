using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    public float duration = 1.0f;
    public float lightPower = 0.5f;
    public float mediumPower = 1.2f;
    public float heavyPower = 3.0f;
    public float slowDownAmount = 5.0f;

    public bool shouldShake;

    public bool lightShake;
    public bool mediumShake;
    public bool heavyShake;

    private Transform camera;
    private Vector3 startPosition;
    private float initialDuration;

	// Use this for initialization
	void Start ()
    {
        camera = this.transform;
        startPosition = camera.localPosition;
        initialDuration = duration;
	}
	
	// Update is called once per frame
	void Update ()
    {
		if (shouldShake)
        {
            if (lightShake && duration > 0)
            {
                mediumShake = false;
                heavyShake = false;
                camera.localPosition = startPosition + Random.insideUnitSphere * lightPower;
                duration -= Time.deltaTime * slowDownAmount;
            }
            else if (mediumShake && duration > 0)
            {
                lightShake = false;
                heavyShake = false;
                camera.localPosition = startPosition + Random.insideUnitSphere * mediumPower;
                duration -= Time.deltaTime * slowDownAmount;
            }
            else if (heavyShake && duration > 0)
            {
                lightShake = false;
                mediumShake = false;
                camera.localPosition = startPosition + Random.insideUnitSphere * heavyPower;
                duration -= Time.deltaTime * slowDownAmount;
            }
            else
            {
                shouldShake = false;
                duration = initialDuration;
                camera.localPosition = startPosition;
            }
        }
	}
}
