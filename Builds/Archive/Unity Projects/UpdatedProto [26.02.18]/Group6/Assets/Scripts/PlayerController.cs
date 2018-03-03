using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public GameObject Player1;
    public GameObject Player2;
    public GameObject PunchDown;
    public GameObject PunchUp;
    private GameObject P1punch;
    private GameObject P2punch;
    public ParticleSystem P1hit;
    public ParticleSystem P2hit;

    public Slider powerMeter;
    private float sliderValue;
    public float sliderSpeedAdjust;
    public float pushDistanceAdjust;
    private float pushDistance;

    private GameObject background;
    private MeshRenderer bgRend;

    private Rigidbody2D p1rb;
    private Rigidbody2D p2rb;

    private int isPlayerTurn;
    private bool player1turn;
    private bool player2turn;
    private bool player1canGo = false;
    private bool player2canGo = false;
    private bool preGame = false;
    private bool mainGame = false;

    private bool powerRising;
    private bool powerFalling;
    private bool p1Click = false;
    private bool p2Click = false;

    public Text p1winText, p2winText;
    public Text whoFirst, p1try, p2try, p1GosFirst, p2GosFirst, p1score, p2score;
    public float P1score, P2score;

    private bool p1Attempt, p2Attempt;
    private bool wait, wait2;

    public float P1PushDistance;
    public float P2PushDistance;

    // Use this for initialization
    void Start()
    {
        preGame = true;
        mainGame = false;


        background = GameObject.FindGameObjectWithTag("Background");
        bgRend = background.GetComponent<MeshRenderer>();
        p1rb = Player1.GetComponent<Rigidbody2D>();
        p2rb = Player2.GetComponent<Rigidbody2D>();

        p1winText.text = "Player 1 Wins!";
        p2winText.text = "Player 2 Wins!";
        p1winText.enabled = false;
        p2winText.enabled = false;
        p1try.enabled = false;
        p2try.enabled = false;
        p1GosFirst.enabled = false;
        p2GosFirst.enabled = false;
        p1score.text = "";
        p1score.enabled = false;
        p2score.text = "";
        p2score.enabled = false;
        whoFirst.enabled = true;
        p1Attempt = false;
        p2Attempt = false;
        wait = true;
        wait2 = false;

        powerRising = true;

        

        //isPlayerTurn = Random.Range(1, 2);

        //if (isPlayerTurn == 1)
        //{
        //    player1turn = true;
        //    player2turn = false;
        //}
        //else if (isPlayerTurn == 2)
        //{
        //    player1turn = false;
        //    player2turn = true;
        //}
    }

    // Update is called once per frame
    void Update()
    {
        PowerMeterBounce();

        if (wait)
            StartCoroutine(Wait(3.0f));


        if (preGame && p1Attempt)
        {
            Debug.Log("p1attempt");
            whoFirst.enabled = false;
            p1try.enabled = true;
            p1Attempt = false;

            if (Input.GetMouseButtonDown(0))
            {
                Debug.Log("p1attemptclick");
                P1score = powerMeter.value;
                p1score.enabled = true;
                p1score.text = ("Player 1 scored: " + P1score);
                p2Attempt = true;
                p2try.enabled = true;
                p2Attempt = true;
                p1try.enabled = false;
                p1Attempt = false;
            }
        }
        else if (preGame && p2Attempt)
        {
            Debug.Log("p2attempt");
            // p2Attempt = false;

            if (Input.GetMouseButtonDown(0))
            {
                Debug.Log("p2attemptclick");
                P2score = powerMeter.value;
                p2score.enabled = true;
                p2score.text = ("Player 2 scored: " + P2score);
                p1try.enabled = false;
                p2Attempt = true;
                p2try.enabled = true;

                preGame = false;
                mainGame = true;

                CompareAttempts();
            }
            StartCoroutine(Wait2(3.0f));
        }


        

        if (mainGame && player1turn)
        {
            Debug.Log("Player 1 Turn");
            bgRend.material.color = new Color(1.0f, 0.0f, 0.0f);

            if (Input.GetMouseButtonUp(0))
            {
                Instantiate(PunchDown, Player1.transform.position + new Vector3(0,-1,0), Quaternion.identity);
                //float punchY = thrownPunchDown.transform.position.y;
                //float distanceResult = punchY - Player2.transform.position.y;
               // Debug.Log(distanceResult);
               // {
                    //P2hit.Play();
                    pushDistance = powerMeter.value * pushDistanceAdjust;
                P1PushDistance = pushDistance;
                    //Vector2 P2pushForce = transform.up * -pushDistance;

                    player1turn = false;
                    player2turn = true;
               // }

                //player1canGo = false;

                //float ddistance = Vector3.Distance(PunchDown.transform.position, Player2.transform.position);
                //Debug.Log("1" + ddistance);
                ////if (PunchDown.transform.position == Player2.transform.position)
                //{
                //    P2hit.Play();
                //    pushDistance = powerMeter.value * pushDistanceAdjust;
                //    p2rb.AddForce(transform.up * -pushDistance, ForceMode2D.Impulse);

                //    player1turn = false;
                //    player2turn = true;
                //}
            }
        }
        else if (mainGame && player2turn)
        {
            Debug.Log("Player 2 Turn");

            bgRend.material.color = new Color(0.0f, 0.0f, 1.0f);

            if (Input.GetMouseButtonUp(0))
            {
                Instantiate(PunchUp, Player2.transform.position + new Vector3(0,1,0), Quaternion.identity);

                //float ddistance = Vector3.Distance(PunchUp.transform.position, Player2.transform.position);
               // Debug.Log("2" + ddistance);
                //{
                pushDistance = powerMeter.value * pushDistanceAdjust;
                P2PushDistance = pushDistance;
                    //p1rb.AddForce(transform.up * pushDistance, ForceMode2D.Impulse);
               // }

                player2turn = false;
                player1turn = true;
            }
        }

        if (Player1.transform.position.y >= 5.5)
        {
            p2winText.enabled = true;
        }
        if (Player2.transform.position.y <= -5.5)
        {
            p1winText.enabled = true;
        }
    }

    public void PowerMeterBounce()
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

    private IEnumerator Wait(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        wait = false;
        whoFirst.enabled = false;
        p1try.enabled = true;
        p1Attempt = true;
        StopCoroutine(Wait(waitTime));
    }

    private IEnumerator Wait2(float waitTime)
    {
        bool loop;
        loop = true;

        while (loop)
        {
            yield return new WaitForSeconds(waitTime);
            //StopCoroutine(Wait2(waitTime));
            loop = false;
        }
        StopCoroutine(Wait2(waitTime));
    }

    void Player2Attempt()
    {
        if (p2Attempt)
        {
            if (Input.GetMouseButtonDown(0))
            {
                P2score = powerMeter.value;
                p2score.enabled = true;
                p2score.text = ("Player 2 scored: " + P2score);
                p2Attempt = false;
                p2try.enabled = false;
                CompareAttempts();
            }
        }
    }

    void CompareAttempts()
    {
        Debug.Log("comparing");
        if (P1score > P2score)
        {
            whoFirst.enabled = false;
            p1try.enabled = false;
            p2try.enabled = false;
            p1score.enabled = false;
            p2score.enabled = false;
            player1turn = true;
            player2turn = false;
            preGame = false;
            mainGame = true;
        }
        if (P1score < P2score)
        {
            whoFirst.enabled = false;
            p1try.enabled = false;
            p2try.enabled = false;
            p1score.enabled = false;
            p2score.enabled = false;
            player2turn = true;
            player1turn = false;
            preGame = false;
            mainGame = true;
        }
    }
}
