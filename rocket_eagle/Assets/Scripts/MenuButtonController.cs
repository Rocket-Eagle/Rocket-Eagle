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

	/*
	 * quit the game
	 */
	public void quitGame()
    {
		Application.Quit();
	}


}
