using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PunchPrefabUp : MonoBehaviour
{
    public GameObject punchPrefabUp;

	// Use this for initialization
	void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        punchPrefabUp.transform.Translate(Vector3.up * 0.1f);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        Destroy(this.gameObject);
    }

}

