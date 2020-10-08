using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{


	int _currentScore;

	GameObject[] pauseObjects;
	GameObject[] finishObjects;
	PlayerController playerController;
	// Use this for initialization
	void Start()
	{
		Time.timeScale = 1;

		pauseObjects = GameObject.FindGameObjectsWithTag("ShowOnPause");            //gets all objects with tag ShowOnPause
		finishObjects = GameObject.FindGameObjectsWithTag("ShowOnFinish");          //gets all objects with tag ShowOnFinish


		hidePaused();
		hideFinished();

		//Checks to make sure MainLevel is the loaded level
		if (Application.loadedLevelName == "MainMenu")
			playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
	}

	// Update is called once per frame
	void Update()
	{

		//uses the p button to pause and unpause the game
		if (Input.GetKeyDown(KeyCode.Escape))
		{
			if (Time.timeScale == 1 && playerController.alive == true)
			{
				Time.timeScale = 0;
				showPaused();
			}
			else if (Time.timeScale == 0 && playerController.alive == true)
			{
				Time.timeScale = 1;
				hidePaused();
			}
		}

		//shows finish gameobjects if player is dead and timescale = 0
		if (Time.timeScale == 0 && playerController.alive == false)
		{
			showFinished();
		}
	}


	//Reloads the Level
	public void Reload()
	{
		Application.LoadLevel(Application.loadedLevel);
	}

	//controls the pausing of the scene
	public void pauseControl()
	{
		if (Time.timeScale == 1)
		{
			Time.timeScale = 0;
			showPaused();
		}
		else if (Time.timeScale == 0)
		{
			Time.timeScale = 1;
			hidePaused();
		}
	}

	//shows objects with ShowOnPause tag
	public void showPaused()
	{
		foreach (GameObject g in pauseObjects)
		{
			g.SetActive(true);
		}
	}

	//hides objects with ShowOnPause tag
	public void hidePaused()
	{
		foreach (GameObject g in pauseObjects)
		{
			g.SetActive(false);
		}
	}

	//shows objects with ShowOnFinish tag
	public void showFinished()
	{
		foreach (GameObject g in finishObjects)
		{
			g.SetActive(true);
		}
	}

	//hides objects with ShowOnFinish tag
	public void hideFinished()
	{
		foreach (GameObject g in finishObjects)
		{
			g.SetActive(false);
		}
	}

	//loads inputted level
	public void LoadLevel(string level)
	{
		Application.LoadLevel(level);
	}

	public void ExitLevel()
	{
		int highScore = PlayerPrefs.GetInt("Highscore");
		if (_currentScore > highScore)
		{
			PlayerPrefs.SetInt("HighScore", _currentScore);
			Debug.Log("New high score: " + _currentScore);
		}
		SceneManager.LoadScene("MainMenu");
	}

	public void RestartLevel()
    {
		SceneManager.LoadScene("Level01");
	}
}
