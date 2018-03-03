using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PushController : MonoBehaviour
{
    private GameManager _gameManager;

    [Header("Hierarchy Objects")]
    public GameObject Player1;
    public GameObject Player2;
    public GameObject punchPrefab;
    public GameObject Empty;
    private Rigidbody2D Player1rb;
    private Rigidbody2D Player2rb;


    void Start ()
    {
        _gameManager = Empty.GetComponent<GameManager>();
        Player1rb = Player1.GetComponent<Rigidbody2D>();
        Player2rb = Player2.GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        Debug.Log("punchControl: update");
        if (GameManager._boolPlayer1Turn == true)
        {
            Debug.Log("punchControl: transform punch down");
            this.transform.Translate(-Vector3.up * 0.1f);
        }
        if (GameManager._boolPlayer1Turn == false)
        {
            Debug.Log("punchControl: transform punch up");
            this.transform.Translate(Vector3.up * 0.1f);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        Destroy(this.gameObject);
    }
}
