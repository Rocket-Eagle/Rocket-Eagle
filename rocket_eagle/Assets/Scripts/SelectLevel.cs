using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using UnityEngine.SceneManagement;

public class SelectLevel : MonoBehaviour
{
    //the array that holds the images that the player can cycle through
    private Object[] imagePreviews;
    //the currently previewed image
    private int currentSelected;

    private void Awake()
    {
        //load in all the images in the 'Assets/Resources/LevelSelection/Levels' file
        //NOTE: these images must be of the size (UNDEFINED AS OF RIGHT NOW) in order for them to load properly
        imagePreviews = Resources.LoadAll("LevelSelection/Levels", typeof(Sprite));


        Debug.Log("Found:" + imagePreviews.Length + " number of images");

        //set the index
        currentSelected = 0;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /*
     * move to the previous preview image
     */
    public void CycleLeft()
    {
        Debug.Log("Going right from index:" + currentSelected);
        if (currentSelected == 0)
        {
            currentSelected = imagePreviews.Length;
        }

        currentSelected--;
        Debug.Log("To:" + currentSelected);

        //reload the preview image
        ReloadPreviewImage();
    }

    /*
     * move to the next preview image
     */
    public void CycleRight()
    {
        Debug.Log("Going right from index:" + currentSelected);
        if (currentSelected == imagePreviews.Length - 1)
        {
            currentSelected = 0;
        }
        else
        {
            currentSelected++;
        }
        Debug.Log("To:" + currentSelected);

        //reload the preview image
        ReloadPreviewImage();
    }

    /*
     * change the image in the preview window
     */
    private void ReloadPreviewImage()
    {
        GetComponent<Image>().sprite = (imagePreviews[currentSelected] as Sprite);
    }

    /*
     * this is called when the user his the "PLAY" button
     */
    public void PlayGame()
    {
        Debug.Log("Playing level:" + currentSelected);

        //unsure of what is better here, to do Additive or Single. I think that since this is loading from
        //the menu to the loading screen Additive is fine
        SceneManager.LoadScene("LoadingScreen", LoadSceneMode.Additive);
    }

    /*
     * debug stuff
     */
    public void PrintImageListNames()
    {
        Debug.Log("Printing the list of images read in:");
        for (int i = 0; i < imagePreviews.Length; i++)
        {
            Debug.Log(imagePreviews[i].name);
        }
    }
}
