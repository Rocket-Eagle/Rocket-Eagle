using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;


public class MenuButtonController : MonoBehaviour {

	// Use this for initialization
	public AudioSource audioSource;
	public bool disableOnce;


	void Start()
	{
		audioSource = GetComponent<AudioSource>();
	}

	// Update is called once per frame
	void Update()
	{

	}

	/*
	 * play the sound (once) when the user hits the button
	 */
	public void playSelectionSound(AudioClip audioClip)
	{
		if (!disableOnce)
		{
			audioSource.PlayOneShot(audioClip);
		}
		else
		{
			disableOnce = false;
		}
	}

	public void goToMainMenu()
    {
		SceneManager.LoadScene("MainMenu");
	}

	public void playAgain()
    {
		//TODO: find a way to dynamically reload this current scene
		SceneManager.LoadScene("SingleField");
	}

	public void quitGame()
    {
		//here is a quick way to remove all the game data
		//DO NOT HAVE THIS LINE UNCOMMENTED FOR RELEASE!!!!
		/*
		{
			Debug.Log("REMOVING ALL SAVE DATA!!!");
			SaveGameData.DeleteAllData();
		}
		*/
		
		Application.Quit();
	}


}
