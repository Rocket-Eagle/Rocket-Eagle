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
    [SerializeField] private GameObject skinPriceDisplay;

    private const string SUCCESS_MESSAGE = "Y o u   P u r c h a s e d   T h e   {0}\nS k i n";
    private const string FAILED_MESSAGE = "S o r r y,   Y o u   D o n ' t   H a v e   E n o u g h   B i r d C o i n   A s   I t   C o s t s :   {0}";
    private const string SELECTING_MESSAGE = "S e l e c t e d   {0}   S k i n";
    private const string PRICE_MESSAGE = "C o s t: {0}";
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
            Debug.Log("Error, loaded in zero skin objects!");
            reSaveSkins();

            //set the selected skin
            SaveGameData.SaveSelectedSkin(possibleSkins[0]);
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
                ShowSuccessMessage();
            }
            else
            {
                //display message that the player doesn't have enough money
                ShowFailedMessage();
            }
        }
    }

    /*
     * Show a popup message on the skin preview image
     * @param message the message to be read to the user (if using the set messages declared above make sure to format them before calling this, when applicable)
     */
    private void ShowPopUpMessage(String message)
    {
        //show the message
        purchaseMessage.SetActive(true);
        purchaseMessage.GetComponent<Text>().text = message;
    }

    /*
     * show the successful purchase message for a short time
     */
    private void ShowSelectMessage()
    {
        String message = String.Format(SELECTING_MESSAGE, possibleSkins[currentSelected].GetPreviewImageName());
        ShowPopUpMessage(message);
    }

    /*
     * show the successful purchase message for a short time
     */
    private void ShowSuccessMessage()
    {
        String message = String.Format(SUCCESS_MESSAGE, possibleSkins[currentSelected].GetPreviewImageName());
        ShowPopUpMessage(message);
    }

    /*
     * show the failed purchase message for a short time
     */
    private void ShowFailedMessage()
    {
        String message = String.Format(FAILED_MESSAGE, possibleSkins[currentSelected].GetCost());
        ShowPopUpMessage(message);
    }

    /*
     * save all the current data
     */
    public void saveData()
    {
        SaveGameData.SavePlayerCoin(birdCoinWallet);
        SaveGameData.SaveSkins(possibleSkins);
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
        //save the currently selected skin, this is going to be loaded in the BirdController class
        SaveGameData.SaveSelectedSkin(possibleSkins[currentSelected]);
        ShowSelectMessage();
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
        if (currentSelected == 0)
        {
            currentSelected = possibleSkins.Length;
        }

        currentSelected--;

        //make sure the correct button shows up on the screen
        enableButton();

        //reload the preview image
        ReloadPreviewImage();

        //reload the cost of the skin
        ReloadSkinCost();
    }

    /*
     * move to the next preview image
     */
    public void CycleRight()
    {
        if (currentSelected == possibleSkins.Length - 1)
        {
            currentSelected = 0;
        }
        else
        {
            currentSelected++;
        }

        //make sure the correct button shows up on the screen
        enableButton();

        //reload the preview image
        ReloadPreviewImage();

        //reload the cost of the skin
        ReloadSkinCost();
    }

    /*
     * change the image in the preview window
     */
    private void ReloadPreviewImage()
    {
        Sprite newSprite = possibleSkins[currentSelected].GetPreviewImage();

        selectionScreen.sprite = newSprite;
    }

    /*
     * This is the function that reloads the cost of the skins so that the user can see what they cost
     */
    private void ReloadSkinCost()
    {
        string msg = "Already purchased!";
        if (possibleSkins[currentSelected].GetIsPurchased())
        {
            //the skin is already purchased, so don't display a price
            skinPriceDisplay.SetActive(false);
        }
        else
        {
            msg = String.Format(PRICE_MESSAGE, possibleSkins[currentSelected].GetCost());
            skinPriceDisplay.SetActive(true);
            skinPriceDisplay.GetComponent<Text>().text = msg;
        }
        
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

        //clear the message box
        purchaseMessage.SetActive(false);
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

        int index = 0;
        possibleSkins = new Skins[4];
        possibleSkins[index++] = new Skins("bird", 0, true);//this is the default skin
        possibleSkins[index++] = new Skins("blueBird", 150, false);
        possibleSkins[index++] = new Skins("fireBird", 200, false);
        possibleSkins[index++] = new Skins("GoldBird", 500, false);

        SaveGameData.SaveSkins(possibleSkins);
        
        //TEMP: MAKE A coin SO THERE IS SOMETHING TO LOAD
        SaveGameData.SavePlayerCoin(0);
    }
}
