using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuButton : MonoBehaviour
{
	[SerializeField] MenuButtonController menuButtonController;
	[SerializeField] Animator animator;
	[SerializeField] AnimatorFunctions animatorFunctions;
	[SerializeField] int thisIndex;

    // Update is called once per frame
    void Update()
    {
		playAnimations();
    }

	public void playAnimations()
    {
		if (menuButtonController.index == thisIndex)
		{
			//Debug.Log("Found Matching index:" + thisIndex);
			animator.SetBool("selected", true);
			if (Input.GetAxis("Submit") == 1 || Input.GetMouseButtonDown(0))
			{
				//the user has selected the menu item at thisIndex
				animator.SetBool("pressed", true);
				Debug.Log("Detected click for:" + thisIndex);
			}
			else if (animator.GetBool("pressed"))
			{
				animator.SetBool("pressed", false);
				animatorFunctions.disableOnce = true;
				Debug.Log("Reseting pressed variable");
			}
		}
		else
		{
			animator.SetBool("selected", false);

		}
	}
}
