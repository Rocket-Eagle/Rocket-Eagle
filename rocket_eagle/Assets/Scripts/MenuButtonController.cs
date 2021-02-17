using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MenuButtonController : MonoBehaviour {

	// Use this for initialization
	[SerializeField] bool keyDown;
	[SerializeField] int maxIndex;
	public int index;
	public AudioSource audioSource;

	

	void Start () 
	{
		audioSource = GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () 
	{		
		/*
		if(Input.GetAxis ("Vertical") != 0){
			if(!keyDown)
			{
				//Debug.Log(String.Format("Index:{0}", index));
				if (Input.GetAxis ("Vertical") < 0) 
				{
					if(index < maxIndex)
					{
						index++;
					}else
					{
						index = 0;
					}
				} else if(Input.GetAxis ("Vertical") > 0)
				{
					if(index > 0)
					{
						index --; 
					}else
					{
						index = maxIndex;
					}
				}
				keyDown = true;
			}
		}else
		{
			keyDown = false;
		}
		*/
	}

	/*
	 * this is the function that is called when the buttons on the main menu are pressed
	 * the index for the animation is set and then the buttons are executed.
	 * 
	 */
	public void ButtonPressed(int buttonIndex)
	{
		index = buttonIndex;
		switch (index)
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
		}
		
	}

}
