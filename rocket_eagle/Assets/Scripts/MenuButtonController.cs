using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class MenuButtonController : MonoBehaviour {

	// Use this for initialization
	public int index;
	[SerializeField] bool keyDown;
	[SerializeField] int maxIndex;
	public AudioSource audioSource;

	void Start () 
	{
		audioSource = GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () 
	{
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
	}

}
