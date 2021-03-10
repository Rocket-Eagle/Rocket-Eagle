using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.UI;
using System;

[RequireComponent(typeof(Image))]
public class PurchaseSkin : MonoBehaviour
{

    [SerializeField] public Skins[] possibleSkins;
    [SerializeField] public Text bcWalletDisplay;
    [SerializeField] public Image selectionScreen;
    [SerializeField] private uint birdCoinWallet = 0;

    [SerializeField] private GameObject selectButton;
    [SerializeField] private GameObject purchaseButton;

    [SerializeField] private GameObject purchaseMessage;

    private const string successMessage = "Y o u   P u r c h a s e d   T h e   {0}\nS k i n";
    private const string failedMessage = "S o r r y,   Y o u   D o n ' t   H a v e   E n o u g h   B i r d C o i n";
    private const string selectingMessage = "S e l e c t e d   {0}   S k i n";
    private const float MESSAGE_TIME = 5;

    //var to keep track of whether we are currently loading data
    private bool onLoad = false;
    private int currentSelected;

    /*
     * load in the data on start
     */
    public void Start()
    {
        onLoad = true;

        currentSelected = 0;

        //load in the skins data to know what has already been purchased
        possibleSkins = SaveGameData.LoadSkins();
        if(possibleSkins == null)
        {
            //this is either an error, or a onetime initialization
            Debug.LogError("Error, loaded in zero skin objects!");
            reSaveSkins();
            Debug.Log("Resaving skins, if you see this message again, there is a problem");
        }

        //load in the players BirdCoin wallet to know what kind of money they have
        birdCoinWallet = SaveGameData.LoadPlayerCoin();
        updateBirdcoinDisplay();

        //make sure that the correct button is being displayed
        enableButton();

        onLoad = false;
    }

    /*
     * try to preform purchase on the currently selected item
     * checks to see if the skin is avaliable (not already purchased) and that the player has enough money
     * 
     */
    public void PreformPurchase()
    {
        //first check if the skin has already been purchased or not
        if (!possibleSkins[currentSelected].GetIsPurchased())
        {
            //check if there is enough birdcoin
            if (possibleSkins[currentSelected].GetCost() <= birdCoinWallet)
            {
                //purchase is valid
                unlockSkin(currentSelected);

                //update the users birdcoin
                removeBirdCoin(possibleSkins[currentSelected].GetCost());

                //the skin is now avaliable to update the button
                enableButton();

                //show a message to the user
                ShowSuccessMessage(MESSAGE_TIME);
            }
            else
            {
                //display message that the player doesn't have enough money
                ShowFailedMessage(MESSAGE_TIME);
            }
        }
    }

    /*
     * Show a popup message on the skin preview image
     * @param message the message to be read to the user (if using the set messages declared above make sure to format them before calling this, when applicable)
     */
    private IEnumerator ShowPopUpMessage(String message, float delay)
    {
        purchaseMessage.SetActive(true);
        purchaseMessage.GetComponent<Text>().text = message;
        yield return new WaitForSeconds(delay);
        purchaseMessage.SetActive(false);

    }

    /*
     * show the successful purchase message for a short time
     */
    private void ShowSelectMessage(float delay)
    {
        String message = String.Format(selectingMessage, possibleSkins[currentSelected].GetPreviewImageName());
        StartCoroutine(ShowPopUpMessage(message, delay));
    }

    /*
     * show the successful purchase message for a short time
     */
    private void ShowSuccessMessage(float delay)
    {
        String message = String.Format(successMessage, possibleSkins[currentSelected].GetPreviewImageName());
        StartCoroutine(ShowPopUpMessage(message, delay));
    }

    /*
     * show the failed purchase message for a short time
     */
    private void ShowFailedMessage(float delay)
    {
        StartCoroutine(ShowPopUpMessage(failedMessage, delay));
    }

    /*
     * save all the current data
     */
    public void saveData()
    {
        Debug.Log("Saving game data!");
        SaveGameData.SavePlayerCoin(birdCoinWallet);
        SaveGameData.SaveSkins(possibleSkins);
        Debug.Log("Game data saved");
    }

    /*
     * remove birdcoin from the players wallet
     * amount should be positive
     */
    public void removeBirdCoin(uint amount)
    {
        if (amount < 0)
        {
            Debug.LogError("ERROR, removeBirdCoin(amount) shoud be positive");
        }
        else
        {
            birdCoinWallet -= amount;
            updateBirdcoinDisplay();
        }
    }

    /*
     * add birdcoin to the players wallet
     * amount must be positive
     */
    public void addBirdCoin(uint amount)
    {
        if(amount < 0)
        {
            Debug.LogError("ERROR, addBirdCoin(amount) shoud be positive");
        }
        else
        {
            birdCoinWallet += amount;
            updateBirdcoinDisplay();
        }
    }

    /*
     * update the 'BirdCoinCount' prefab on the customization menu's page
     */
    private void updateBirdcoinDisplay()
    {
        bcWalletDisplay.text = birdCoinWallet.ToString();

        if (!onLoad)
        {
            //I AM UNSURE IF THE UPDATEBIRDCOIDISPLAY() WILL CAUSE PROBLEMS IF THE SCENE IS NOT LOADED
            //IF THIS IS THE CASE, MOVE THIS SAVE LINE TO INSIDE TEH ADD AND REMOVE BIRDCOIN()'S
            SaveGameData.SavePlayerCoin(birdCoinWallet);
        }
    }

    /*
     * go to the birdController class and update the Image that it is using
     */
    public void updatePlayerSkin()
    {
        Debug.Log("Selecting this player skin");

        //save the currently selected skin, this is going to be loaded in the BirdController class
        SaveGameData.SaveSelectedSkin(possibleSkins[currentSelected]);
        ShowSelectMessage(MESSAGE_TIME);
    }

    /*
     * unlock the skin in the array at spot 'theSkinIndex'
     */
    public void unlockSkin(int theSkinIndex)
    {
        //unlock the skin
        possibleSkins[theSkinIndex].unlockSkin();

        //save the game data
        saveData();
    }

    /*
     * move to the previous preview image
     */
    public void CycleLeft()
    {
        Debug.Log("Going right from index:" + currentSelected);
        if (currentSelected == 0)
        {
            currentSelected = possibleSkins.Length;
        }

        currentSelected--;
        Debug.Log("To:" + currentSelected);

        //make sure the correct button shows up on the screen
        enableButton();

        //reload the preview image
        ReloadPreviewImage();
    }

    /*
     * move to the next preview image
     */
    public void CycleRight()
    {
        Debug.Log("Going right from index:" + currentSelected);
        if (currentSelected == possibleSkins.Length - 1)
        {
            currentSelected = 0;
        }
        else
        {
            currentSelected++;
        }
        Debug.Log("To:" + currentSelected);

        //make sure the correct button shows up on the screen
        enableButton();

        //reload the preview image
        ReloadPreviewImage();
    }

    /*
     * change the image in the preview window
     */
    private void ReloadPreviewImage()
    {
        Debug.Log("Trying to reload the image");
        Debug.Log("Trying to load image at skin:" + currentSelected);
        for (int i = 0; i < possibleSkins.Length; i++)
        {
            Debug.Log("Skin:" + i + " name:" + possibleSkins[i].GetPreviewImage().ToString());
        }

        Sprite newSprite = possibleSkins[currentSelected].GetPreviewImage();

        Debug.Log("Sprint name:" + newSprite.name);

        selectionScreen.sprite = newSprite;
    }

    /*
     * This makes sure the approriate button shows up when cycling through the skins
     * Shows select button if the skin is purchased
     * shows the buy button if the skin is not purchased
     */
    private void enableButton()
    {
        if(possibleSkins[currentSelected].GetIsPurchased())
        {
            //if the skin is purchased, don't let the player purchase it again,
            //give them the ability to select the skin (show the select button)
            selectButton.SetActive(true);
            purchaseButton.SetActive(false);
        }
        else
        {
            //if this skin is not purchased don't let them select it
            //show the purchase button
            selectButton.SetActive(false);
            purchaseButton.SetActive(true);
        }
    }

    /*
     * the only purpose for this function is to resave skins. This would be nessisary when:
     *      - adding new skins
     *      - changing the price of a skin (doesn't handle checking if the skin is purchased)
     *      - resetting purchased skin
     *      - run on first time startup
     *      
     */
    private void reSaveSkins()
    {
        //TEMP: MAKE A SKIN SO THERE IS SOMETHING TO LOAD
        
        possibleSkins = new Skins[3];
        possibleSkins[0] = new Skins("bird", 0, true);//this is the default skin
        possibleSkins[1] = new Skins("blueBird", 150, false);
        possibleSkins[2] = new Skins("fireBird", 200, false);

        SaveGameData.SaveSkins(possibleSkins);
        
        //TEMP: MAKE A coin SO THERE IS SOMETHING TO LOAD
        SaveGameData.SavePlayerCoin(0);
    }
}
