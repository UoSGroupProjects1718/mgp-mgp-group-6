using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PunchPrefabDown : MonoBehaviour
{
    public GameObject Empty;
    public GameObject PS2;
    public GameObject punchPrefabDown;
    public Vector3 DownPosition;
    public GameObject Player2;
    public ParticleSystem P2effect;
    Rigidbody2D rb;
    public GameObject PLAYER2;
    public Rigidbody2D RB2D;

    PlayerController playerController;

	// Use this for initialization
	void Start ()
    {
        rb = Player2.GetComponent<Rigidbody2D>();
        playerController = Empty.GetComponent<PlayerController>();
        P2effect = PS2.GetComponent<ParticleSystem>();
        PLAYER2 = GameObject.FindGameObjectWithTag("PLAYER2");
        RB2D = PLAYER2.GetComponent<Rigidbody2D>();
    }
	
	// Update is called once per frame
	void Update ()
    {
        punchPrefabDown.transform.Translate(Vector3.up * -0.1f);
	}

    void OnTriggerEnter2D(Collider2D other)
    {
        Destroy(this.gameObject);
    }
}
