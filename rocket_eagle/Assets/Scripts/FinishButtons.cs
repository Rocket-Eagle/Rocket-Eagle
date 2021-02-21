using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FinishButtons : MonoBehaviour
{
	// Start is called before the first frame update
	void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

	public void ButtonPressed(int buttonIndex)
	{
		switch (buttonIndex)
		{
			case 0:
				SceneManager.LoadScene("MainMenu");
				break;
			case 1:
				SceneManager.LoadScene("Field");
				break;
		}

	}
}
