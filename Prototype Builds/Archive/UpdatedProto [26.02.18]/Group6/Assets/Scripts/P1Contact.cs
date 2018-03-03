using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class P1Contact : MonoBehaviour
{
    Rigidbody2D rb1;
    public GameObject empty;
    PlayerController playercontroller;
    public ParticleSystem P1hit;

    // Use this for initialization
    void Start()
    {
        rb1 = this.GetComponent<Rigidbody2D>();
        playercontroller = empty.GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter2D(Collider2D other)
    {
        rb1.AddForce(transform.up * playercontroller.P2PushDistance, ForceMode2D.Impulse);
        P1hit.Play();
    }
}