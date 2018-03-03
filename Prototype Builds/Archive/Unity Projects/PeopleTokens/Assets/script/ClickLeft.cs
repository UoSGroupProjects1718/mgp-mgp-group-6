using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickLeft : MonoBehaviour
{
    private Rigidbody2D rb;
    public Vector2 tapForce;

    // Use this for initialization
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);

            if (hit.collider != null)
            {
                if (hit.collider.gameObject == gameObject)
                {
                    rb.AddForce(tapForce, ForceMode2D.Impulse);
                }
            }
        }
    }
}
