using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MiniAbsorbScale : MonoBehaviour
{
    public Slider powerMeter;
    private float sliderValue;
    public float sliderSpeedAdjust;
    public float growAmountAdjust;
    private float growAmount;

    private GameObject background;
    private MeshRenderer bgRend;

    private int isPlayerTurn;
    private bool player1turn;
    private bool player2turn;

    private bool powerRising;
    private bool powerFalling;

    public ParticleSystem PS_P1;
    public ParticleSystem PS_P2;
    public ParticleSystem PS_End;

    public Image ball_p1;
    public Image ball_p2;

    // Use this for initialization
    void Start()
    {
        PS_P1.Stop();
        PS_P2.Stop();
        PS_End.Stop();

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
    void Update()
    {
        PowerMeterBounce();

        if (player1turn)
        {
            bgRend.material.color = new Color(1.0f, 0.0f, 0.0f);

            if (Input.GetMouseButtonUp(0))
            {
                //ShrinkBall();
                growAmount = powerMeter.value * growAmountAdjust;
                ball_p1.gameObject.GetComponent<RectTransform>().localScale += new Vector3(growAmount, growAmount, 0);

                PS_P1.Play();

                player1turn = false;
                player2turn = true;
            }
        }
        else if (player2turn)
        {
            bgRend.material.color = new Color(0.0f, 0.0f, 1.0f);

            if (Input.GetMouseButtonUp(0))
            {
               // ShrinkBall();
                PS_P2.Play();

                growAmount = powerMeter.value * growAmountAdjust;
                ball_p2.gameObject.GetComponent<RectTransform>().localScale += new Vector3(growAmount, growAmount, 0);

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

    //void ShrinkBall()
    //{
    //    if (transform.localScale.x >= 0)
    //    {
    //        growAmount = powerMeter.value * growAmountAdjust;
    //        transform.localScale += new Vector3(growAmount, growAmount, 0);

    //        if (transform.localScale.x <= 0)
    //        {
    //            PS_End.Play();
    //            transform.localScale = new Vector3(0, 0, 0);
    //            Destroy(PS_P1);
    //            Destroy(PS_P2);
    //        }
    //    }
    //}

    void ScaleP1()
    {

    }

    void ScaleP2()
    {

    }
}
