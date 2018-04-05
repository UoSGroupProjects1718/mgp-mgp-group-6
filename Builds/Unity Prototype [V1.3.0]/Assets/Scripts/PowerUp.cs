using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PowerUp : MonoBehaviour
{
    [Header("[Hierarchy Objects]")]
    public GameObject Empty;
    public GameObject Player1;
    public GameObject Player2;
    public Text player1PowerUpText, player2PowerUpText;
    public Image player1PowerUpImage, player2PowerUpImage;
    public Sprite powerHit, recovery, extraTurn;
    GameManager _gameManager;
    PlayerController _playerController;
    AudioManager _audioManager;

    public enum PowerUps
    { unlucky, PowerHit, DoubleChance, Recover };
    [Header("[Game States (enum)]")]
    public PowerUps thePowerUps;

    [Header("[Randomise Values]")]
    private int randomLow = 0;
    private int randomHigh = 100;
    private int chance;
    private int powerChanceWeighting;
    private float player1threshold, player2threshold;
    public bool isPowerHit;
    private bool audioIsPowerHit;
    public bool isDoubleChance;
    private bool audioIsDoubleChance;
    public bool isRecover;
    private bool audioIsRecover;

    void Start()
    {
        _gameManager = Empty.GetComponent<GameManager>();
        _audioManager = Empty.GetComponent<AudioManager>();
        player1PowerUpText.text = "";
        player2PowerUpText.text = "";

        isPowerHit = false;
        audioIsPowerHit = false;
        isDoubleChance = false;
        audioIsDoubleChance = false;
        isRecover = false;
        audioIsRecover = false;
    }

    void Update()
    {
        chance = Random.Range(randomLow, randomHigh);
        powerChanceWeighting = Random.Range(randomLow, randomHigh);

        PlayerPositionChanceThreshold();

        //DecidePowerUp();

        PowerDecider();
    }

    void PowerDecider()
    {
        switch (thePowerUps)
        {
            case PowerUps.unlucky:
                Debug.Log("unlucky");
                player1PowerUpText.enabled = false;
                player2PowerUpText.enabled = false;
                player1PowerUpImage.enabled = false;
                player2PowerUpImage.enabled = false;
                break;

            case PowerUps.PowerHit:
                Debug.Log("PowerHit");
                isPowerHit = true;
                audioIsDoubleChance = false;
                audioIsRecover = false;
                if (audioIsPowerHit)
                {
                    _audioManager.PlayAudio(4);
                    audioIsPowerHit = false;
                }
                if (GameManager._boolPlayer1Turn == true)
                {
                    player2PowerUpImage.enabled = false;
                    player2PowerUpText.enabled = false;
                    player1PowerUpText.text = "POWER HIT!";
                    player1PowerUpText.enabled = true;
                    player1PowerUpText.color = new Color(255f, 0f, 0f, 1f);
                    player1PowerUpImage.sprite = powerHit;
                    player1PowerUpImage.enabled = true;
                }
                if (GameManager._boolPlayer1Turn == false)
                {
                    player1PowerUpImage.enabled = false;
                    player1PowerUpText.enabled = false;
                    player2PowerUpText.text = "POWER HIT!";
                    player2PowerUpText.enabled = true;
                    player2PowerUpText.color = new Color(255f, 0f, 0f, 1f);
                    player2PowerUpImage.sprite = powerHit;
                    player2PowerUpImage.enabled = true;
                }
                break;

            case PowerUps.DoubleChance:
                Debug.Log("DoubleChance");
                isDoubleChance = true;
                audioIsPowerHit = false;
                audioIsRecover = false;
                if (audioIsDoubleChance)
                {
                    _audioManager.PlayAudio(5);
                    audioIsDoubleChance = false;
                }
                if (GameManager._boolPlayer1Turn == true)
                {
                    player2PowerUpImage.enabled = false;
                    player2PowerUpText.enabled = false;
                    player1PowerUpText.text = "DOUBLE CHANCE!";
                    player1PowerUpText.enabled = true;
                    player1PowerUpText.color = new Color(0f, 0f, 0f, 1f);
                    player1PowerUpText.color = new Color(255f, 255f, 0f, 1f);
                    player1PowerUpImage.sprite = extraTurn;
                    player1PowerUpImage.enabled = true;
                }
                if (GameManager._boolPlayer1Turn == false)
                {
                    player1PowerUpImage.enabled = false;
                    player1PowerUpText.enabled = false;
                    player2PowerUpText.text = "DOUBLE CHANCE!";
                    player2PowerUpText.enabled = true;
                    player2PowerUpText.color = new Color(0f, 0f, 0f, 1f);
                    player2PowerUpText.color = new Color(255f, 255f, 0f, 1f);
                    player2PowerUpImage.sprite = extraTurn;
                    player2PowerUpImage.enabled = true;
                }
                break;

            case PowerUps.Recover:
                Debug.Log("Recover");
                isRecover = true;
                audioIsPowerHit = false;
                audioIsDoubleChance = false;
                if (audioIsRecover)
                {
                    _audioManager.PlayAudio(6);
                    audioIsRecover = false;
                }
                if (GameManager._boolPlayer1Turn == true)
                {
                    player2PowerUpImage.enabled = false;
                    player2PowerUpText.enabled = false;
                    player1PowerUpText.text = "RECOVERY!";
                    player1PowerUpText.enabled = true;
                    player1PowerUpText.color = new Color(0f, 255f, 0f, 1f);
                    player1PowerUpImage.sprite = recovery;
                    player1PowerUpImage.enabled = true;
                }
                if (GameManager._boolPlayer1Turn == false)
                {
                    player1PowerUpImage.enabled = false;
                    player1PowerUpText.enabled = false;
                    player2PowerUpText.text = "RECOVERY!";
                    player2PowerUpText.enabled = true;
                    player2PowerUpText.color = new Color(0f, 255f, 0f, 1f);
                    player2PowerUpImage.sprite = recovery;
                    player2PowerUpImage.enabled = true;
                }
                break;
        }
    }

    public void DecidePowerUp()
    {
        thePowerUps = PowerUps.unlucky;

        if (GameManager._boolPlayer1Turn == true);
        {
            if (chance < player1threshold)
            {
                if (powerChanceWeighting <= 100)
                {
                    audioIsDoubleChance = true;
                    thePowerUps = PowerUps.DoubleChance;
                }
                if (powerChanceWeighting < 85)
                {
                    audioIsRecover = true;
                    thePowerUps = PowerUps.Recover;
                }
                if (powerChanceWeighting < 50)
                {
                    audioIsPowerHit = true;
                    thePowerUps = PowerUps.PowerHit;
                }
                if (powerChanceWeighting < 25)
                {
                    thePowerUps = PowerUps.unlucky;
                }
                //thePowerUps = (PowerUps)Random.Range(0, 4);
            }
        }
        if (GameManager._boolPlayer1Turn == false);
        {
            if (chance < player2threshold)
            {
                if (powerChanceWeighting <= 100)
                {
                    audioIsDoubleChance = true;
                    thePowerUps = PowerUps.DoubleChance;
                }
                if (powerChanceWeighting < 85)
                {
                    audioIsRecover = true;
                    thePowerUps = PowerUps.Recover;
                }
                if (powerChanceWeighting < 50)
                {
                    audioIsPowerHit = true;
                    thePowerUps = PowerUps.PowerHit;
                }
                if (powerChanceWeighting < 25)
                {
                    thePowerUps = PowerUps.unlucky;
                }
                //thePowerUps = (PowerUps)Random.Range(0, 4);
            }
        }
    }

    void PlayerPositionChanceThreshold()
    {
        if (Player1.transform.position.y > 1)
        {
            player1threshold = 0;
        }

        if (Player1.transform.position.y > 1.1)
        {
            player1threshold = 10;
        }

        if (Player1.transform.position.y > 2)
        {
            player1threshold = 20;
        }

        if (Player1.transform.position.y > 2.5)
        {
            player1threshold = 33;
        }

        if (Player1.transform.position.y > 3)
        {
            player1threshold = 50;
        }

        if (Player1.transform.position.y > 3.5)
        {
            player1threshold = 75;
        }

        if (Player1.transform.position.y > 4)
        {
            player1threshold = 85;
        }

        if (Player1.transform.position.y > 4.75)
        {
            player1threshold = 100;
        }

        ///////////////////////////////////////

        if (Player2.transform.position.y < -1)
        {
            player2threshold = 0;
        }

        if (Player2.transform.position.y < -1.1)
        {
            player2threshold = 10;
        }

        if (Player2.transform.position.y < -2)
        {
            player2threshold = 20;
        }

        if (Player2.transform.position.y < -2.5)
        {
            player2threshold = 33;
        }

        if (Player2.transform.position.y < -3)
        {
            player2threshold = 5;
        }

        if (Player2.transform.position.y < -3.5)
        {
            player2threshold = 75;
        }

        if (Player2.transform.position.y < -4)
        {
            player2threshold = 85;
        }

        if (Player2.transform.position.y < -4.75)
        {
            player2threshold = 100;
        }
    }
}
