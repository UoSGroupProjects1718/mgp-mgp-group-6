using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Hierarchy References")]
    public GameObject Empty;
    public Canvas canvas;

    [Header("Main Camera (shake) Reference")]
    public Camera camera;
    public float duration = 2.0f;
    public float slowDownAmount = 5.0f;
    public float shakeStrengthModifier = 0.1f;
    private bool shouldShake;
    private Vector3 startPosition;
    private float initialDuration;

    CountdownTimer _countDownTimer;
    GameManager _gameManager;
    PowerUp _powerUp;
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
        _countDownTimer = canvas.GetComponent<CountdownTimer>();
        _powerUp = Empty.GetComponent<PowerUp>();
        Player1rb = Player1.GetComponent<Rigidbody2D>();
        Player2rb = Player2.GetComponent<Rigidbody2D>();

        startPosition = camera.transform.localPosition;
        initialDuration = duration;
    }

    void Update()
    {
        if (shouldShake && duration > 0)
        {
            camera.transform.localPosition = startPosition + Random.insideUnitSphere * _gameManager.pushStrength * shakeStrengthModifier;
            duration -= Time.deltaTime * slowDownAmount;
        }
        else if (duration < 0)
        {
            //Debug.Log("camera shake resetting");
            shouldShake = false;
            duration = initialDuration;
            camera.transform.localPosition = startPosition;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (this.gameObject == Player1)
        {
            //Debug.Log("PlayerController: Player1");
            //_gameManager.sliderSpeedAdjust += 0.0025f;
            if (_countDownTimer.timeLeftReset > 1)
                _countDownTimer.timeLeftReset -= 1;
            _countDownTimer.countdownFillDiv1 = _countDownTimer.timeLeftReset;

            Player1rb.AddForce(transform.up * -_gameManager.pushStrength, ForceMode2D.Impulse);
            shouldShake = true;
            Player1ps.Play();


            if (_powerUp.isDoubleChance == false)
            {
                _gameManager.theGameTurn = GameManager.GameTurn.Player1Turn;
                GameManager._boolPlayer1Turn = true;
            }
            if (_powerUp.isDoubleChance == true)
            {
                _gameManager.theGameTurn = GameManager.GameTurn.Player2Turn;
                GameManager._boolPlayer1Turn = false;
                _powerUp.isDoubleChance = false;
            }

            _powerUp.DecidePowerUp();
            _powerUp.isPowerHit = false;
        }
        else
        if (this.gameObject == Player2)
        {
            //Debug.Log("PLayerController: Player2");
            //_gameManager.sliderSpeedAdjust += 0.0025f;
            if (_countDownTimer.timeLeftReset > 2)
                _countDownTimer.timeLeftReset -= 1;
            _countDownTimer.countdownFillDiv1 = _countDownTimer.timeLeftReset;

            Player2rb.AddForce(transform.up * -_gameManager.pushStrength, ForceMode2D.Impulse);
            shouldShake = true;
            Player2ps.Play();

            if (_powerUp.isDoubleChance == false)
            {
                _gameManager.theGameTurn = GameManager.GameTurn.Player2Turn;
                GameManager._boolPlayer1Turn = false;
            }
            if (_powerUp.isDoubleChance == true)
            {
                _gameManager.theGameTurn = GameManager.GameTurn.Player1Turn;
                GameManager._boolPlayer1Turn = true;
                _powerUp.isDoubleChance = false;
            }
          
            _powerUp.DecidePowerUp();
            _powerUp.isPowerHit = false;
        }
    }
}