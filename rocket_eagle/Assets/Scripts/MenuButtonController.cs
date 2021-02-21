using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;


public class MenuButtonController : MonoBehaviour {

	// Use this for initialization
	[SerializeField] int maxIndex;
	public AudioSource audioSource;
	public bool disableOnce;



	void Start () 
	{
		audioSource = GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () 
	{		
		
	}

	/*
	 * this is the function that is called when the buttons on the main menu are pressed
	 * the index for the animation is set and then the buttons are executed.
	 * 
	 */
	public void ButtonPressed(int buttonIndex)
	{
		switch (buttonIndex)
		{
			case 0:
				//clicked on the single player option
				Debug.Log("Clicked: SP Menu");

				//launch the SP sub menu
				break;
			case 1:
				//clicked on the multi player option
				Debug.Log("Clicked: MP Menu");

				//launch the MP sub menu
				break;
			case 2:
				//clicked on the customize option
				Debug.Log("Clicked: CUST Menu");

				//launch the customization sub menu
				break;
			case 3:
				//clicked on the options option
				Debug.Log("Clicked: OP Menu");

				//launch the options sub menu
				break;

			default:
					Debug.Log("WARNING! Button was selected that doesn't have a proper function with it!");
					Debug.Log("The Index was:" + buttonIndex);
				break;
		}
		
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

}
