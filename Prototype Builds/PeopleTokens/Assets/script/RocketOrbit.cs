using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketOrbit : MonoBehaviour
{
    public Transform objectToOrbit; //Object To Orbit
    public Vector3 orbitAxis = Vector3.up; //Which vector to use for Orbit
    public float orbitRadius = 75.0f; //Orbit Radius
    public  float orbitRadiusCorrectionSpeed = 0.5f; //How quickly the object moves to new position
    public float orbitRoationSpeed = 10.0f; //Speed Of Rotation arround object
    public float orbitAlignToDirectionSpeed = 0.5f; //Realign speed to direction of travel
 
    private Vector3 orbitDesiredPosition;
    private Vector3 previousPosition;
    private Vector3 relativePos;
    private Quaternion rotation;
    private Transform thisTransform;
 
 //---------------------------------------------------------------------------------------------------------------------
 
    void Start()
    {
        thisTransform = transform;
    }

    //---------------------------------------------------------------------------------------------------------------------

    void Update()
    {
        //Movement
        thisTransform.RotateAround(objectToOrbit.position, orbitAxis, orbitRoationSpeed * Time.deltaTime);
        orbitDesiredPosition = (thisTransform.position - objectToOrbit.position).normalized * orbitRadius + objectToOrbit.position;
        thisTransform.position = Vector3.Slerp(thisTransform.position, orbitDesiredPosition, Time.deltaTime * orbitRadiusCorrectionSpeed);

        //Rotation
        relativePos = thisTransform.position - previousPosition;
        rotation = Quaternion.LookRotation(relativePos);
        thisTransform.rotation = Quaternion.Slerp(thisTransform.rotation, rotation, orbitAlignToDirectionSpeed * Time.deltaTime);
        previousPosition = thisTransform.position;
    }

}
