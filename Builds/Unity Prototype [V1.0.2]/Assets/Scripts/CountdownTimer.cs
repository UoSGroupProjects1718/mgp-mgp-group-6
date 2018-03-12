using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CountdownTimer : MonoBehaviour
{
    [Header("Game Manager")]
    public GameObject Empty;
    GameManager _gameManager;

    [Header("Coutdown UI")]
    private float tempTime = 0;
    public int timeLeft = 10;
    public int timeLeftReset;
    public float countdownFillDiv1 = 10;
    private float countdown = 1f;
    [Header("Coutdown UI P1")]
    public Text timerTextP1;
    public Image countdownFillP1;
    public Image countdownFillBackgroundP1;
    [Header("Coutdown UI P2")]
    public Text timerTextP2;
    public Image countdownFillP2;
    public Image countdownFillBackgroundP2;

    void Start ()
    {
        _gameManager = Empty.GetComponent<GameManager>();
        timerTextP1.enabled = false;
        countdownFillP1.enabled = false;
        countdownFillBackgroundP1.enabled = false;
        timerTextP2.enabled = false;
        countdownFillP2.enabled = false;
        countdownFillBackgroundP2.enabled = false;
    }
	
	void Update ()
    {
        if (_gameManager.theGameTurn == GameManager.GameTurn.Player1Turn)
        {
            tempTime += Time.deltaTime;
            if (tempTime > 1.0f)
            {
                timeLeft--;
                tempTime = 0;
            }

            timerTextP1.enabled = true;
            countdownFillP1.enabled = true;
            countdownFillBackgroundP1.enabled = true;
            timerTextP2.enabled = false;
            countdownFillP2.enabled = false;
            countdownFillBackgroundP2.enabled = false;

            countdown -= 1f / countdownFillDiv1 * Time.deltaTime;
            countdownFillP1.fillAmount = countdown;

            timerTextP1.text = ("" + timeLeft);

            if (countdown <= 0)
            {
                timerTextP1.enabled = false;
                countdownFillP1.enabled = false;
                countdownFillBackgroundP1.enabled = false;
                _gameManager.theGameTurn = GameManager.GameTurn.Player2Turn;
                GameManager._boolPlayer1Turn = false;
                timerTextP1.text = "0";
                countdown = 1f;
                timeLeft = timeLeftReset;
            }

            if (Input.GetMouseButtonUp(0))
            {
                timerTextP1.enabled = false;
                countdownFillP1.enabled = false;
                countdownFillBackgroundP1.enabled = false;
                timerTextP1.text = "0";
                countdown = 1f;
                timeLeft = timeLeftReset;
            }
        }

        ///////////////////////////////////////////////////////////////////////////////////////////////////////
        ///////////////////////////////////////////////////////////////////////////////////////////////////////
        ///////////////////////////////////////////////////////////////////////////////////////////////////////


        if (_gameManager.theGameTurn == GameManager.GameTurn.Player2Turn)
        {
            tempTime += Time.deltaTime;
            if (tempTime > 1.0f)
            {
                timeLeft--;
                tempTime = 0;
            }

            timerTextP2.enabled = true;
            countdownFillP2.enabled = true;
            countdownFillBackgroundP2.enabled = true;
            timerTextP1.enabled = false;
            countdownFillP1.enabled = false;
            countdownFillBackgroundP1.enabled = false;

            countdown -= 1f / countdownFillDiv1 * Time.deltaTime;
            countdownFillP2.fillAmount = countdown;

            timerTextP2.text = ("" + timeLeft);

            if (countdown <= 0)
            {
                timerTextP2.enabled = false;
                countdownFillP2.enabled = false;
                countdownFillBackgroundP2.enabled = false;
                _gameManager.theGameTurn = GameManager.GameTurn.Player1Turn;
                GameManager._boolPlayer1Turn = true;
                timerTextP2.text = "0";
                countdown = 1f;
                timeLeft = timeLeftReset;
            }

            if (Input.GetMouseButtonUp(0))
            {
                timerTextP2.enabled = false;
                countdownFillP2.enabled = false;
                countdownFillBackgroundP2.enabled = false;
                timerTextP2.text = "0";
                countdown = 1f;
                timeLeft = timeLeftReset;
            }
        }
    }
}