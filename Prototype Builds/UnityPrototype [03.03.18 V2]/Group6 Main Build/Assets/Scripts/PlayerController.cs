using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public GameObject Empty;
    GameManager _gameManager;
    [Header("Hierarchy Objects")]
    public GameObject Player1;
    public GameObject Player2;
    private Rigidbody2D Player1rb;
    public ParticleSystem Player1ps;
    private Rigidbody2D Player2rb;
    public ParticleSystem Player2ps;
    public GameObject punchPrefab;

    bool Player1Turn;


    void Start()
    {
        _gameManager = Empty.GetComponent<GameManager>();
        Player1rb = Player1.GetComponent<Rigidbody2D>();
        Player2rb = Player2.GetComponent<Rigidbody2D>();
    }

    void Update()
    {  }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (this.gameObject == Player1)
        {
            Debug.Log("PlayerController: Player1");
            Player1rb.AddForce(transform.up * -_gameManager.pushStrength, ForceMode2D.Impulse);
            Player1ps.Play();
            _gameManager.theGameTurn = GameManager.GameTurn.Player1Turn;
            GameManager._boolPlayer1Turn = true;
        }
        else
        if (this.gameObject == Player2)
        {
            Debug.Log("PLayerController: Player2");
            Player2rb.AddForce(transform.up * -_gameManager.pushStrength, ForceMode2D.Impulse);
            Player2ps.Play();
            _gameManager.theGameTurn = GameManager.GameTurn.Player2Turn;
            GameManager._boolPlayer1Turn = false;
        }
    }
}