﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager GMInstance;

    [Header("Hierarchy Objects")]
    public GameObject Player1;
    public GameObject Player2;
    public GameObject background;
    private MeshRenderer bgRend;
    public GameObject punchPrefab;
    
    [Header("PowerMeter Variables")]
    public Slider powerMeter;
    private bool powerLevelRising;
    public float sliderValue;
    public float sliderSpeedAdjust;

    [Header("Player Tap Value")]
    public float player1Score;
    public float player2Score;

    [Header("Push Force")]
    public float pushStrength;
    public float pushStrengthAdjust;

    [Header("State Delay Countdowns")]
    public float preGameCountdown;
    public float compareScoreCountdown;
    public float winCountdown;

    public enum GameTurn { PreGame, DecideTurnPlayer1, DecideTurnPlayer2, CompareAttempts, Player1Turn, Player2Turn, Punching, Player1Win, Player2Win, GameRestart};
    [Header("Game State (enum)")]
    public GameTurn theGameTurn;

    public bool canTap;
    static public bool _boolPlayer1Turn;

    [Header("UI")]
    public Text player1TextBox;
    public Text player2TextBox;
    public Button restartButton;

    #region gameManager singleton
    void Awake()
    {
        //DontDestroyOnLoad(gameObject);

        if (GMInstance == null)
            GMInstance = this;
        else if (GMInstance != null)
            Destroy(gameObject);
    }
    #endregion
    

    void Start ()
    {
        restartButton = restartButton.GetComponent<Button>();
        canTap = true;
        theGameTurn = GameTurn.PreGame;
        bgRend = background.GetComponent<MeshRenderer>();
        restartButton.gameObject.SetActive(false);
        player1TextBox.enabled = true;
        player2TextBox.enabled = true;
    }
	
	void Update ()
    {
        PowerMeterBounce();

        TurnDecider();
    }

    void PowerMeterBounce()
    {
        if (powerLevelRising)
        {
            sliderValue += sliderSpeedAdjust;
            powerMeter.value = sliderValue;

            if (powerMeter.value >= 0.99)
                powerLevelRising = false;
        }
        if (!powerLevelRising)
        {
            sliderValue -= sliderSpeedAdjust;
            powerMeter.value = sliderValue;

            if (powerMeter.value <= 0.01)
                powerLevelRising = true;
        }
        if (sliderValue < 0.01)
            sliderValue = 0.011f;
    }

    #region Game Turn Switch
    void TurnDecider()
    {
        switch (theGameTurn)
        {
            case GameTurn.PreGame:
                //Debug.Log("GameTurn: PreGame");
                player1TextBox.text = "Best score goes first...";
                player2TextBox.text = "Best score goes first...";
                preGameCountdown -= Time.deltaTime;
                if (preGameCountdown < 0)
                    theGameTurn = GameTurn.DecideTurnPlayer1;
                break;

            case GameTurn.DecideTurnPlayer1:
                //Debug.Log("GameTurn: Player1 Decider Attempt");
                player2TextBox.enabled = false;
                player1TextBox.text = "Player 1 test your timing!";
                bgRend.material.color = new Color(1.0f, 0.0f, 0.0f);
                if (Input.GetMouseButtonUp(0))
                {
                    //Debug.Log("p1attemptclick");
                    player1Score = powerMeter.value;
                    theGameTurn = GameTurn.DecideTurnPlayer2;
                }
                break;

            case GameTurn.DecideTurnPlayer2:
                //Debug.Log("GameTurn: Player2 Decider Attempt");
                player1TextBox.enabled = false;
                player2TextBox.text = "Player 2 test your timing!";
                player2TextBox.enabled = true;
                bgRend.material.color = new Color(0.0f, 0.0f, 1.0f);
                if (Input.GetMouseButtonUp(0))
                {
                    //Debug.Log("p2attemptclick");
                    player2Score = powerMeter.value;
                    theGameTurn = GameTurn.CompareAttempts;
                }
                break;

            case GameTurn.CompareAttempts:
                //Debug.Log("GameTurn: Compare Attempts");
                if (player1Score >= player2Score)
                {
                    bgRend.material.color = new Color(1.0f, 0.0f, 0.0f);
                    player1TextBox.text = "Player 1 wins first turn!";
                    player2TextBox.text = "Player 1 wins first turn!";
                    player1TextBox.enabled = true;
                    player2TextBox.enabled = true;
                }
                else if (player2Score > player1Score)
                {
                    bgRend.material.color = new Color(0.0f, 0.0f, 1.0f);
                    player1TextBox.text = "Player 2 wins first turn!";
                    player2TextBox.text = "Player 2 wins first turn!";
                    player1TextBox.enabled = true;
                    player2TextBox.enabled = true;
                }
                compareScoreCountdown -= Time.deltaTime;
                if (compareScoreCountdown <= 0)
                {
                    if (player1Score >= player2Score)
                    {
                        player1TextBox.enabled = false;
                        player2TextBox.enabled = false;
                        _boolPlayer1Turn = true;
                        theGameTurn = GameTurn.Player1Turn;
                    }
                    else if (player2Score > player1Score)
                    {
                        player1TextBox.enabled = false;
                        player2TextBox.enabled = false;
                        _boolPlayer1Turn = false;
                        theGameTurn = GameTurn.Player2Turn;
                    }
                }
                break;

            case GameTurn.Player1Turn:
                canTap = true;
                //Debug.Log("GameTurn: Player1 Turn");
                bgRend.material.color = new Color(1.0f, 0.0f, 0.0f);
                if (Input.GetMouseButtonUp(0) && canTap)
                {
                    Instantiate(punchPrefab, Player1.transform.position + new Vector3(0, -1, 0), Quaternion.identity);
                    pushStrength = powerMeter.value * pushStrengthAdjust;
                    theGameTurn = GameTurn.Punching;
                }
                if (Player1.transform.position.y >= 5.5)
                    theGameTurn = GameTurn.Player2Win;
                if (Player2.transform.position.y <= -5.5)
                    theGameTurn = GameTurn.Player1Win;
                break;

            case GameTurn.Player2Turn:
                canTap = true;
                //Debug.Log("GameTurn: Player2 Turn");
                bgRend.material.color = new Color(0.0f, 0.0f, 1.0f);
                if (Input.GetMouseButtonUp(0) && canTap)
                {
                    Instantiate(punchPrefab, Player2.transform.position + new Vector3(0, 1, 0), Quaternion.identity);
                    pushStrength = powerMeter.value * pushStrengthAdjust;
                    theGameTurn = GameTurn.Punching;
                }
                if (Player1.transform.position.y >= 5.5)
                    theGameTurn = GameTurn.Player2Win;
                if (Player2.transform.position.y <= -5.5)
                    theGameTurn = GameTurn.Player1Win;
                break;

            case GameTurn.Punching:
                canTap = false;
                break;

            case GameTurn.Player1Win:
                bgRend.material.color = new Color(1.0f, 0.0f, 0.0f);
                player1TextBox.text = "PLAYER 1 WINS";
                player2TextBox.text = "PLAYER 1 WINS";
                player1TextBox.enabled = true;
                player2TextBox.enabled = true;
                winCountdown -= Time.deltaTime;
                if (winCountdown < 0)
                    restartButton.gameObject.SetActive(true);
                break;

            case GameTurn.Player2Win:
                bgRend.material.color = new Color(0.0f, 0.0f, 1.0f);
                player1TextBox.text = "PLAYER 2 WINS";
                player2TextBox.text = "PLAYER 2 WINS";
                player1TextBox.enabled = true;
                player2TextBox.enabled = true;
                winCountdown -= Time.deltaTime;
                if (winCountdown < 0)
                    restartButton.gameObject.SetActive(true);
                break;

            case GameTurn.GameRestart:

                break;
        }
    }
    #endregion

    public void ButtonRestart()
    {
        SceneManager.LoadScene(1);
    }
}
