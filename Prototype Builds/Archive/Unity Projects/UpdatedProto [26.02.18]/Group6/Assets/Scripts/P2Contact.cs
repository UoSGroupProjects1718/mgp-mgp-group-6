using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class P2Contact : MonoBehaviour
{
    Rigidbody2D rb2;
    public GameObject empty;
    PlayerController playercontroller;
    public ParticleSystem P2hit;

    // Use this for initialization
    void Start ()
    {
        rb2 = this.GetComponent<Rigidbody2D>();
        playercontroller = empty.GetComponent<PlayerController>();
    }
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    void OnTriggerEnter2D(Collider2D other)
    {
        rb2.AddForce(transform.up * -playercontroller.P2PushDistance, ForceMode2D.Impulse);
        P2hit.Play();
    }
}
