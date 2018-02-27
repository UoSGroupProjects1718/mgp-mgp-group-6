using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TugOnClick : MonoBehaviour
{
    public Slider powerMeter;
    private float sliderValue;
    public float sliderSpeedAdjust;
    public float moveDistanceAdjust;
    private float tugDistance;

    private GameObject background;
    private MeshRenderer bgRend;

    private int isPlayerTurn;
    private bool player1turn;
    private bool player2turn;

    private bool powerRising;
    private bool powerFalling;

	// Use this for initialization
	void Start ()
    {
        background = GameObject.FindGameObjectWithTag("Background");
        bgRend = background.GetComponent<MeshRenderer>();

        powerRising = true;

        isPlayerTurn = Random.Range(1, 2);

        if (isPlayerTurn == 1)
        {
            player1turn = true;
            player2turn = false;
        }
        else if (isPlayerTurn == 2)
        {
            player1turn = false;
            player2turn = true;
        }
}
	
	// Update is called once per frame
	void Update ()
    {
        PowerMeterBounce();

		if (player1turn)
        {
            bgRend.material.color = new Color(1.0f, 0.0f, 0.0f);

            if (Input.GetMouseButtonUp(0))
            {
                tugDistance = powerMeter.value * moveDistanceAdjust;
                transform.Translate(Vector3.left * tugDistance * Time.deltaTime);

                player1turn = false;
                player2turn = true;
            }
        }
        else if (player2turn)
        {
            bgRend.material.color = new Color(0.0f, 0.0f, 1.0f);

            if (Input.GetMouseButtonUp(0))
            {
                tugDistance = powerMeter.value * moveDistanceAdjust;
                transform.Translate(Vector3.right * tugDistance * Time.deltaTime);

                player2turn = false;
                player1turn = true;
            }
        }
	}

    void PowerMeterBounce()
    {
        if (powerRising)
        {
            sliderValue += sliderSpeedAdjust;
            powerMeter.value = sliderValue;

            if (powerMeter.value >= 1)
            {
                powerRising = false;
                powerFalling = true;
            }
        }
        if (powerFalling)
        {
            sliderValue -= sliderSpeedAdjust;
            powerMeter.value = sliderValue;

            if (powerMeter.value <= 0)
            {
                powerFalling = false;
                powerRising = true;
            }
        }
    }
}
