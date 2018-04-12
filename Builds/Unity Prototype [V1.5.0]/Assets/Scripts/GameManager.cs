using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager GMInstance;
    PowerUp _powerUp;
    AudioManager _audioManager;
    private bool audioGameWon;

    [Header("Hierarchy Objects")]
    public GameObject Empty;
    public GameObject Player1;
    private Rigidbody2D Player1rb;
    public GameObject Player2;
    private Rigidbody2D Player2rb;
    public GameObject background;
    private MeshRenderer bgRend;
    public GameObject punchPrefab;
    
    [Header("PowerMeter Variables")]
    public Slider powerMeter;
    private bool powerLevelRising;
    public float sliderValue;
    public float sliderSpeedAdjust;
    private float player1yPos, player2yPos;

    [Header("Player Tap Value")]
    public float player1Score;
    public float player2Score;

    [Header("Push Force")]
    public float pushStrength;
    public float pushStrengthAdjust;
    public float UPDATEDpushStrengthAdjust;

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
    public Text player1DeciderResultText;
    public Text player2DeciderResultText;
    public Text P1sliderResultText;
    public Text P2sliderResultText;
    public Text P1TurnText;
    public Text P2TurnText;
    public Button restartButton;
    public Button homeButton;

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
        _powerUp = Empty.GetComponent<PowerUp>();
        _audioManager = Empty.GetComponent<AudioManager>();
        Player1rb = Player1.GetComponent<Rigidbody2D>();
        Player2rb = Player2.GetComponent<Rigidbody2D>();
        restartButton = restartButton.GetComponent<Button>();
        homeButton = homeButton.GetComponent<Button>();
        canTap = true;
        theGameTurn = GameTurn.PreGame;
        bgRend = background.GetComponent<MeshRenderer>();
        restartButton.gameObject.SetActive(false);
        homeButton.gameObject.SetActive(false);
        player1TextBox.enabled = true;
        player2TextBox.enabled = true;
        P1sliderResultText.enabled = false;
        P2sliderResultText.enabled = false;
        player1DeciderResultText.enabled = false;
        player2DeciderResultText.enabled = false;
        P1TurnText.enabled = false;
        P2TurnText.enabled = false;
    }
	
	void Update ()
    {
        PlayerPositionYaxis();
        PowerMeterBounce();

        TurnDecider();
    }

    void PowerMeterBounce()
    {
        if (_boolPlayer1Turn == true)
        {
            sliderSpeedAdjust = player2yPos;
        }
        if (_boolPlayer1Turn == false)
        {
            sliderSpeedAdjust = player1yPos;
        }

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
        if (sliderValue <= 0.01)
            sliderValue = 0.011f;
    }

    #region Game Turn Switch
    void TurnDecider()
    {
        switch (theGameTurn)
        {
            case GameTurn.PreGame:
                //Debug.Log("GameTurn: PreGame");
                player1TextBox.text = "Best score goes first. . .";
                player2TextBox.text = "Best score goes first. . .";
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
                    SliderScoreThreshold();
                    //player1Score = powerMeter.value;
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
                    SliderScoreThreshold();
                    //player2Score = powerMeter.value;
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
                if (player2Score > player1Score)
                {
                    bgRend.material.color = new Color(0.0f, 0.0f, 1.0f);
                    player1TextBox.text = "Player 2 wins first turn!";
                    player2TextBox.text = "Player 2 wins first turn!";
                    player1TextBox.enabled = true;
                    player2TextBox.enabled = true;
                }
                else if (player2Score == player1Score)
                {
                    int _1score, _2score;
                    _1score = Random.Range(0, 10);
                    _2score = Random.Range(0, 9);
                    if (_1score > _2score)
                    {
                        bgRend.material.color = new Color(1.0f, 0.0f, 0.0f);
                        player1TextBox.text = "Player 1 wins first turn!";
                        player2TextBox.text = "Player 1 wins first turn!";
                        player1TextBox.enabled = true;
                        player2TextBox.enabled = true;
                    }
                    else
                    {
                        bgRend.material.color = new Color(0.0f, 0.0f, 1.0f);
                        player1TextBox.text = "Player 2 wins first turn!";
                        player2TextBox.text = "Player 2 wins first turn!";
                        player1TextBox.enabled = true;
                        player2TextBox.enabled = true;
                    }
                }
                compareScoreCountdown -= Time.deltaTime;
                if (compareScoreCountdown <= 0)
                {
                    player1DeciderResultText.enabled = false;
                    player2DeciderResultText.enabled = false;

                    if (player1Score >= player2Score)
                    {
                        player1TextBox.enabled = false;
                        player2TextBox.enabled = false;
                        _boolPlayer1Turn = true;
                        theGameTurn = GameTurn.Player2Turn;
                    }
                    if (player2Score > player1Score)
                    {
                        player1TextBox.enabled = false;
                        player2TextBox.enabled = false;
                        _boolPlayer1Turn = false;
                        theGameTurn = GameTurn.Player1Turn;
                    }
                }
                break;

            case GameTurn.Player1Turn:
                P2sliderResultText.enabled = false;
                P1TurnText.enabled = true;
                canTap = true;
                //Debug.Log("GameTurn: Player1 Turn");
                bgRend.material.color = new Color(1.0f, 0.0f, 0.0f);
                if (Input.GetMouseButtonUp(0) && canTap)
                {
                    P1TurnText.enabled = false;

                    //pushStrength = powerMeter.value * pushStrengthAdjust;
                    SliderScoreThreshold();

                    if (_powerUp.isRecover == false)
                    {
                        Instantiate(punchPrefab, Player1.transform.position + new Vector3(0, -1, 0), Quaternion.identity);

                        if (_powerUp.isPowerHit)
                        {
                            pushStrength = pushStrength * 1.5f;
                            _powerUp.isPowerHit = false;
                        }
                        theGameTurn = GameTurn.Punching;
                    }

                    if (_powerUp.isRecover == true)
                    {
                        Player1rb.AddForce(transform.up * -pushStrength * 1.75f, ForceMode2D.Impulse);
                        _powerUp.isRecover = false;
                        _boolPlayer1Turn = false;
                        theGameTurn = GameTurn.Player2Turn;
                        _powerUp.DecidePowerUp();
                    }
                }
                if (Player1.transform.position.y >= 5.5)
                {
                    theGameTurn = GameTurn.Player2Win;
                    //audioGameWon = true;
                    _audioManager.PlayAudio(9);
                }
                if (Player2.transform.position.y <= -5.5)
                {
                    theGameTurn = GameTurn.Player1Win;
                    //audioGameWon = true;
                    _audioManager.PlayAudio(9);
                }
                break;

            case GameTurn.Player2Turn:
                P1sliderResultText.enabled = false;
                P2TurnText.enabled = true;
                canTap = true;
                //Debug.Log("GameTurn: Player2 Turn");
                bgRend.material.color = new Color(0.0f, 0.0f, 1.0f);
                if (Input.GetMouseButtonUp(0) && canTap)
                {
                    P2TurnText.enabled = false;

                    //pushStrength = powerMeter.value * pushStrengthAdjust;
                    SliderScoreThreshold();

                    if (_powerUp.isRecover == false)
                    {
                        Instantiate(punchPrefab, Player2.transform.position + new Vector3(0, 1, 0), Quaternion.identity);
                        if (_powerUp.isPowerHit)
                        {
                            pushStrength = pushStrength * 1.5f;
                        }
                        theGameTurn = GameTurn.Punching;
                    }

                    if (_powerUp.isRecover == true)
                    {
                        Player2rb.AddForce(transform.up * pushStrength * 1.75f, ForceMode2D.Impulse);
                        _powerUp.isRecover = false;
                        _boolPlayer1Turn = true;
                        theGameTurn = GameTurn.Player1Turn;
                        _powerUp.DecidePowerUp();
                    }
                }
                if (Player1.transform.position.y >= 5.5)
                {
                    theGameTurn = GameTurn.Player2Win;
                    //audioGameWon = true;
                    _audioManager.PlayAudio(9);
                }
                if (Player2.transform.position.y <= -5.5)
                {
                    theGameTurn = GameTurn.Player1Win;
                    //audioGameWon = true;
                    _audioManager.PlayAudio(9);
                }
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
                {
                    restartButton.gameObject.SetActive(true);
                    homeButton.gameObject.SetActive(true);
                }
                break;

            case GameTurn.Player2Win:
                bgRend.material.color = new Color(0.0f, 0.0f, 1.0f);
                player1TextBox.text = "PLAYER 2 WINS";
                player2TextBox.text = "PLAYER 2 WINS";
                player1TextBox.enabled = true;
                player2TextBox.enabled = true;
                winCountdown -= Time.deltaTime;
                if (winCountdown < 0)
                {
                    restartButton.gameObject.SetActive(true);
                    homeButton.gameObject.SetActive(true);
                }
                break;

            case GameTurn.GameRestart:

                break;
        }
    }
    #endregion

    void PlayerPositionYaxis()
    {
        if (Player1.transform.position.y > 0)
        {
            player1yPos = 0.02f;
        }

        if (Player1.transform.position.y > 1.1)
        {
            player1yPos = 0.025f;
        }

        if (Player1.transform.position.y > 2)
        {
            player1yPos = 0.03f;
        }

        if (Player1.transform.position.y > 2.5)
        {
            player1yPos = 0.035f;
        }

        if (Player1.transform.position.y > 3)
        {
            player1yPos = 0.04f;
        }

        if (Player1.transform.position.y > 3.5)
        {
            player1yPos = 0.045f;
        }

        if (Player1.transform.position.y > 4)
        {
            player1yPos = 0.05f;
        }

        if (Player1.transform.position.y > 4.75)
        {
            player1yPos = 0.055f;
        }

        ///////////////////////////////////////

        if (Player2.transform.position.y < 0)
        {
            player2yPos = 0.02f;
        }

        if (Player2.transform.position.y < -1.1)
        {
            player2yPos = 0.025f;
        }

        if (Player2.transform.position.y < -2)
        {
            player2yPos = 0.03f;
        }

        if (Player2.transform.position.y < -2.5)
        {
            player2yPos = 0.035f;
        }

        if (Player2.transform.position.y < -3)
        {
            player2yPos = 0.04f;
        }

        if (Player2.transform.position.y < -3.5)
        {
            player2yPos = 0.045f;
        }

        if (Player2.transform.position.y < -4)
        {
            player2yPos = 0.05f;
        }

        if (Player2.transform.position.y < -4.75)
        {
            player2yPos = 0.055f;
        }
    }

    public float SliderScoreThreshold()
    {
        if (powerMeter.value < 0.15)
        {
            pushStrength = UPDATEDpushStrengthAdjust * 1f;

            if (theGameTurn == GameTurn.Player1Turn)
            {
                _audioManager.PlayAudio(0);
                P1sliderResultText.text = "b a d";
                P1sliderResultText.enabled = true;
                //StartCoroutine(SliderResultText());
                //P1sliderResultText.enabled = false;
            }
            if (theGameTurn == GameTurn.Player2Turn)
            {
                _audioManager.PlayAudio(0);
                P2sliderResultText.text = "b a d";
                P2sliderResultText.enabled = true;
                //StartCoroutine(SliderResultText());
                //P2sliderResultText.enabled = false;
            }
            if (theGameTurn == GameTurn.DecideTurnPlayer1)
            {
                player1DeciderResultText.text = "b a d";
                player1DeciderResultText.enabled = true;
                player1Score = 0;
            }
            if (theGameTurn == GameTurn.DecideTurnPlayer2)
            {
                player2DeciderResultText.text = "b a d";
                player2DeciderResultText.enabled = true;
                player2Score = 0;
            }

        }
        if (powerMeter.value >= 0.15 && powerMeter.value <= 0.29)
        {
            pushStrength = UPDATEDpushStrengthAdjust * 2.25f;

            if (theGameTurn == GameTurn.Player1Turn)
            {
                _audioManager.PlayAudio(1);
                P1sliderResultText.text = "g o o d";
                P1sliderResultText.enabled = true;
                //StartCoroutine(SliderResultText());
                //P1sliderResultText.enabled = false;
            }
            if (theGameTurn == GameTurn.Player2Turn)
            {
                _audioManager.PlayAudio(1);
                P2sliderResultText.text = "g o o d";
                P2sliderResultText.enabled = true;
                //StartCoroutine(SliderResultText());
                //P2sliderResultText.enabled = false;
            }
            if (theGameTurn == GameTurn.DecideTurnPlayer1)
            {
                player1DeciderResultText.text = "g o o d";
                player1DeciderResultText.enabled = true;
                player1Score = 1;
            }
            if (theGameTurn == GameTurn.DecideTurnPlayer2)
            {
                player2DeciderResultText.text = "g o o d";
                player2DeciderResultText.enabled = true;
                player2Score = 1;
            }
        }
        if (powerMeter.value >= 0.30 && powerMeter.value <= 0.44)
        {
            pushStrength = UPDATEDpushStrengthAdjust * 3.5f;

            if (theGameTurn == GameTurn.Player1Turn)
            {
                _audioManager.PlayAudio(2);
                P1sliderResultText.text = "g r e a t";
                P1sliderResultText.enabled = true;
                //StartCoroutine(SliderResultText());
                //P1sliderResultText.enabled = false;
            }
            if (theGameTurn == GameTurn.Player2Turn)
            {
                _audioManager.PlayAudio(2);
                P2sliderResultText.text = "g r e a t";
                P2sliderResultText.enabled = true;
                //StartCoroutine(SliderResultText());
                //P2sliderResultText.enabled = false;
            }
            if (theGameTurn == GameTurn.DecideTurnPlayer1)
            {
                player1DeciderResultText.text = "g r e a t";
                player1DeciderResultText.enabled = true;
                player1Score = 2;
            }
            if (theGameTurn == GameTurn.DecideTurnPlayer2)
            {
                player2DeciderResultText.text = "g r e a t";
                player2DeciderResultText.enabled = true;
                player2Score = 2;
            }
        }
        if (powerMeter.value >= 0.45 && powerMeter.value <= 0.55)
        {
            pushStrength = UPDATEDpushStrengthAdjust * 4.75f;

            if (theGameTurn == GameTurn.Player1Turn)
            {
                _audioManager.PlayAudio(3);
                P1sliderResultText.text = "p e r f e c t!";
                P1sliderResultText.enabled = true;
                //StartCoroutine(SliderResultText());
                //P1sliderResultText.enabled = false;
            }
            if (theGameTurn == GameTurn.Player2Turn)
            {
                _audioManager.PlayAudio(3);
                P2sliderResultText.text = "p e r f e c t!";
                P2sliderResultText.enabled = true;
                //StartCoroutine(SliderResultText());
                //P2sliderResultText.enabled = false;
            }
            if (theGameTurn == GameTurn.DecideTurnPlayer1)
            {
                player1DeciderResultText.text = "p e r f e c t!";
                player1DeciderResultText.enabled = true;
                player1Score = 3;
            }
            if (theGameTurn == GameTurn.DecideTurnPlayer2)
            {
                player2DeciderResultText.text = "p e r f e c t!";
                player2DeciderResultText.enabled = true;
                player2Score = 3;
            }
        }
        if (powerMeter.value >= 0.56 && powerMeter.value <= 0.70)
        {
            pushStrength = UPDATEDpushStrengthAdjust * 3.5f;

            if (theGameTurn == GameTurn.Player1Turn)
            {
                _audioManager.PlayAudio(2);
                P1sliderResultText.text = "g r e a t";
                P1sliderResultText.enabled = true;
                //StartCoroutine(SliderResultText());
                //P1sliderResultText.enabled = false;
            }
            if (theGameTurn == GameTurn.Player2Turn)
            {
                _audioManager.PlayAudio(2);
                P2sliderResultText.text = " g r e a t";
                P2sliderResultText.enabled = true;
                //StartCoroutine(SliderResultText());
                //P2sliderResultText.enabled = false;
            }
            if (theGameTurn == GameTurn.DecideTurnPlayer1)
            {
                player1DeciderResultText.text = "g r e a t";
                player1DeciderResultText.enabled = true;
                player1Score = 2;
            }
            if (theGameTurn == GameTurn.DecideTurnPlayer2)
            {
                player2DeciderResultText.text = "g r e a t";
                player2DeciderResultText.enabled = true;
                player2Score = 2;
            }
        }
        if (powerMeter.value >= 0.71 && powerMeter.value <= 0.85)
        {
            pushStrength = UPDATEDpushStrengthAdjust * 2.25f;

            if (theGameTurn == GameTurn.Player1Turn)
            {
                _audioManager.PlayAudio(1);
                P1sliderResultText.text = "g o o d";
                P1sliderResultText.enabled = true;
                //StartCoroutine(SliderResultText());
                //P1sliderResultText.enabled = false;
            }
            if (theGameTurn == GameTurn.Player2Turn)
            {
                _audioManager.PlayAudio(1);
                P2sliderResultText.text = "g o o d";
                P2sliderResultText.enabled = true;
                //StartCoroutine(SliderResultText());
                //P2sliderResultText.enabled = false;
            }
            if (theGameTurn == GameTurn.DecideTurnPlayer1)
            {
                player1DeciderResultText.text = "g o o d";
                player1DeciderResultText.enabled = true;
                player1Score = 1;
            }
            if (theGameTurn == GameTurn.DecideTurnPlayer2)
            {
                player2DeciderResultText.text = "g o o d";
                player2DeciderResultText.enabled = true;
                player2Score = 1;
            }
        }
        if (powerMeter.value >= 0.86 && powerMeter.value <= 0.99)
        {
            pushStrength = UPDATEDpushStrengthAdjust * 1f;

            if (theGameTurn == GameTurn.Player1Turn)
            {
                _audioManager.PlayAudio(0);
                P1sliderResultText.text = "b a d";
                P1sliderResultText.enabled = true;
                //StartCoroutine(SliderResultText());
                //P1sliderResultText.enabled = false;
            }
            if (theGameTurn == GameTurn.Player2Turn)
            {
                _audioManager.PlayAudio(0);
                P2sliderResultText.text = "b a d";
                P2sliderResultText.enabled = true;
                //StartCoroutine(SliderResultText());
                //P2sliderResultText.enabled = false;
            }
            if (theGameTurn == GameTurn.DecideTurnPlayer1)
            {
                player1DeciderResultText.text = "b a d";
                player1DeciderResultText.enabled = true;
                player1Score = 0;
            }
            if (theGameTurn == GameTurn.DecideTurnPlayer2)
            {
                player2DeciderResultText.text = "b a d";
                player2DeciderResultText.enabled = true;
                player2Score = 0;
            }
        }

        return pushStrength;
    }

    //IEnumerator SliderResultText()
    //{
    //    yield return new WaitForSeconds(1);

    //    if (theGameTurn == GameTurn.Player2Turn)
    //    {
    //        P1sliderResultText.enabled = false;
    //    }
    //    if (theGameTurn == GameTurn.Player2Turn)
    //    {
    //        P2sliderResultText.enabled = false;
    //    }
    //}
}
