using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager GMInstance;

    public enum GameState { PreGame, MainGame }

    private bool player1Turn;
    private bool player2Turn;

    private bool pauseEum;
    private float clickResult;









    public GameObject Player1, Player2, background;
    private MeshRenderer bgRend;
    private Rigidbody2D p1rb, p2rb;

    public GameObject PunchDown;
    public GameObject PunchUp;

    public Text P1WinText, P2WinText;
    public Text PreGame, P1PreGame, P2PreGame, P1Score, P2Score, P1FirstMove, P2FirstMove;
    public float P1score, P2score;

    public Slider powerMeter;
    private float sliderValue;
    public float sliderSpeedAdjust;
    public float pushDistanceAdjust;
    private float pushDistance;
    private bool powerRising;
    private bool powerFalling;

    


    

    void Awake()
    {
        if (GMInstance == null)
            GMInstance = this;
        else if (GMInstance != null)
            Destroy(gameObject);

        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        background = GameObject.FindGameObjectWithTag("Background");
        bgRend = background.GetComponent<MeshRenderer>();
        p1rb = Player1.GetComponent<Rigidbody2D>();
        p2rb = Player2.GetComponent<Rigidbody2D>();

        powerRising = false;
        powerFalling = false;
    }

	

	void Update()
    {
        PowerMeterBounce();

        if (Input.GetMouseButtonDown(0))
        {
            clickResult = powerMeter.value;
        }

        //ChangeTurns();
        

        //if (Player1.transform.position.y >= 5.5)
        //{
        //    P2WinText.enabled = true;
        //}
        //if (Player2.transform.position.y <= -5.5)
        //{
        //    P1WinText.enabled = true;
        //}
    }

    void ChangeTurns()
    {
        StartCoroutine(ChangeTurnIE());
    }

    IEnumerator ChangeTurnIE() { return null; }

    //IEnumerator ChangeTurnIE()
    //{
    //    if (turn == Turns.PreGame)
    //    {
    //        bool player1Turn = false;
    //        bool player2Turn = false;

    //        if (!player1Turn && !player2Turn)
    //            player1Turn = !player1Turn;

    //        // Pregame state for both players.
    //        if (player1Turn)
    //        {
    //            // ... display your stuff here.
    //            // While its paused it won't do anything.
    //            while (pauseEum)
    //            {
    //                yield return null;
    //            }

    //            player1Turn = false;
    //            player2Turn = true;
    //        }

    //        if (player2Turn)
    //        {
    //            // .. display your stuff here.
    //            // While its paused it wont do anything.
    //            while (pauseEum) {
    //                yield return null;
    //            }

    //            player1Turn = false;
    //            player2Turn = false;

    //            CompareScore();
    //            StopCoroutine(ChangeTurnIE());
    //        }
    //    }

    //    else if (turn == Turns.Player1PreGame)
    //    {
    //        P1score = powerMeter.value;
    //        P1Score.enabled = true;
    //        P1Score.text = ("Player 1 Scored: " + P1score);
    //        yield return new WaitForSeconds(1.0f);
    //        turn = Turns.Player2PreGame;
    //        StopCoroutine(ChangeTurnIE());
    //    }

    //    else if (turn == Turns.Player2PreGame)
    //    {
    //        P2score = powerMeter.value;
    //        P2Score.enabled = true;
    //        P2Score.text = ("Player 1 Scored: " + P2score);
    //        yield return new WaitForSeconds(1.0f);
    //        StopCoroutine(ChangeTurnIE());
    //    }

    //    else if (turn == Turns.Pause)
    //    {
    //        yield return new WaitForSeconds(3.0f);
    //        StopCoroutine(ChangeTurnIE());
    //    }

    //    else if (turn == Turns.Player1Turn)
    //    {
    //        bgRend.material.color = new Color(1.0f, 0.0f, 0.0f);
    //        //P1punch = Instantiate(PunchDown, Player1.transform.position, Quaternion.identity);
    //        Instantiate(PunchDown, Player1.transform.position, Quaternion.identity);// as GameObject;

    //        if (PunchUp.transform.position == Player2.transform.position)
    //        {
    //            //P2hit.Play();
    //            pushDistance = powerMeter.value * pushDistanceAdjust;
    //            p2rb.AddForce(transform.up * -pushDistance, ForceMode2D.Impulse);
    //        }
    //    }
    //    else if (turn == Turns.Player2Turn)
    //    {
    //        yield return new WaitForSeconds(1.0f);
    //    }
    //}

    void CompareScore() {
        // Compare your score here.
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
}

