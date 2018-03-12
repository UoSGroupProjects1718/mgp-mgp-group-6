using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public Button mainMenuStartButton;

	void Start ()
    {
        mainMenuStartButton = mainMenuStartButton.GetComponent<Button>();
    }
	

	void Update ()
    {
		
	}

    public void MainMenuStart()
    {
        SceneManager.LoadScene(1);
    }
}
